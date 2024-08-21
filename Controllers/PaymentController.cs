using Newtonsoft.Json;
using OrangeProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace OrangeProjectMVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string PayPalBaseUrl = "https://api.sandbox.paypal.com/";
        private readonly string ClientId = "ARmbTVRGNhDVhiAzA5JRjs9GVG2eTPrL6PX-hMfUHZC94WoJVMUPV3uv25tbpKHJmi6lrjBtjFKOv9bs"; // Replace with your sandbox client ID
        private readonly string Secret = "EK9dHi0sChUdkpKOzalHXGoDiIRH7g7Q6qDwR5aH_2IvJsUUuzjAtvMoff2LiNUCjO8DrBjyZUPrCfTZ"; // Replace with your sandbox secret

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Checkout(decimal price)
        {
            try
            {
                var accessToken = await GetAccessToken();

                var paymentPayload = new
                {
                    intent = "sale",
                    payer = new
                    {
                        payment_method = "paypal"
                    },
                    transactions = new[]
                    {
                        new
                        {
                            amount = new
                            {
                                total = price.ToString("F2"),
                                currency = "USD"
                            },
                            description = "Payment description"
                        }
                    },
                    redirect_urls = new
                    {
                        cancel_url = Url.Action("Cancel", "Payment", null, Request.Url.Scheme),
                        return_url = Url.Action("Return", "Payment", null, Request.Url.Scheme)
                    }
                };

                var paymentJson = JsonConvert.SerializeObject(paymentPayload);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PayPalBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(paymentJson, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/v1/payments/payment", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

                        string approvalUrl = null;
                        var linksArray = responseObject.links as Newtonsoft.Json.Linq.JArray;
                        if (linksArray != null)
                        {
                            var approvalLink = linksArray.FirstOrDefault(l => (string)l["rel"] == "approval_url");
                            if (approvalLink != null)
                            {
                                approvalUrl = approvalLink["href"].ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(approvalUrl))
                        {
                            return Redirect(approvalUrl);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Approval URL not found in PayPal response.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to initiate PayPal payment: " + response.ReasonPhrase;
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }

        private async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PayPalBaseUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ClientId}:{Secret}")));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var requestData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                var requestContent = new FormUrlEncodedContent(requestData);
                var response = await client.PostAsync("/v1/oauth2/token", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return responseObject.access_token;
                }
                else
                {
                    throw new Exception("Failed to retrieve PayPal access token: " + response.ReasonPhrase);
                }
            }
        }

        // Handle the return URL after payment approval
        public async Task<ActionResult> Return(string paymentId, string token, string PayerID)
        {
            try
            {
                var accessToken = await GetAccessToken();
                var paymentExecutionPayload = new
                {
                    payer_id = PayerID
                };
                var paymentExecutionJson = JsonConvert.SerializeObject(paymentExecutionPayload);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PayPalBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(paymentExecutionJson, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"/v1/payments/payment/{paymentId}/execute", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var executedPayment = JsonConvert.DeserializeObject<dynamic>(responseContent);

                        if (executedPayment.state == "approved")
                        {
                            // Save transaction to database
                            using (var db = new electionEntities())
                            {
                                var transaction = new Transaction()
                                {
                                    status = executedPayment.state,
                                    amount = Convert.ToDecimal(executedPayment.transactions[0].amount.total),
                                    paypal_transaction_id = executedPayment.id // Save the PayPal transaction ID
                                };
                                db.Transactions.Add(transaction);
                                db.SaveChanges();
                            }

                            return View("PaymentSuccess");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Payment was not approved.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to execute PayPal payment: " + response.ReasonPhrase;
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }

        // Handle the cancel URL if the user cancels the payment
        public ActionResult Cancel()
        {
            return View();
        }

        // Add Success Action Method
        public ActionResult Success()
        {
            return RedirectToAction("Index", "UserCycle");
        }
    }
}