using System.ComponentModel.DataAnnotations;

namespace OrangeProjectMVC.Models
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
