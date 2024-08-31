!(function () {
  "use strict";
  function l(t) {
    return "function" == typeof t || "[object Function]" === e.call(t);
  }
  function s(t) {
    return (
      (t = (function (t) {
        t = Number(t);
        return isNaN(t)
          ? 0
          : 0 !== t && isFinite(t)
          ? (0 < t ? 1 : -1) * Math.floor(Math.abs(t))
          : t;
      })(t)),
      Math.min(Math.max(t, 0), n)
    );
  }
  var e, n, i, o, a, u;
  Array.prototype.fill ||
    Object.defineProperty(Array.prototype, "fill", {
      value: function (t) {
        if (null == this) throw new TypeError("this is null or not defined");
        for (
          var e = Object(this),
            n = e.length >>> 0,
            r = arguments[1] >> 0,
            i = r < 0 ? Math.max(n + r, 0) : Math.min(r, n),
            r = arguments[2],
            r = void 0 === r ? n : r >> 0,
            o = r < 0 ? Math.max(n + r, 0) : Math.min(r, n);
          i < o;

        )
          (e[i] = t), i++;
        return e;
      },
    }),
    Array.prototype.find ||
      Object.defineProperty(Array.prototype, "find", {
        value: function (t) {
          if (null == this)
            throw new TypeError(
              "Array.prototype.find called on null or undefined"
            );
          if ("function" != typeof t)
            throw new TypeError("predicate must be a function");
          for (
            var e = Object(this), n = e.length >>> 0, r = arguments[1], i = 0;
            i !== n;
            i++
          )
            if (t.call(r, this[i], i, e)) return this[i];
        },
      }),
    Array.from ||
      (Array.from =
        ((e = Object.prototype.toString),
        (n = Math.pow(2, 53) - 1),
        function (t) {
          var e = Object(t);
          if (null == t)
            throw new TypeError(
              "Array.from requires an array-like object - not null or undefined"
            );
          var n,
            r = 1 < arguments.length ? arguments[1] : void 0;
          if (void 0 !== r) {
            if (!l(r))
              throw new TypeError(
                "Array.from: when provided, the second argument must be a function"
              );
            2 < arguments.length && (n = arguments[2]);
          }
          for (
            var i,
              o = s(e.length),
              a = l(this) ? Object(new this(o)) : new Array(o),
              u = 0;
            u < o;

          )
            (i = e[u]),
              (a[u] = r ? (void 0 === n ? r(i, u) : r.call(n, i, u)) : i),
              (u += 1);
          return (a.length = o), a;
        })),
    (Array.prototype.includes =
      Array.prototype.includes ||
      function (t, e) {
        if (!this)
          throw new TypeError(
            "Array.prototype.includes called on null or undefined"
          );
        if (void 0 === e) {
          for (var n = this.length; n--; ) if (this[n] === t) return !0;
        } else
          for (var n = e, r = this.length; n++ !== r; )
            if (this[n] === t) return !0;
        return !1;
      }),
    "function" != typeof Object.assign &&
      (Object.assign = function (t, e) {
        if (null == t)
          throw new TypeError("Cannot convert undefined or null to object");
        for (var n = Object(t), r = 1; r < arguments.length; r++) {
          var i = arguments[r];
          if (null != i)
            for (var o in i)
              Object.prototype.hasOwnProperty.call(i, o) && (n[o] = i[o]);
        }
        return n;
      }),
    Object.keys ||
      (Object.keys =
        ((i = Object.prototype.hasOwnProperty),
        (o = !{ toString: null }.propertyIsEnumerable("toString")),
        (u = (a = [
          "toString",
          "toLocaleString",
          "valueOf",
          "hasOwnProperty",
          "isPrototypeOf",
          "propertyIsEnumerable",
          "constructor",
        ]).length),
        function (t) {
          if ("object" != typeof t && ("function" != typeof t || null === t))
            throw new TypeError("Object.keys called on non-object");
          var e,
            n,
            r = [];
          for (e in t) i.call(t, e) && r.push(e);
          if (o) for (n = 0; n < u; n++) i.call(t, a[n]) && r.push(a[n]);
          return r;
        }));
})(),
  (function (t) {
    "use strict";
    function r(t, e) {
      for (var n = 0; n < e.length; n++) {
        var r = e[n];
        (r.enumerable = r.enumerable || !1),
          (r.configurable = !0),
          "value" in r && (r.writable = !0),
          Object.defineProperty(t, r.key, r);
      }
    }
    var e, m;
    t.Tick || (t.Tick = []),
      t.Tick.push([
        "view",
        "flip",
        ((e = e || {}),
        "function" == typeof Symbol &&
          Symbol.asyncIterator &&
          (Symbol.asyncIterator, 0),
        (m = function (t, e, n) {
          return e && r(t.prototype, e), n && r(t, n), t;
        }),
        (e.exports = function (t) {
          function n(o) {
            var t, e, a;
            o.isInitialValue() &&
              ((o.root.textContent = ""),
              (o.spacer = l.create("span", "tick-flip-spacer")),
              o.root.appendChild(o.spacer),
              (e = l.create(
                "span",
                "tick-flip-shadow-top tick-flip-shadow tick-flip-front"
              )),
              (t = l.create(
                "span",
                "tick-flip-shadow-bottom tick-flip-shadow tick-flip-back"
              )),
              o.root.appendChild(e),
              o.root.appendChild(t),
              (o.shadowCard = l.create("span", "tick-flip-card-shadow")),
              o.root.appendChild(o.shadowCard)),
              (o.spacer.textContent = o.value),
              o.isInitialValue() || l.visible(o.root)
                ? ((e = o.cards[o.cards.length - 1]) &&
                    ((e.waiting = !1), (e.offset = u()), (e.back = o.value)),
                  o.isInitialValue() &&
                    (((t = new h()).back = o.value),
                    (t.offset = null),
                    (t.progress = 1),
                    o.root.insertBefore(t.root, o.root.firstChild),
                    o.cards.push(t)),
                  ((e = new h()).offset = null),
                  (e.progress = 0),
                  (e.visual_progress = 0),
                  (e.waiting = !0),
                  (e.front = o.value),
                  e.rotate(0),
                  o.root.insertBefore(e.root, o.root.firstChild),
                  o.cards.push(e),
                  o.animating ||
                    ((o.animating = !0),
                    (a = r.getExtension(
                      r.Type.EASING_FUNCTION,
                      o.style.flipEasing
                    )),
                    (function t() {
                      var n,
                        r,
                        e,
                        i = o.cards.filter(function (t) {
                          return !t.done && !t.waiting;
                        });
                      0 === i.length
                        ? (o.animating = !1)
                        : (i.forEach(function (t) {
                            null !== t.offset &&
                              (t.progress =
                                (u() - t.offset) / o.style.flipDuration),
                              1 <= t.progress &&
                                ((t.progress = 1), (t.done = !0)),
                              (t.visual_progress = a(t.progress));
                          }),
                          i.reverse().forEach(function (t, e) {
                            e = i[e - 1];
                            e &&
                              t.visual_progress <= e.visual_progress &&
                              (t.visual_progress = e.visual_progress + 0.01);
                          }),
                          i.reverse(),
                          o.cards.forEach(function (t, e) {
                            var n = 1 - 2 * Math.abs(t.visual_progress - 0.5),
                              r = 1 - (t.visual_progress - 0.5) / 0.5,
                              n =
                                ((t.shadowFront = n),
                                (t.highlightBack = r),
                                o.cards[e + 1]);
                            n &&
                              0.5 < t.visual_progress &&
                              0 < t.visual_progress &&
                              (t.shadowBack = f(n.visual_progress));
                          }),
                          i.forEach(function (t, e) {
                            var n = t.visual_progress;
                            0.5 < n && !t.done
                              ? (t.root.style.zIndex = 10 + e)
                              : t.root.style.removeProperty("z-index"),
                              t.rotate(-180 * n);
                          }),
                          (n = 0),
                          (r = 1),
                          i.forEach(function (t) {
                            var e = Math.abs(t.visual_progress - 0.5);
                            e < r && ((r = e), (n = t.visual_progress));
                          }),
                          (e = d(n < 0.5 ? n / 0.5 : (1 - n) / 0.5)),
                          (o.shadowCard.style.opacity = e),
                          l.transform(o.shadowCard, "scaleY", e),
                          o.cards
                            .filter(function (t) {
                              return t.done;
                            })
                            .slice(0, -1)
                            .forEach(function (e) {
                              (o.cards = o.cards.filter(function (t) {
                                return t !== e;
                              })),
                                e.root.parentNode && o.root.removeChild(e.root);
                            }),
                          requestAnimationFrame(t));
                    })()))
                : o.cards.forEach(function (t) {
                    (t.back = o.value), (t.front = o.value);
                  });
          }
          var l = t.DOM,
            r = (t.Animation.animate, t.Extension),
            u = t.Date.performance,
            t = t.View,
            i = t.rooter,
            o = t.destroyer,
            a = t.drawer,
            s = t.updater,
            c = t.styler,
            f = r.getExtension(r.Type.EASING_FUNCTION, "ease-out-cubic"),
            d = r.getExtension(r.Type.EASING_FUNCTION, "ease-out-sine"),
            h =
              (m(p, [
                {
                  key: "rotate",
                  value: function (t) {
                    (this._front.style.transform = "rotateX(" + t + "deg)"),
                      (this._back.style.transform =
                        "rotateX(" + (-180 + t) + "deg)");
                  },
                },
                {
                  key: "root",
                  get: function () {
                    return this._root;
                  },
                },
                {
                  key: "front",
                  set: function (t) {
                    (this._frontValue = t), (this._textFront.textContent = t);
                  },
                  get: function () {
                    return this._frontValue;
                  },
                },
                {
                  key: "back",
                  set: function (t) {
                    (this._backValue = t), (this._textBack.textContent = t);
                  },
                  get: function () {
                    return this._backValue;
                  },
                },
                {
                  key: "highlightBack",
                  set: function (t) {
                    this._highlightBack.style.opacity = t;
                  },
                },
                {
                  key: "shadowBack",
                  set: function (t) {
                    this._shadowBack.style.opacity = t;
                  },
                },
                {
                  key: "shadowFront",
                  set: function (t) {
                    this._shadowFront.style.opacity = t;
                  },
                },
              ]),
              p);
          function p() {
            if (!(this instanceof p))
              throw new TypeError("Cannot call a class as a function");
            this._root = l.create("span", "tick-flip-card");
            var t = l.create(
                "span",
                "tick-flip-panel-front tick-flip-front tick-flip-panel"
              ),
              e = l.create("span", "tick-flip-panel-front-text"),
              n = l.create("span", "tick-flip-panel-text-wrapper"),
              r =
                (e.appendChild(n),
                l.create("span", "tick-flip-panel-front-shadow")),
              e =
                (t.appendChild(e),
                t.appendChild(r),
                l.create(
                  "span",
                  "tick-flip-panel-back tick-flip-back tick-flip-panel"
                )),
              i = l.create("span", "tick-flip-panel-back-text"),
              o = l.create("span", "tick-flip-panel-text-wrapper"),
              a =
                (i.appendChild(o),
                l.create("span", "tick-flip-panel-back-highlight")),
              u = l.create("span", "tick-flip-panel-back-shadow");
            e.appendChild(i),
              e.appendChild(a),
              e.appendChild(u),
              this._root.appendChild(t),
              this._root.appendChild(e),
              (this._front = t),
              (this._back = e),
              (this._shadowFront = r),
              (this._shadowBack = u),
              (this._highlightBack = a),
              (this._textBack = o),
              (this._textFront = n),
              (this._frontValue = null),
              (this._backValue = null);
          }
          return function (t) {
            var e = {
              cards: [],
              lastCard: null,
              initialCard: null,
              shadowAbove: null,
              shadowBelow: null,
              shadowCard: null,
              currentValue: null,
              lastValue: null,
              front: null,
              back: null,
            };
            return Object.assign(
              {},
              i(e, t, "flip"),
              s(e),
              c(e, { flipDuration: 800, flipEasing: "ease-out-bounce" }),
              a(e, n),
              o(e)
            );
          };
        }),
        (e.exports.identifier = { name: "flip", type: "view" }),
        e.exports),
      ]);
  })(window),
  (function (t, e, pn) {
    "use strict";
    var n;
    function r(t) {
      n.plugin.add.apply(null, t);
    }
    function i() {
      n.DOM.parse(document);
    }
    t &&
      "MutationObserver" in t &&
      "requestAnimationFrame" in t &&
      (((n = (function () {
        function t(t, e) {
          if (r[t])
            for (var n in e)
              if (e.hasOwnProperty(n)) {
                if (r[t][n]) return;
                r[t][n] = e[n];
              }
        }
        function F(t, e, n) {
          if (!r[t])
            throw (
              "Can't add extension with type of \"" +
              t +
              '", "' +
              t +
              '" is not a valid extension type. The following types are valid: ' +
              je(r)
            );
          if (!/^[-a-z]+$/.test(e))
            throw (
              "Can't add extension with name \"" +
              e +
              '", "' +
              e +
              '" is contains invalid characters. Only lowercase alphabetical characters and dashes are allowed.'
            );
          if (r[t][e])
            throw (
              "Can't add extension with name \"" +
              e +
              '", "' +
              e +
              '" is already added.'
            );
          r[t][e] = n;
        }
        var e,
          n,
          U = U || {},
          s = {
            FONT: "font",
            VIEW: "view",
            TRANSFORM: "transform",
            EASING_FUNCTION: "easing-function",
            TRANSITION: "transition",
          },
          r = {},
          c =
            ((r[s.FONT] = {}),
            (r[s.VIEW] = {}),
            (r[s.TRANSFORM] = {}),
            (r[s.EASING_FUNCTION] = {}),
            (r[s.TRANSITION] = {}),
            function (t, e) {
              if (!r[t])
                throw (
                  "Can't get extension with type of \"" +
                  t +
                  '", "' +
                  t +
                  '" is not a valid extension type. The following types are available: ' +
                  je(r)
                );
              if (r[t][e]) return r[t][e];
              throw (
                "Can't get extension with name \"" +
                e +
                '", "' +
                e +
                '" is not available. The following extensions are available: ' +
                je(r[t])
              );
            }),
          p = {
            Week: 6048e5,
            Day: 864e5,
            Hour: 36e5,
            Minute: 6e4,
            Second: 1e3,
            Millisecond: 1,
            Month: 2628e6,
            Year: 31536e6,
          },
          P = [
            "Januari",
            "Februari",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
          ];
        for (e in p)
          p.hasOwnProperty(e) &&
            (1 === (n = p[e])
              ? ((p.mi = n), (p.ms = n))
              : 2628e6 === n
              ? (p.M = n)
              : (p[e.charAt(0).toLowerCase()] = n),
            (p[e.toLowerCase()] = n),
            (p[e.toLowerCase() + "s"] = n));
        function o(t) {
          return t instanceof Date;
        }
        function z(t, e) {
          return (e -= t.getDay()), t.setDate(t.getDate() + e), t;
        }
        function m(t, e) {
          var n = W(t.getMonth() + 1, t.getFullYear());
          return (
            (e = "last" === e ? n : Math.max(1, Math.min(n, e))),
            t.setDate(e),
            t
          );
        }
        function j(t, e) {
          return (
            t.setMonth(
              P.map(function (t) {
                return t.toLowerCase();
              }).indexOf(e)
            ),
            t
          );
        }
        function q(t) {
          return new Date(Date.now() + t);
        }
        function G(t, e) {
          return t.toDateString() === e.toDateString();
        }
        function W(t, e) {
          return new Date(e, t, 0).getDate();
        }
        function Y(t) {
          return new Date(t.getTime() + 6e4 * t.getTimezoneOffset());
        }
        function V(n, t) {
          return t.map(function (t) {
            var t = p[t],
              e = Math.max(0, Math.floor(n / t));
            return (n %= t), e;
          });
        }
        function f(t, e, n) {
          var r = e - t,
            i = !1;
          r < 0 && ((r = t - e), (t = (a = [e, t])[0]), (e = a[1]), (i = !0));
          0 <= (a = (n = n || ["d", "h", "m"]).indexOf("m")) &&
            ("y" === n[a - 1] || "d" === n[a + 1]) &&
            (n[a].key = "M");
          var e = void 0,
            o = void 0,
            a = n.includes("y"),
            u =
              (((u = n.includes("M")) || a) &&
                ((e = new Date(t.valueOf() + r)),
                (o = tt(e, t)),
                (a = u ? Math.floor(o) : 12 * Math.floor(o / 12)),
                (r = e.valueOf() - g(v(t), a).valueOf())),
              n.map(function (t) {
                var e;
                return "y" === t || "M" === t
                  ? ((e = Math.max(0, Math.floor(o / J[t]))),
                    (o -= e * J[t]),
                    e)
                  : ((e = p[t]),
                    (t = Math.max(0, Math.floor(r / e))),
                    (r %= e),
                    t);
              }));
          return i
            ? u.map(function (t) {
                return 0 < t ? -t : t;
              })
            : u;
        }
        function B() {
          for (var t = arguments.length, e = Array(t), n = 0; n < t; n++)
            e[n] = arguments[n];
          if ("number" != typeof e[0] || "string" != typeof e[1])
            return o(e[0])
              ? f.apply(pn, e)
              : "number" == typeof e[0] && Array.isArray(e[1])
              ? V.apply(pn, e)
              : null;
          if (p[e[1]]) return e[0] * p[e[1]];
          throw '"' + e[1] + '" is not a valid amount.';
        }
        function a(t) {
          return {
            destroy: function () {
              (t.destroyed = !0),
                t.frame && cancelAnimationFrame(t.frame),
                t.styleObserver && t.styleObserver.disconnect(),
                t.didResizeWindow &&
                  window.removeEventListener("resize", t.didResizeWindow),
                t.root &&
                  t.root.parentNode &&
                  t.root.parentNode.removeChild(t.root);
            },
          };
        }
        function u(n) {
          var t =
              1 < arguments.length && arguments[1] !== pn
                ? arguments[1]
                : document.createElement("span"),
            e =
              2 < arguments.length && arguments[2] !== pn ? arguments[2] : null;
          return (
            (n.root = t),
            (n.aligned = null),
            (n.destroyed = !1),
            t &&
              e &&
              (n.root.classList.add("tick-" + e),
              n.root.setAttribute("data-view", e)),
            t &&
              t.dataset.layout &&
              (n.align =
                (t.dataset.layout.match(/left|right|center/) || [])[0] ||
                "left"),
            {
              appendTo: function (t) {
                var e =
                  1 < arguments.length && arguments[1] !== pn
                    ? arguments[1]
                    : "last";
                !n.root ||
                  (n.root && n.root.parentNode) ||
                  ("last" === e
                    ? t.childNodes.length &&
                      t.childNodes[t.childNodes.length - 1].nodeType ===
                        Node.TEXT_NODE
                      ? t.insertBefore(
                          n.root,
                          t.childNodes[t.childNodes.length - 1]
                        )
                      : t.appendChild(n.root)
                    : ("first" === e &&
                        (0 === t.childNodes.length
                          ? t.appendChild(n.root)
                          : 0 === t.children.length && t.childNodes.length
                          ? t.insertBefore(
                              n.root,
                              t.childNodes[t.childNodes.length - 1]
                            )
                          : t.insertBefore(n.root, t.children[0])),
                      "string" != typeof e && t.insertBefore(n.root, e)));
              },
            }
          );
        }
        function H(e, t) {
          return (
            (e.definition = t),
            {
              setDefinition: function (t) {
                e.definition = t;
              },
            }
          );
        }
        function l(t, e, n, r) {
          return {
            draw: function () {
              return t.dirty
                ? (e(t, r), $(t), !(t.dirty = !1))
                : (n && n(t) && $(t), !1);
            },
          };
        }
        function $(t) {
          if (!t.fit) {
            if (
              !t.root ||
              !(t.root.getAttribute("data-layout") || "").match(/fit/)
            )
              return void (t.fit = !1);
            var e = window.getComputedStyle(t.root, null);
            (t.fit = !0),
              (t.fitInfo = {
                currentFontSize: parseInt(e.getPropertyValue("font-size"), 10),
              });
          }
          (t.fitInfo.availableWidth = t.root.parentNode.clientWidth),
            (t.fitInfo.currentWidth = t.root.scrollWidth),
            (e = Math.min(
              Math.max(
                4,
                (t.fitInfo.availableWidth / t.fitInfo.currentWidth) *
                  t.fitInfo.currentFontSize
              ),
              1024
            )),
            Math.abs(e - t.fitInfo.currentFontSize) <= 1 ||
              ((t.fitInfo.currentFontSize = e),
              (t.root.style.fontSize = t.fitInfo.currentFontSize + "px"),
              t.fitInfo.currentWidth / t.fitInfo.availableWidth < 0.5 &&
                requestAnimationFrame(function () {
                  return $(t);
                }));
        }
        function d(e) {
          return (
            (e.dirty = !0),
            (e.value = null),
            (e.valueUpdateCount = 0),
            (e.isInitialValue = function () {
              return e.valueUpdateCount <= 1;
            }),
            {
              reset: function () {
                (e.dirty = !0), (e.value = null), (e.valueUpdateCount = 0);
              },
              update: function (t) {
                Fe(e.value, t) ||
                  ((e.value = t), e.valueUpdateCount++, (e.dirty = !0));
              },
            }
          );
        }
        function X(t) {
          (t.didResizeWindow = function () {
            t.dirty = !0;
          }),
            window.addEventListener("resize", t.didResizeWindow);
        }
        var Z = {
            Monday: 1,
            Tuesday: 2,
            Wednesday: 3,
            Thursday: 4,
            Friday: 5,
            Saturday: 6,
            Sunday: 0,
          },
          J = { M: 1, y: 12 },
          K = function (n) {
            var r = new XMLHttpRequest(),
              i = Date.now();
            r.open("HEAD", window.location + "?noCache=" + i),
              r.setRequestHeader("Content-Type", "text/html"),
              r.setRequestHeader("Cache-Control", "no-cache"),
              (r.onload = function () {
                var t = 0.5 * (i - Date.now()),
                  e = new Date(r.getResponseHeader("Date"));
                n(new Date(e.getTime() + t));
              }),
              r.send();
          },
          h = function (t) {
            return t.match(/(Z)|([+\-][0-9]{2}:?[0-9]*$)/g)
              ? new Date(t)
              : ((t += -1 !== t.indexOf("T") ? "Z" : ""), Y(new Date(t)));
          },
          Q = function () {
            return new Date();
          },
          v = function (t) {
            return new Date(t.valueOf());
          },
          g = function (t, e) {
            return t.setMonth(t.getMonth() + e), t;
          },
          tt = function (t, e) {
            var n =
                12 * (e.getFullYear() - t.getFullYear()) +
                (e.getMonth() - t.getMonth()),
              r = g(v(t), n);
            return -(
              n +
              (e - r < 0
                ? (e - r) / (r - g(v(t), n - 1))
                : (e - r) / (g(v(t), 1 + n) - r))
            );
          },
          et =
            "function" == typeof Symbol && "symbol" == typeof Symbol.iterator
              ? function (t) {
                  return typeof t;
                }
              : function (t) {
                  return t &&
                    "function" == typeof Symbol &&
                    t.constructor === Symbol &&
                    t !== Symbol.prototype
                    ? "symbol"
                    : typeof t;
                };
        var i = function (t, e, n) {
          return e && nt(t.prototype, e), n && nt(t, n), t;
        };
        function nt(t, e) {
          for (var n = 0; n < e.length; n++) {
            var r = e[n];
            (r.enumerable = r.enumerable || !1),
              (r.configurable = !0),
              "value" in r && (r.writable = !0),
              Object.defineProperty(t, r.key, r);
          }
        }
        function rt(e, n) {
          var t = (e.definition || []).concat(),
            r =
              ("right" === e.align && t.reverse(),
              Array.isArray(e.value)
                ? e.value.concat()
                : "object" === et(e.value)
                ? Re(e.value)
                : e.value);
          t.forEach(function (t) {
            t.presenter ||
              ((e.update = n(t)), t.presenter && t.presenter.appendTo(e.root));
          }),
            t
              .filter(function (t) {
                return t.presenter !== pn;
              })
              .forEach(function (t) {
                Array.isArray(r) && e.valueMapping
                  ? e.update(
                      t,
                      "indexes" === e.valueMapping
                        ? "right" === e.align
                          ? r.pop()
                          : r.shift()
                        : r
                    )
                  : t.key && r[t.key] !== pn
                  ? e.update(t, r[t.key])
                  : e.update(t, r);
              }),
            (e.views = t),
            it(e);
        }
        function it(t) {
          var e = !1;
          return (
            t.views
              .filter(function (t) {
                return t.presenter !== pn;
              })
              .forEach(function (t) {
                t.presenter.draw() && (e = !0);
              }),
            e
          );
        }
        function ot(r, i, t) {
          var e = Le(
            Array.isArray(r.value) ? r.value : (r.value + "").split("")
          );
          if (
            ("right" === r.align && e.reverse(),
            r.definitions.length > e.length)
          )
            for (; r.definitions.length > e.length; )
              r.definitions.pop().presenter.destroy();
          e.forEach(function (t, e) {
            var n = r.definitions[e];
            n ||
              ((n = r.definitions[e] = xt(r.definition)),
              (r.update = i(n)),
              n.presenter.appendTo(
                r.root,
                "right" === r.align ? "first" : "last"
              ));
          }),
            e.forEach(function (t, e) {
              return r.update(r.definitions[e], t);
            }),
            (r.views = e),
            at(r);
        }
        function at(n) {
          var r = !1;
          return (
            n.views.forEach(function (t, e) {
              n.definitions[e].presenter.draw() && (r = !0);
            }),
            r
          );
        }
        function ut(t, e) {
          return (t = document.createElement(t)), e && (t.className = e), t;
        }
        function lt(n, r, i) {
          var t = new MutationObserver(function (e) {
            r.forEach(function (t) {
              e.filter(function (t) {
                return r.includes(t.attributeName);
              }).length && i(n.getAttribute(t));
            });
          });
          return t.observe(n, { attributes: !0 }), t;
        }
        function st(t) {
          return t instanceof HTMLElement;
        }
        function y(t, e, n) {
          var r =
              3 < arguments.length && arguments[3] !== pn ? arguments[3] : "",
            i =
              (t.transforms || (t.transforms = []),
              t.transforms.find(function (t) {
                return t.name === e;
              }));
          i ? (i.value = n) : t.transforms.push({ name: e, value: n, unit: r }),
            Bt(t, t.transforms);
        }
        function ct(t) {
          return (
            !((t = t.getBoundingClientRect()).bottom < 0) &&
            !(t.top > window.scrollY + window.innerHeight)
          );
        }
        function ft(t) {
          return t.trim();
        }
        function w(t, e) {
          var n = e.toString();
          return C[n] || (C[n] = {}), C[n][t] || (C[n][t] = e(t)), C[n][t];
        }
        function dt(t) {
          return t
            .match(
              /[a-z]+(?:\(.*?\))?\s?(?:origin\(.*?\))?\s?(?:[a-z]+\(.*?\))?[ .a-z-0-9]*/g
            )
            .map(ht);
        }
        function ht(t) {
          var t = t.match(
              /([a-z]+(?:\(.*?\))?)\s?(?:origin\((.*?)\))?\s?([a-z]+(?:\(.*?\))?)?\s?(?:([.0-9ms]+)?\s?(?:(ease-[a-z-]+))?\s?([.0-9ms]+)?)?/
            ),
            e = Ae(t[1]),
            n = pn,
            r = pn,
            i = pn,
            o = pn,
            a = pn;
          return (
            t
              .slice(2)
              .filter(function (t) {
                return void 0 !== t;
              })
              .forEach(function (t) {
                Qt.test(t)
                  ? void 0 === r
                    ? (r = D(t))
                    : (o = D(t))
                  : / /.test(t)
                  ? (n = t)
                  : /^ease-[a-z-]+$/.test(t)
                  ? (i = t)
                  : /^[a-z]+/.test(t) && (a = Ae(t));
              }),
            a && (i = r = pn),
            {
              name: e.name,
              parameters: e.parameters,
              duration: r,
              ease: i,
              delay: o,
              origin: n,
              resolver: a,
            }
          );
        }
        function pt(t) {
          var e = t.match(
            /follow-gradient|horizontal-gradient|vertical-gradient/
          )[0];
          return {
            type: e,
            colors: t
              .substring(e.length)
              .match(
                /(?:transparent|rgb\(.*?\)|hsl\(.*?\)|hsla\(.*?\)|rgba\(.*?\)|[a-z]+|#[abcdefABCDEF\d]+)\s?(?:[\d]{1,3}%?)?/g
              )
              .map(mt),
          };
        }
        function mt(t) {
          var e = t.match(ne);
          return {
            offset: e ? parseFloat(e[1]) / 100 : null,
            value: ie(t.replace(ne, "")),
          };
        }
        function vt(t) {
          return "string" != typeof t
            ? t
            : t.match(
                /([-.\d]+(?:%|ms|s|deg|cm|em|ch|ex|q|in|mm|pc|pt|px|vh|vw|vmin|vmax)?)|[%#A-Za-z0-9,.()]+/g
              );
        }
        function gt(t) {
          var e,
            n,
            r = (t = t.split(":").map(ft))[0]
              .trim()
              .split("-")
              .map(function (t, e) {
                return 0 < e ? (e = t).charAt(0).toUpperCase() + e.slice(1) : t;
              })
              .join(""),
            t =
              ((e = t[1]),
              (t = t[0]),
              $t.test(e)
                ? "string" == typeof (n = e)
                  ? "true" === n
                  : n
                : Ht.test(e)
                ? parseInt(e, 10)
                : Xt.test(e)
                ? parseFloat(e)
                : ee.test(e)
                ? 1 ===
                  (n = (n = e).match(/url\((.*?)\)/g).map(function (t) {
                    return t.substring(4, t.length - 1);
                  })).length
                  ? n[0]
                  : n
                : Zt.test(t)
                ? Kt.test(e)
                  ? w(e, pt)
                  : w(e, ie)
                : Jt.test(t)
                ? w(e, vt)
                : !te.test(t) || "none" === e
                ? e
                : w(e, dt));
          return r && null != t ? { property: r, value: t } : null;
        }
        function b(t) {
          return t < 0.5 ? 2 * t * t : (4 - 2 * t) * t - 1;
        }
        function yt(e, t) {
          var n =
              2 < arguments.length && arguments[2] !== pn ? arguments[2] : 500,
            r = 3 < arguments.length && arguments[3] !== pn ? arguments[3] : ae;
          return ue(
            function (t) {
              e(r(t));
            },
            t,
            n,
            4 < arguments.length && arguments[4] !== pn ? arguments[4] : 0
          );
        }
        function wt(t) {
          for (
            var e = arguments.length, n = Array(1 < e ? e - 1 : 0), r = 1;
            r < e;
            r++
          )
            n[r - 1] = arguments[r];
          var i = le(),
            o = { update: null, cancel: i.cancel, getPosition: i.getPosition };
          return (
            "arrive" === t
              ? (o.update = se.apply(pn, [i.translate].concat(n)))
              : "spring" === t
              ? (o.update = fe.apply(pn, [i.translate].concat(n)))
              : "step" === t &&
                (o.update = ce.apply(pn, [i.translate].concat(n))),
            o
          );
        }
        function bt(t, e, n) {
          var r =
            3 < arguments.length && arguments[3] !== pn ? arguments[3] : 0.001;
          return Math.abs(t - e) < r && Math.abs(n) < r;
        }
        function _t(t, r, e) {
          var i = e && c(s.EASING_FUNCTION, e),
            o = c(s.TRANSITION, t);
          return function (t, e, n) {
            o.apply(pn, [t, n, e, i].concat(M(r)));
          };
        }
        function At(r) {
          var i =
              1 < arguments.length && arguments[1] !== pn
                ? arguments[1]
                : "50% 50% 0",
            o =
              2 < arguments.length && arguments[2] !== pn ? arguments[2] : 500,
            a = arguments[3];
          return function (e) {
            var n =
                1 < arguments.length && arguments[1] !== pn ? arguments[1] : 1,
              t = arguments[2];
            Vt(e, i),
              ue(
                function (t) {
                  r(e, n, t);
                },
                t,
                o,
                a
              );
          };
        }
        function Et(e) {
          var n =
            1 < arguments.length && arguments[1] !== pn ? arguments[1] : {};
          return (
            (e.lastAppliedStyles = null),
            Mt(e, n, e.root.dataset.style),
            (e.styleObserver = lt(e.root, ["data-style"], function (t) {
              Mt(e, n, t);
            })),
            {
              setStyle: function (t) {
                Mt(e, n, t);
              },
            }
          );
        }
        function Mt(t, e, n) {
          var r, i;
          t.lastAppliedStyles !== n &&
            ((t.lastAppliedStyles = n),
            (t.style = n ? k(e, oe(n)) : e),
            (r = []),
            (i = []),
            t.style.transitionIn && t.style.transitionIn.length
              ? ((r = t.style.transitionIn), (i = t.style.transitionOut))
              : t.style.transition &&
                "none" !== t.style.transition &&
                t.style.transition.forEach(function (t) {
                  t = he(t);
                  (r = r.concat(t.intro)), (i = i.concat(t.outro));
                }),
            r &&
              i &&
              ((t.transitionIn = de(r)),
              (t.transitionOut = de(i)),
              (t.skipToTransitionInEnd = de(r.map(pe))),
              (t.skipToTransitionOutEnd = de(i.map(pe)))),
            (t.dirty = !0));
        }
        function Ct(t) {
          return (
            t[Yt + "BackingStorePixelRatio"] || t.backingStorePixelRatio || 1
          );
        }
        function Dt() {
          return window.devicePixelRatio || 1;
        }
        function Nt(t) {
          t.getContext("2d").clearRect(0, 0, t.width, t.height);
        }
        function kt(t) {
          return 0 !== t.trim().length;
        }
        function It(t, e) {
          t.push(e.trim());
        }
        function Ot(t, e) {
          return kt(e) ? (It(t, e), "") : e;
        }
        function St(t, e) {
          return t.length && e.push(1 < t.length ? t.concat() : t[0]), [];
        }
        function Tt(t, e, n) {
          for (var r, i, o = "", a = [], u = null, l = !1; t < e.length; )
            if ("(" === (c = e[t])) {
              var l = !1,
                s = [o.trim()],
                c = e[(t = Tt(t + 1, e, s))];
              a.push(s), (o = "");
            } else {
              if (")" === c)
                return (
                  l &&
                    o.trim().length &&
                    (a.push([o.trim()]), (o = ""), (l = !1)),
                  kt(o) && It(a, o),
                  (a = St(a, n)),
                  t + 1
                );
              null !== u && c !== u
                ? (o += c)
                : c === u
                ? (a.push(o), (o = ""), (u = null))
                : "'" === (s = c) || '"' === s
                ? ((o = ""), (u = c))
                : "-" === (r = e)[(i = t)] && ">" === r[i + 1]
                ? ((l = !0),
                  o.trim().length && (a.push([o.trim()]), (o = "")),
                  (t += 2))
                : "," === c
                ? (l &&
                    o.trim().length &&
                    (a.push([o.trim()]), (o = ""), (l = !1)),
                  (a = St(a, n)),
                  (o = Ot(n, o)))
                : (o += c),
                t++;
            }
          return (
            ((l && o.trim().length) || (!l && o.trim().length && !a.length)) &&
              (a.push([o.trim()]), (o = "")),
            St(a, n),
            Ot(n, o),
            t
          );
        }
        function xt(t) {
          var e,
            n = {};
          for (e in t)
            t.hasOwnProperty(e) &&
              (n[e] =
                "root" === e
                  ? t[e].cloneNode()
                  : "children" === e
                  ? null === t[e]
                    ? null
                    : t[e].map(xt)
                  : "repeat" === e
                  ? null === t[e]
                    ? null
                    : xt(t[e])
                  : t[e]);
          return (n.presenter = null), n;
        }
        function Rt(t) {
          return Array.from(t).map(function (t) {
            var e,
              n = k(be, { root: t });
            for (e in t.dataset)
              t.dataset.hasOwnProperty(e) &&
                void 0 !== n[e] &&
                (n[e] = t.dataset[e]);
            return (
              n.repeat
                ? ((n.repeat = Rt(t.children).pop()),
                  Array.from(t.children).forEach(function (t) {
                    t.parentNode.removeChild(t);
                  }))
                : t.children.length && (n.children = Rt(t.children)),
              n
            );
          });
        }
        function Lt(t) {
          return t.map(function (e) {
            return (
              "string" == typeof (e = k(be, e)).root
                ? (e.root = document.createElement(e.root))
                : (e.root = document.createElement("span")),
              e.transform && (e.root.dataset.transform = e.transform),
              e.className && (e.root.className = e.className),
              e.overlay && (e.root.dataset.overlay = e.overlay),
              e.view
                ? ((e.root.dataset.view = e.view),
                  e.style && (e.root.dataset.style = e.style),
                  (e.repeat = null))
                : (e.layout && (e.root.dataset.layout = e.layout),
                  e.repeat
                    ? ((e.root.dataset.repeat = !0),
                      (e.repeat = Lt(e.children).pop()))
                    : e.children &&
                      ((e.children = Lt(e.children)),
                      e.children.forEach(function (t) {
                        e.root.appendChild(t.root);
                      }))),
              e
            );
          });
        }
        function Ft(o) {
          function a(e, t) {
            e.transform(
              t,
              function (t) {
                e.presenter.update(t);
              },
              o
            ),
              n || ((n = !0), r());
          }
          var n = !1,
            r = function t() {
              o.baseDefinition.presenter.draw(), requestAnimationFrame(t);
            };
          return (function t(e) {
            var n, r, i;
            return (
              (e.presenter =
                ((r = t),
                (i = void 0),
                (n = e).repeat
                  ? (i = ve(n.root, n.repeat, r))
                  : "string" == typeof n.view
                  ? (i = ge(n.view, n.root, n.style))
                  : we(n) && (i = me(n.root, n.children, r)),
                i)),
              (e.transform = zt(e.transform, o)),
              a
            );
          })(o.baseDefinition);
        }
        function Ut(i) {
          for (
            var t = arguments.length, o = Array(1 < t ? t - 1 : 0), e = 1;
            e < t;
            e++
          )
            o[e - 1] = arguments[e];
          return function (t, r) {
            !(function t(e, n) {
              o.length <= e ? r(n) : o[e](n, Pt(t, [e + 1]), i);
            })(0, t);
          };
        }
        function Pt(e) {
          var n =
              1 < arguments.length && arguments[1] !== pn ? arguments[1] : [],
            r = arguments[2];
          return function () {
            var t = Array.from(n);
            return Array.prototype.push.apply(t, arguments), e.apply(r, t);
          };
        }
        function zt(t, e) {
          return t
            ? "function" == typeof t
              ? t
              : ((t = ye(
                  "transform(" + (/^[a-z]+$/.test(t) ? t + "()" : t) + ")"
                )),
                _e(t, e))
            : function (t, e) {
                return e(t);
              };
        }
        function _(t) {
          return (
            (t = (t + "").match(
              /(-?[.\d]+)(%|ms|s|deg|cm|em|ch|ex|q|in|mm|pc|pt|px|vh|vw|vmin|vmax)?/
            )),
            { value: parseFloat(t[1]), units: t[2] }
          );
        }
        function A(t) {
          var n = window,
            r = t.split(".");
          return (
            r.forEach(function (t, e) {
              n[r[e]] && (n = n[r[e]]);
            }),
            n !== window ? n : null
          );
        }
        function jt(t) {
          return /^(?:[\w]+\s?:\s?[\w.]+,\s?)+(?:[\w]+\s?:\s?[\w.]+)$/g.test(t)
            ? t.match(/(?:(\w+)\s?:\s?([\w.]+))/g).reduce(function (t, e) {
                e = e.split(":");
                return (t[e[0]] = Te(e[1])), t;
              }, {})
            : Te(t);
        }
        function qt(t) {
          return parseInt(t, 10);
        }
        function Gt() {
          return window.performance.now();
        }
        function Wt(t, e, n, r) {
          var i = new XMLHttpRequest();
          r && r(i),
            i.open("GET", t, !0),
            (i.onload = function () {
              e(i.response);
            }),
            n &&
              (i.onerror = function () {
                n(i, i.status);
              }),
            i.send();
        }
        var E =
            Object.assign ||
            function (t) {
              for (var e = 1; e < arguments.length; e++) {
                var n,
                  r = arguments[e];
                for (n in r)
                  Object.prototype.hasOwnProperty.call(r, n) && (t[n] = r[n]);
              }
              return t;
            },
          M = function (t) {
            if (Array.isArray(t)) {
              for (var e = 0, n = Array(t.length); e < t.length; e++)
                n[e] = t[e];
              return n;
            }
            return Array.from(t);
          },
          Yt =
            "undefined" == typeof document
              ? null
              : (function () {
                  for (
                    var t = ["webkit", "Moz", "ms", "O"],
                      e = 0,
                      n = t.length,
                      r = document.createElement("div").style;
                    e < n;
                    e++
                  )
                    if (t[e] + "Transform" in r) return t[e];
                  return null;
                })(),
          Vt = function (t, e) {
            t.style.transformOrigin = e;
          },
          Bt = function (t, e) {
            t.style.transform = e
              .map(function (t) {
                return t.name + "(" + t.value + t.unit + ")";
              })
              .join(" ");
          },
          C = {},
          Ht = new RegExp("^[0-9]+$"),
          $t = new RegExp("^(true|false)$"),
          Xt = new RegExp("^[0-9.]+$"),
          Zt = new RegExp("color"),
          Jt = new RegExp("shadow"),
          Kt = new RegExp(
            "^(follow-gradient|horizontal-gradient|vertical-gradient)"
          ),
          Qt = new RegExp("^[.0-9]+(?:ms|s){1}$"),
          te = new RegExp("^transition-?(?:in|out)?$"),
          ee = new RegExp("^url\\("),
          D = function (t) {
            return t ? parseFloat(t) * (/ms$/.test(t) ? 1 : 1e3) : 0;
          },
          ne = /\s([\d]{1,3})%?$/,
          N = [],
          re =
            "undefined" == typeof document
              ? function (t) {
                  return 0;
                }
              : function (t) {
                  var e,
                    n,
                    r,
                    i,
                    o =
                      1 < arguments.length && arguments[1] !== pn
                        ? arguments[1]
                        : document.body,
                    a =
                      2 < arguments.length && arguments[2] !== pn
                        ? arguments[2]
                        : null;
                  return 0 == t
                    ? 0
                    : a
                    ? ((r = o),
                      (n =
                        (N[(n = a)]
                          ? N[n].find(function (t) {
                              return t.node.parentNode === r;
                            })
                          : null) || {}).node ||
                        ((n.node = document.createElement("span")),
                        (n.node.style.cssText =
                          "position:absolute;padding:0;visibility:hidden;"),
                        o.appendChild(n.node)),
                      (n.node.style.marginTop = t),
                      n.style || (n.style = window.getComputedStyle(n.node)),
                      (e = n),
                      N[(a = a)] || (N[a] = []),
                      N[a].push(e),
                      parseInt(n.style.marginTop, 10))
                    : (((i = document.createElement("span")).style.cssText =
                        "position:absolute;padding:0;visibility:hidden;margin-top:" +
                        t),
                      o.appendChild(i),
                      requestAnimationFrame(function () {
                        i.parentNode.removeChild(i);
                      }),
                      parseInt(window.getComputedStyle(i).marginTop, 10));
                },
          ie =
            "undefined" == typeof document
              ? function (t) {
                  return t;
                }
              : function (t) {
                  var e;
                  return "transparent" === t
                    ? "rgba(0,0,0,0)"
                    : (((e = document.createElement("span")).style.cssText =
                        "position:absolute;visibility:hidden;color:" + t),
                      document.body.appendChild(e),
                      requestAnimationFrame(function () {
                        e.parentNode.removeChild(e);
                      }),
                      window.getComputedStyle(e).getPropertyValue("color"));
                },
          oe = function (t) {
            return t
              .split(";")
              .filter(function (t) {
                return t.trim().length;
              })
              .map(gt)
              .filter(function (t) {
                return null !== t;
              })
              .reduce(function (t, e) {
                return (t[e.property] = e.value), t;
              }, {});
          },
          ae = function (t) {
            return t;
          },
          ue =
            (t(s.EASING_FUNCTION, {
              "ease-linear": ae,
              "ease-in-sine": function (t) {
                return -1 * Math.cos(t * (Math.PI / 2)) + 1;
              },
              "ease-out-sine": function (t) {
                return Math.sin(t * (Math.PI / 2));
              },
              "ease-in-out-sine": function (t) {
                return -0.5 * (Math.cos(Math.PI * t) - 1);
              },
              "ease-in-cubic": function (t) {
                return t * t * t;
              },
              "ease-out-cubic": function (t) {
                t -= 1;
                return t * t * t + 1;
              },
              "ease-in-out-cubic": function (t) {
                return t < 0.5
                  ? 4 * t * t * t
                  : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
              },
              "ease-in-circ": function (t) {
                return -1 * (Math.sqrt(1 - +t * t) - 1);
              },
              "ease-out-circ": function (t) {
                t -= 1;
                return Math.sqrt(1 - t * t);
              },
              "ease-in-out-circ": function (t) {
                var t = 2 * t,
                  e = t - 2;
                return t < 1
                  ? -0.5 * (Math.sqrt(1 - t * t) - 1)
                  : 0.5 * (Math.sqrt(1 - e * e) + 1);
              },
              "ease-in-quad": function (t) {
                return t * t;
              },
              "ease-out-quad": function (t) {
                return t * (2 - t);
              },
              "ease-in-out-quad": b,
              "ease-in-quart": function (t) {
                return t * t * t * t;
              },
              "ease-out-quart": function (t) {
                return 1 - --t * t * t * t;
              },
              "ease-in-out-quart": function (t) {
                return t < 0.5 ? 8 * t * t * t * t : 1 - 8 * --t * t * t * t;
              },
              "ease-in-expo": function (t) {
                return 0 === t ? 0 : Math.pow(2, 10 * (t - 1));
              },
              "ease-out-expo": function (t) {
                return 1 === t ? 1 : 1 - Math.pow(2, -10 * t);
              },
              "ease-in-out-expo": function (t) {
                var e;
                return 0 === t || 1 === t
                  ? t
                  : ((e = (t = 2 * t) - 1),
                    t < 1
                      ? 0.5 * Math.pow(2, 10 * e)
                      : 0.5 * (2 - Math.pow(2, -10 * e)));
              },
              "ease-in-back": function (t) {
                var e =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 1.70158,
                  t = +t;
                return t * t * ((e + 1) * t - e);
              },
              "ease-out-back": function (t) {
                var e =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 1.70158,
                  t = +t - 1;
                return t * t * ((e + 1) * t + e) + 1;
              },
              "ease-in-out-back": function (t) {
                var t = 2 * t,
                  e = t - 2,
                  n =
                    1.525 *
                    (1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 1.70158);
                return t < 1
                  ? 0.5 * t * t * ((1 + n) * t - n)
                  : 0.5 * (e * e * ((1 + n) * e + n) + 2);
              },
              "ease-out-elastic": function (t) {
                var e =
                    1 -
                    (1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 0.7),
                  n = 2 * t;
                return 0 === t || 1 === t
                  ? t
                  : ((t = (e / (2 * Math.PI)) * Math.asin(1)),
                    Math.pow(2, -10 * n) *
                      Math.sin(((n - t) * (2 * Math.PI)) / e) +
                      1);
              },
              "ease-out-bounce": function (t) {
                var e,
                  t = +t;
                return t < 1 / 2.75
                  ? 7.5625 * t * t
                  : t < 2 / 2.75
                  ? 7.5625 * (e = t - 1.5 / 2.75) * e + 0.75
                  : t < 2.5 / 2.75
                  ? 7.5625 * (e = t - 2.25 / 2.75) * e + 0.9375
                  : 7.5625 * (e = t - 2.625 / 2.75) * e + 0.984375;
              },
            }),
            function (n) {
              var r,
                i,
                o,
                a =
                  1 < arguments.length && arguments[1] !== pn
                    ? arguments[1]
                    : null,
                u =
                  2 < arguments.length && arguments[2] !== pn
                    ? arguments[2]
                    : 500,
                l =
                  3 < arguments.length && arguments[3] !== pn
                    ? arguments[3]
                    : 0;
              return n
                ? ((i = void 0),
                  (o = r = null),
                  (function t(e) {
                    if ((i = e - (r = null === r ? e : r) - l) < u)
                      return (
                        n(0 <= i ? i / u : 0),
                        (o = requestAnimationFrame(t)),
                        null
                      );
                    n(1), a && a();
                  })(Gt()),
                  function () {
                    cancelAnimationFrame(o);
                  })
                : null;
            }),
          le = function () {
            function a() {
              cancelAnimationFrame(u);
            }
            var u = null,
              l = { velocity: 0, origin: 0, position: 0, destination: 1 };
            return {
              getPosition: function () {
                return l.position;
              },
              cancel: a,
              translate: function (r, t, e, i) {
                a(),
                  null === e
                    ? (l.destination = t)
                    : ((l.position = t), (l.destination = e), (l.velocity = 0)),
                  (l.origin = l.position);
                var o = null;
                (function t(e) {
                  u = requestAnimationFrame(t);
                  var n = e - (o = o || e);
                  n <= 1e3 / 24 ||
                    ((o = e - (n % (1e3 / 24))), i(l, a), r(l.position));
                })(Gt());
              },
            };
          },
          se = function (e) {
            var i =
                1 < arguments.length && arguments[1] !== pn ? arguments[1] : 1,
              o =
                2 < arguments.length && arguments[2] !== pn
                  ? arguments[2]
                  : 0.01;
            return function (t) {
              e(
                t,
                1 < arguments.length && arguments[1] !== pn
                  ? arguments[1]
                  : null,
                2 < arguments.length && arguments[2] !== pn
                  ? arguments[2]
                  : null,
                function (t, e) {
                  var n = t.destination - t.position,
                    r = t.origin + 0.5 * (t.destination - t.origin);
                  (t.velocity += 2 * (-(r - t.origin) + n) * o),
                    (t.position +=
                      t.velocity < 0
                        ? Math.max(t.velocity, -i)
                        : Math.min(t.velocity, i)),
                    ((t.origin < t.destination &&
                      t.position >= t.destination) ||
                      (t.origin >= t.destination &&
                        t.position <= t.destination)) &&
                      (e(), (t.velocity = 0), (t.position = t.destination));
                }
              );
            };
          },
          ce = function (e) {
            var n =
              1 < arguments.length && arguments[1] !== pn ? arguments[1] : 0.01;
            return function (t) {
              e(
                t,
                1 < arguments.length && arguments[1] !== pn
                  ? arguments[1]
                  : null,
                2 < arguments.length && arguments[2] !== pn
                  ? arguments[2]
                  : null,
                function (t, e) {
                  (t.velocity = n),
                    (t.position += t.velocity),
                    ((t.origin < t.destination &&
                      t.position >= t.destination) ||
                      (t.origin >= t.destination &&
                        t.position <= t.destination)) &&
                      (e(), (t.velocity = 0), (t.position = t.destination));
                }
              );
            };
          },
          fe = function (e) {
            var r =
                1 < arguments.length && arguments[1] !== pn
                  ? arguments[1]
                  : 0.5,
              i =
                2 < arguments.length && arguments[2] !== pn
                  ? arguments[2]
                  : 0.75,
              o =
                3 < arguments.length && arguments[3] !== pn ? arguments[3] : 10;
            return function (t) {
              e(
                t,
                1 < arguments.length && arguments[1] !== pn
                  ? arguments[1]
                  : null,
                2 < arguments.length && arguments[2] !== pn
                  ? arguments[2]
                  : null,
                function (t, e) {
                  var n = -(t.position - t.destination) * r;
                  (t.velocity += n / o),
                    (t.position += t.velocity),
                    (t.velocity *= i),
                    bt(t.position, t.destination, t.velocity) &&
                      (e(), (t.position = t.destination), (t.velocity = 0));
                }
              );
            };
          },
          de = function (t) {
            var o = t.map(function (t) {
              return At(
                _t(t.name, t.parameters, t.ease),
                t.origin,
                t.duration,
                t.delay
              );
            });
            return function (e, n, r) {
              if (!st(e)) return !1;
              var i = o.length;
              o.forEach(function (t) {
                t(e, n, function () {
                  !--i && r && r(e);
                });
              });
            };
          },
          he = function (t) {
            return c(s.TRANSITION, t.name).apply(pn, M(t.parameters || []));
          },
          pe = function (t) {
            t = Re(t);
            return (t.duration = 0), (t.delay = 0), t;
          },
          me =
            (t(s.VIEW, {
              text: function () {
                return function (t) {
                  var e = {};
                  return Object.assign(
                    {},
                    u(e, t, "text"),
                    d(e),
                    l(e, function (t) {
                      var e, n;
                      t.root.setAttribute("data-value", t.value),
                        (e = t.root),
                        (t = t.value),
                        (n = e.childNodes[0])
                          ? t !== n.nodeValue && (n.nodeValue = t)
                          : ((n = document.createTextNode(t)),
                            e.appendChild(n));
                    }),
                    a(e)
                  );
                };
              },
            }),
            function (t, e, n) {
              return (
                (e = e),
                (n = n),
                (i = { valueMapping: null }),
                (t = t) &&
                  t.dataset.valueMapping &&
                  ((r = t.dataset.valueMapping),
                  (i.valueMapping =
                    -1 !== ["none", "indexes"].indexOf(r) ? r : null)),
                Object.assign(
                  {},
                  u(i, t),
                  X(i),
                  d(i),
                  H(i, e),
                  l(i, rt, it, n),
                  a(i)
                )
              );
              var r, i;
            }),
          ve = function (t, e, n) {
            return (
              (e = e),
              (n = n),
              (r = { definitions: [] }),
              Object.assign({}, u(r, t), d(r), H(r, e), l(r, ot, at, n), a(r))
            );
            var r;
          },
          ge = function (t, e, n) {
            t = c(s.VIEW, t);
            return t
              ? t({
                  Extension: { Type: s, getExtension: c },
                  Utils: { toPixels: re, toColor: ie },
                  Canvas: {
                    clear: Nt,
                    getDevicePixelRatio: Dt,
                    getBackingStoreRatio: Ct,
                  },
                  DOM: { visible: ct, create: ut, transform: y },
                  Animation: { animate: yt },
                  Data: { request: Wt },
                  Date: { performance: Gt },
                  View: {
                    rooter: u,
                    drawer: l,
                    updater: d,
                    styler: Et,
                    grouper: H,
                    resizer: X,
                    destroyer: a,
                  },
                })(e, n)
              : null;
          },
          ye = function (t) {
            var e = [];
            return Tt(0, t, e), e;
          },
          we = function (t) {
            return t.children && t.children.length;
          },
          be = {
            root: null,
            key: null,
            view: null,
            overlay: null,
            presenter: null,
            transform: null,
            layout: null,
            style: null,
            repeat: null,
            children: null,
            className: null,
          },
          _e = function n(t, r) {
            t = t.map(function (t) {
              var e = t.shift(),
                e =
                  c(s.TRANSFORM, e) ||
                  function (t, e, n) {
                    e(t);
                  },
                t = t.map(function (t) {
                  return Array.isArray(t)
                    ? "string" == typeof t[0]
                      ? n([t], r)
                      : n(t, r)
                    : Te(t);
                });
              return e.apply(pn, M(t));
            });
            return Ut.apply(pn, [r].concat(M(t)));
          },
          Ae = function (t) {
            var e = t.match(/[a-z]+/)[0];
            return { name: e, parameters: Ee(t.substring(e.length)) };
          },
          Ee = function (t) {
            return (
              t.match(/('.+?')|(".+?")|(\[.+?])|([.:\-\d\sa-zA-Z]+%?)/g) || []
            )
              .map(xe)
              .filter(function (t) {
                return t.length;
              })
              .map(Te);
          },
          Me = function (t) {
            return t.substring(1, t.length - 1);
          },
          Ce = /^([\d]{4}-[\d]{1,2}-[\d]{1,2})/,
          De = /^(true|false)$/,
          Ne = /^[\a-zA-Z]+$/,
          ke = /^0[\d]+/,
          Ie = /^('|")/,
          Oe = /^-?(?:\d+)?(?:\.|0\.)?[\d]+$/,
          Se = /^(\[)/,
          Te = function (t) {
            return De.test(t)
              ? "true" === t
              : Se.test(t)
              ? Ee(Me(t))
              : Ce.test(t)
              ? h(t)
              : Ie.test(t)
              ? Me(t)
              : !Ne.test(t) && !ke.test(t) && Oe.test(t)
              ? parseFloat(t)
              : t;
          },
          k = function (t) {
            var e =
                1 < arguments.length && arguments[1] !== pn ? arguments[1] : {},
              n = void 0,
              r = {};
            for (n in t)
              t.hasOwnProperty(n) && (r[n] = (void 0 === e[n] ? t : e)[n]);
            return r;
          },
          xe = function (t) {
            return t.trim();
          },
          Re = function (t) {
            return "object" === (void 0 === t ? "undefined" : et(t)) &&
              null !== t
              ? JSON.parse(JSON.stringify(t))
              : t;
          },
          Le = function (t) {
            return t.slice();
          },
          Fe = function (t, e) {
            return Ue(t) ? Pe(t, e) : Array.isArray(t) ? ze(t, e) : t === e;
          },
          Ue = function (t) {
            return t === Object(t);
          },
          Pe = function (t, e) {
            for (var n in t)
              if (!e.hasOwnProperty(n) || t[n] !== e[n]) return !1;
            return !0;
          },
          ze = function (t, n) {
            return (
              t.length == n.length &&
              t.every(function (t, e) {
                return t === n[e];
              })
            );
          },
          je = function (t) {
            return Object.keys(t)
              .map(function (t) {
                return '"' + t + '"';
              })
              .join(", ");
          },
          qe =
            (i(
              Ge,
              [
                {
                  key: "isRootElement",
                  value: function (t) {
                    return this._element === t;
                  },
                },
                {
                  key: "setConstant",
                  value: function (t, e) {
                    this._constants[t] = e;
                  },
                },
                {
                  key: "getConstants",
                  value: function () {
                    return this._constants;
                  },
                },
                {
                  key: "getConstant",
                  value: function (t) {
                    return this._constants[t];
                  },
                },
                {
                  key: "setPreset",
                  value: function (t, e) {
                    this._presets[t] = e;
                  },
                },
                {
                  key: "getPreset",
                  value: function (t) {
                    return this._presets[t];
                  },
                },
                {
                  key: "destroy",
                  value: function () {
                    this._willDestroy(this),
                      this._observer.disconnect(),
                      this.baseDefinition.presenter.destroy(),
                      this._didDestroy(this);
                  },
                },
                {
                  key: "redraw",
                  value: function () {
                    this.baseDefinition &&
                      this.baseDefinition.presenter &&
                      (this.baseDefinition.presenter.reset(),
                      this.baseDefinition.presenter.draw(),
                      this._updater(this.baseDefinition, this._value));
                  },
                },
                {
                  key: "_init",
                  value: function () {
                    var t,
                      e = this;
                    (this._viewDefinition = this._options.view),
                      (this._willDestroy = this._options.willDestroy),
                      (this._didDestroy = this._options.didDestroy),
                      (this._didInit = this._options.didInit),
                      (this._didUpdate = this._options.didUpdate),
                      (this._value = this._options.value),
                      (this._presets = this._options.presets),
                      (this._constants = this._options.constants),
                      (this._credits = this._options.credits),
                      this._element.classList.contains("tick") ||
                        this._element.classList.add("tick"),
                      (this._observer = lt(
                        this._element,
                        ["data-value"],
                        function (t) {
                          e.value = t;
                        }
                      )),
                      this._viewDefinition.root !== this._element &&
                        (Array.from(this._viewDefinition.root.children).forEach(
                          function (t) {
                            e._element.appendChild(t);
                          }
                        ),
                        (this._viewDefinition.root = this._element)),
                      this._viewDefinition.view ||
                        this._viewDefinition.children ||
                        (this._viewDefinition.view = "text"),
                      (this._updater = Ft(this)),
                      null !== this.value && this._update(this.value),
                      (this._element.dataset.state = "initialised"),
                      this._didInit(this, this.value),
                      this._credits &&
                        (((t = document.createElement("a")).className =
                          "tick-credits"),
                        (t.href = this._credits.url),
                        (t.tabindex = -1),
                        (t.target = "_blank"),
                        (t.rel = "noopener noreferrer"),
                        (t.textContent = this._credits.label),
                        this._element.appendChild(t));
                  },
                },
                {
                  key: "_update",
                  value: function (t) {
                    this._updater(this.baseDefinition, t),
                      this._didUpdate(this, t);
                  },
                },
                {
                  key: "baseDefinition",
                  get: function () {
                    return this._viewDefinition;
                  },
                },
                {
                  key: "root",
                  get: function () {
                    return this._element;
                  },
                },
                {
                  key: "value",
                  get: function () {
                    return this._value;
                  },
                  set: function (t) {
                    (this._value = "string" == typeof t ? jt(t) : t),
                      this._update(t);
                  },
                },
              ],
              [
                {
                  key: "options",
                  value: function () {
                    return {
                      constants: nn(),
                      presets: rn(),
                      value: null,
                      view: null,
                      didInit: function (t) {},
                      didUpdate: function (t, e) {},
                      willDestroy: function (t) {},
                      didDestroy: function (t) {},
                      credits: { label: "", url: "" },
                    };
                  },
                },
              ]
            ),
            Ge);
        function Ge() {
          var t =
              0 < arguments.length && arguments[0] !== pn ? arguments[0] : {},
            e =
              1 < arguments.length && arguments[1] !== pn
                ? arguments[1]
                : document.createElement("div"),
            n = this,
            r = Ge;
          if (!(n instanceof r))
            throw new TypeError("Cannot call a class as a function");
          (this._options = k(Ge.options(), t)),
            (this._element = e),
            (this._value = null),
            (this._observer = null),
            (this._viewDefinition = null),
            (this._constants = null),
            (this._presets = null),
            (this._updater = null),
            (this._credits = null),
            (this._didInit = null),
            (this._didDestroy = null),
            (this._willDestroy = null),
            (this._didUpdate = null),
            this._init();
        }
        function I(t, e, n, r) {
          return { label: 1 === t ? e : n, progress: t / r, value: t };
        }
        function We(e) {
          var t = x.filter(function (t) {
            return t.isRootElement(e);
          });
          return t ? t[0] : null;
        }
        function Ye() {
          var t =
              0 < arguments.length && arguments[0] !== pn ? arguments[0] : pn,
            e = 1 < arguments.length && arguments[1] !== pn ? arguments[1] : pn;
          if ((t && !st(t) && ((e = t), (t = pn)), !t || !We(t)))
            return (
              e && e.view && (e.view = Lt([e.view])[0]),
              !e &&
                t &&
                (e = (function (t) {
                  var e,
                    n,
                    r,
                    i =
                      1 < arguments.length && arguments[1] !== pn
                        ? arguments[1]
                        : {},
                    o =
                      2 < arguments.length && arguments[2] !== pn
                        ? arguments[2]
                        : {},
                    a = t.dataset,
                    u = { meta: {} };
                  for (e in a)
                    a.hasOwnProperty(e) &&
                      ((n = i[e]),
                      (r = a[e]),
                      n &&
                        ((r = null === (r = n(r)) ? Re(o[e]) : r), (u[e] = r)));
                  return "false" === a.credits && (u.credits = !1), u;
                })(
                  t,
                  un,
                  E({}, qe.options(), {
                    constants: E({}, on),
                    presets: E({}, an),
                  })
                )),
              t && !(e = e || {}).view && (e.view = Rt([t])[0]),
              (e = new qe(e, t)),
              x.push(e),
              e
            );
        }
        function Ve(t) {
          return function () {
            setTimeout(t, 0);
          };
        }
        function O() {
          return Date.now();
        }
        function S(r) {
          function i() {
            var t = O(),
              e = s - t,
              n = u + e;
            (s = t + n), r(t - c - d + e), (m = setTimeout(i, n));
          }
          function t() {
            v()
              ? n()
              : g() ||
                ((c = O()),
                setTimeout(function () {
                  r(0);
                }, 0),
                (s = O() + u),
                _(),
                y()
                  ? o()
                  : (m = setTimeout(function () {
                      i();
                    }, u)));
          }
          function e() {
            clearTimeout(m), (d = 0), (h = f = s = c = m = null), (p = !1), a();
          }
          function n() {
            v() && g() && !y() && ((p = !1), _(), b());
          }
          function o() {
            w();
          }
          function a() {
            document.removeEventListener("visibilitychange", A);
          }
          var u =
              1 < arguments.length && arguments[1] !== pn ? arguments[1] : 1e3,
            l = k(
              { autostart: !0 },
              2 < arguments.length && arguments[2] !== pn ? arguments[2] : {}
            ),
            s = null,
            c = null,
            f = null,
            d = 0,
            h = null,
            p = !1,
            m = null,
            v = function () {
              return p;
            },
            g = function () {
              return null !== c;
            },
            y = function () {
              return document.hidden;
            },
            w = function () {
              clearTimeout(m), (f = O()), (h = s - f);
            },
            b = function () {
              (d += O() - f),
                (f = null),
                (s = O() + h),
                (m = setTimeout(function () {
                  i();
                }, h));
            },
            _ = function () {
              document.addEventListener("visibilitychange", A);
            },
            A = function () {
              y() ? o() : g() && b();
            };
          return (
            l.autostart && t(),
            {
              start: t,
              stop: Ve(e),
              reset: Ve(function () {
                e(), t();
              }),
              pause: Ve(function () {
                g() && !y() && ((p = !0), a(), w());
              }),
              resume: n,
            }
          );
        }
        function Be(r, e) {
          var n,
            t = e.match(
              /(?:mon|tues|wednes|thurs|fri|satur|sun)day|at\s[\d]+(?::[\d]+)?(?::[\d]+)?/g
            ),
            i =
              (1 < t.length &&
                ((n = ""),
                t.forEach(function (t) {
                  n = e.split(t)[1] || "";
                }),
                (i = n.trim().match(/wait\s[\d]+\s[a-z]+/)) && t.push(i[0])),
              t.reduce(
                function (t, e) {
                  var n;
                  return (
                    /(?:mon|tues|wednes|thurs|fri|satur|sun)day/.test(e) &&
                      (t.day = Z[(n = e).charAt(0).toUpperCase() + n.slice(1)]),
                    /^at/.test(e)
                      ? (t.time = L(v(r), e.substring(3)))
                      : /wait/.test(e) && (t.idle = R(e.substring(5))),
                    t
                  );
                },
                {
                  idle: null,
                  day: null,
                  time: null,
                  date: null,
                  dist: null,
                  wait: !1,
                }
              ));
          if (i.time) {
            i.time = z(i.time, i.day);
            t = i.time - r;
            if (
              (t < 0 &&
                (i.time.setDate(i.time.getDate() + 7), (t = i.time - r)),
              null !== i.idle && t >= p.Week - i.idle)
            )
              return (i.wait = !0), i;
            i.dist = t;
          } else {
            i.time = z(v(r), i.day);
            t = He(i.time, e);
            if (t.wait) return i;
            i.time = v(G(r, i.time) && t.date ? t.date : t.from);
            t = i.time - r;
            t < 0 && i.time.setDate(i.time.getDate() + 7), (i.dist = t);
          }
          return (i.date = v(i.time)), i;
        }
        function He(t, e) {
          var n,
            e = e
              .match(
                /((?:[\d]+\s)?(?:hours|hour|minutes|minute|seconds|second))|((?:from|till)\s[\d]+(?::[\d]+)?(?::[\d]+)?)|(wait\s[\d]+\s[a-z]+)/g
              )
              .reduce(
                function (t, e) {
                  return (
                    /from/.test(e)
                      ? (t.from = L(t.from, e.split(" ")[1]))
                      : /till/.test(e)
                      ? (t.till = L(t.till, e.split(" ")[1]))
                      : /wait/.test(e)
                      ? (t.idle = R(e.substring(5)))
                      : /hours|hour|minutes|minute|seconds|second/.test(e) &&
                        (t.interval = R(e)),
                    t
                  );
                },
                {
                  idle: null,
                  interval: null,
                  date: null,
                  dist: null,
                  wait: !1,
                  from: L(v(t), "0"),
                  till: L(v(t), "23:59:59:999"),
                }
              );
          return (
            t < e.from ||
              t >= e.till ||
              e.interval > e.till - e.from ||
              ((n = t - e.from),
              (n = e.interval - (n % e.interval)),
              null !== e.idle && n >= e.interval - e.idle
                ? (e.wait = !0)
                : ((e.dist = n), (e.date = new Date(t.getTime() + e.dist)))),
            e
          );
        }
        function $e(t, e) {
          !0 === t
            ? K(function (t) {
                e(t.getTime() - Q().getTime());
              })
            : "string" == typeof t
            ? setTimeout(function () {
                e(h(t).getTime() - Q().getTime());
              }, 0)
            : setTimeout(function () {
                e(0);
              }, 0);
        }
        function Xe(t) {
          return E(
            {
              complete: !1,
              offset: null,
              value: null,
              timer: null,
              onload: function () {},
              onupdate: function (t) {},
            },
            t
          );
        }
        function Ze() {
          var n =
              0 < arguments.length && arguments[0] !== pn ? arguments[0] : 0,
            r =
              1 < arguments.length && arguments[1] !== pn ? arguments[1] : 100;
          return function (t, e) {
            return e((parseFloat(t) - n) / (r - n));
          };
        }
        function Je(t, e, n, r) {
          (e[t] = n[t]), r(Le(e));
        }
        function Ke(t, e, n) {
          return t + (e - t) * n;
        }
        var Qe,
          tn,
          T,
          en,
          x = [],
          nn = function () {
            return on;
          },
          rn = function () {
            return an;
          },
          on = {
            YEAR_PLURAL: "سنة",
            YEAR_SINGULAR: "سنة",
            MONTH_PLURAL: "شهر",
            MONTH_SINGULAR: "شهر",
            WEEK_PLURAL: "اسبوع",
            WEEK_SINGULAR: "اسبوع",
            DAY_PLURAL: "يوم",
            DAY_SINGULAR: "يوم",
            HOUR_PLURAL: "ساعة",
            HOUR_SINGULAR: "ساعة",
            MINUTE_PLURAL: "دقيقة",
            MINUTE_SINGULAR: "دقيقة",
            SECOND_PLURAL: "ثانية",
            SECOND_SINGULAR: "ثانية",
            MILLISECOND_PLURAL: "Milliseconds",
            MILLISECOND_SINGULAR: "Millisecond",
          },
          an = {
            y: function (t, e) {
              return I(t, e.YEAR_SINGULAR, e.YEAR_PLURAL, 10);
            },
            M: function (t, e) {
              return I(t, e.MONTH_SINGULAR, e.MONTH_PLURAL, 12);
            },
            w: function (t, e) {
              return I(t, e.WEEK_SINGULAR, e.WEEK_PLURAL, 52);
            },
            d: function (t, e) {
              return I(t, e.DAY_SINGULAR, e.DAY_PLURAL, 365);
            },
            h: function (t, e) {
              return I(t, e.HOUR_SINGULAR, e.HOUR_PLURAL, 24);
            },
            m: function (t, e) {
              return I(t, e.MINUTE_SINGULAR, e.MINUTE_PLURAL, 60);
            },
            s: function (t, e) {
              return I(t, e.SECOND_SINGULAR, e.SECOND_PLURAL, 60);
            },
            mi: function (t, e) {
              return I(t, e.MILLISECOND_SINGULAR, e.MILLISECOND_PLURAL, 1e3);
            },
          },
          un = {
            value: jt,
            didInit: A,
            didUpdate: A,
            didDestroy: A,
            willDestroy: A,
          },
          R = function (t) {
            t = (t = /^[\d]+/.test(t) ? t : "1 " + t).split(" ");
            return parseFloat(t[0]) * p[t[1].toLowerCase()];
          },
          L = function (t, e) {
            return (
              (t = t),
              (e = e.split(":").map(qt)),
              t.setHours(e[0] || 0, e[1] || 0, e[2] || 0, e[3] || 0),
              t
            );
          },
          ln = function (t, e) {
            if (
              /januari|februari|march|april|may|june|july|august|september|october|november|december/.test(
                e
              )
            ) {
              var n,
                r = t,
                i = e,
                o =
                  (1 <
                    (a = i.match(
                      /januari|februari|march|april|may|june|july|august|september|october|november|december|[\d]+th|\dst|\dnd|first|last|at\s[\d]+(?::[\d]+)?(?::[\d]+)?/g
                    )).length &&
                    ((n = ""),
                    a.forEach(function (t) {
                      n = i.split(t)[1] || "";
                    }),
                    (o = n.trim().match(/wait\s[\d]+\s[a-z]+/)) &&
                      a.push(o[0])),
                  a.reduce(
                    function (t, e) {
                      return (
                        /([\d]+th|\dst|\dnd|first|last)/.test(e) &&
                          (t.day = /^[\d]/.test(e)
                            ? parseInt(e, 10)
                            : "first" === e
                            ? 1
                            : e),
                        /^at/.test(e)
                          ? (t.time = L(v(r), e.substring(3)))
                          : /wait/.test(e)
                          ? (t.idle = R(e.substring(5)))
                          : /^[\a-zA-Z]+$/.test(e) && (t.month = e),
                        t
                      );
                    },
                    {
                      idle: null,
                      day: null,
                      month: null,
                      time: null,
                      date: null,
                      dist: null,
                      wait: !1,
                    }
                  ));
              if (o.time) {
                o.time.setDate(1),
                  (o.time = j(o.time, o.month)),
                  (o.time = m(o.time, o.day));
                var a = o.time - r,
                  u = 0;
                if (
                  (a < 0 &&
                    ((u = a),
                    o.time.setFullYear(o.time.getFullYear() + 1),
                    (a = o.time - r)),
                  null !== o.idle && 0 < u + o.idle)
                )
                  return (o.wait = !0), o;
                o.dist = a;
              } else {
                (o.time = v(r)),
                  o.time.setDate(1),
                  (o.time = j(o.time, o.month)),
                  (o.time = m(o.time, o.day));
                u = He(o.time, i);
                if (u.wait) return o;
                o.time = v(G(r, o.time) && u.date ? u.date : u.from);
                a = o.time - r;
                a < 0 &&
                  ((o.time = v(u.from)),
                  o.time.setFullYear(o.time.getFullYear() + 1),
                  (a = o.time - r)),
                  (o.dist = a);
              }
              return (o.date = v(o.time)), o;
            }
            if (/month/.test(e)) {
              var l,
                s = t,
                c = e,
                f =
                  (1 <
                    (u = c.match(
                      /[\d]+th|\dst|\dnd|first|last|at\s[\d]+(?::[\d]+)?(?::[\d]+)?/g
                    )).length &&
                    ((l = ""),
                    u.forEach(function (t) {
                      l = c.split(t)[1] || "";
                    }),
                    (f = l.trim().match(/wait\s[\d]+\s[a-z]+/)) &&
                      u.push(f[0])),
                  u.reduce(
                    function (t, e) {
                      return (
                        /([\d]+th|\dst|\dnd|first|last)/.test(e) &&
                          (t.day = /^[\d]/.test(e)
                            ? parseInt(e, 10)
                            : "first" === e
                            ? 1
                            : e),
                        /^at/.test(e)
                          ? (t.time = L(v(s), e.substring(3)))
                          : /wait/.test(e) && (t.idle = R(e.substring(5))),
                        t
                      );
                    },
                    {
                      idle: null,
                      day: null,
                      time: null,
                      date: null,
                      dist: null,
                      wait: !1,
                    }
                  ));
              if (f.time) {
                f.time = m(f.time, f.day);
                var u = f.time - s,
                  d = 0;
                if (
                  (u < 0 &&
                    ((d = u),
                    f.time.setDate(1),
                    f.time.setMonth(f.time.getMonth() + 1),
                    m(f.time, f.day),
                    (u = f.time - s)),
                  null !== f.idle && 0 < d + f.idle)
                )
                  return (f.wait = !0), f;
                f.dist = u;
              } else {
                f.time = m(v(s), f.day);
                d = He(f.time, c);
                if (d.wait) return f;
                f.time = v(G(s, f.time) && d.date ? d.date : d.from);
                u = f.time - s;
                u < 0 &&
                  ((f.time = v(d.from)),
                  f.time.setDate(1),
                  f.time.setMonth(f.time.getMonth() + 1),
                  m(f.time, f.day),
                  (u = f.time - s)),
                  (f.dist = u);
              }
              return (f.date = v(f.time)), f;
            }
            var h;
            return /(?:mon|tues|wednes|thurs|fri|satur|sun)day/.test(e)
              ? Be(t, e)
              : /day at/.test(e) || /^at /.test(e)
              ? ((h = t),
                (a = (a = e)
                  .match(/([\d]+(?::[\d]+)?(?::[\d]+)?)|(wait\s[\d]+\s[a-z]+)/g)
                  .reduce(
                    function (t, e) {
                      return (
                        /^[\d]/.test(e)
                          ? (t.time = L(v(h), e))
                          : /wait/.test(e) && (t.idle = R(e.substring(5))),
                        t
                      );
                    },
                    { idle: null, time: null, date: null, wait: !1, dist: null }
                  )),
                (o = a.time - h) < 0 &&
                  (a.time.setDate(a.time.getDate() + 1), (o = a.time - h)),
                null !== a.idle && o >= p.Day - a.idle
                  ? (a.wait = !0)
                  : ((a.dist = o), (a.date = v(a.time))),
                a)
              : /hours|hour|minutes|minute|seconds|second/.test(e)
              ? He(t, e)
              : null;
          },
          sn = {
            format: ["d", "h", "m", "s"],
            cascade: !0,
            server: null,
            interval: 1e3,
          },
          cn =
            (t(s.TRANSFORM, {
              ascii: function () {
                return function (t, e) {
                  return e((t + "").charCodeAt(0));
                };
              },
              char: function (t) {
                var n =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : "",
                  r = t ? new RegExp("[^" + t + "]", "g") : null;
                return function (t, e) {
                  t = String.fromCharCode(t);
                  e((t = r ? t.replace(r, n) : t));
                };
              },
              tween: function (i) {
                var t =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : "ease-linear",
                  o = arguments[2],
                  a = ((i = D(i)), c(s.EASING_FUNCTION, t)),
                  u = null,
                  l = null;
                return function (t, e) {
                  var n, r;
                  (t = parseFloat(t)),
                    u && u(),
                    null === l || t === l
                      ? e((l = t))
                      : ((r = t - (n = l)),
                        (u = yt(
                          function (t) {
                            e(n + t * r);
                          },
                          function () {
                            u = null;
                          },
                          i,
                          a,
                          o
                        )),
                        (l = t));
                };
              },
              value: function (n) {
                return function (t, e) {
                  return e(n);
                };
              },
              input: function () {
                return function (t, e) {
                  return e(t);
                };
              },
              rotate: function () {
                for (var t = arguments.length, a = Array(t), e = 0; e < t; e++)
                  a[e] = arguments[e];
                return function (t, n) {
                  var r = Array.isArray(t) ? t : [t],
                    i = [],
                    o = a.length;
                  r.forEach(function (t, e) {
                    a[e % o](t, function (t) {
                      (i[e] = t), e === r.length - 1 && n(i);
                    });
                  });
                };
              },
              map: function (o) {
                return function (t, n) {
                  var r = [],
                    i = t;
                  i.forEach(function (t, e) {
                    o(t, function (t) {
                      (r[e] = t), e === i.length - 1 && n(r.concat());
                    });
                  });
                };
              },
              transform: function () {
                for (var t = arguments.length, o = Array(t), e = 0; e < t; e++)
                  o[e] = arguments[e];
                return function (t, n) {
                  var r = [],
                    i = t;
                  o.forEach(function (t, e) {
                    t(i, function (t) {
                      (r[e] = t),
                        e === o.length - 1 && n(1 === r.length ? r[0] : r);
                    });
                  });
                };
              },
              upper: function () {
                return function (t, e) {
                  return e((t + "").toUpperCase());
                };
              },
              lower: function () {
                return function (t, e) {
                  return e((t + "").toLowerCase());
                };
              },
              abs: function () {
                return function (t, e) {
                  return e(Math.abs(t));
                };
              },
              add: function (n) {
                return function (t, e) {
                  return e(t + n);
                };
              },
              subtract: function (n) {
                return function (t, e) {
                  return e(t - n);
                };
              },
              modulus: function (n) {
                return function (t, e) {
                  return e(t % n);
                };
              },
              pad: function () {
                var n =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : "",
                  r =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : "left";
                return function (t, e) {
                  return e(
                    n.length > ("" + t).length
                      ? "left" === r
                        ? ("" + n + t).slice(-n.length)
                        : ("" + t + n).substring(0, n.length)
                      : t
                  );
                };
              },
              number: function () {
                var r =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : ".",
                  i =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : ",",
                  n =
                    2 < arguments.length && arguments[2] !== pn
                      ? arguments[2]
                      : 2;
                return function (t, e) {
                  e(
                    (t < 0 ? "-" : "") +
                      parseFloat(Math.abs(t))
                        .toFixed(n)
                        .replace(/./g, function (t, e, n) {
                          return "." === t
                            ? r
                            : e && (n.length - e) % 3 == 0
                            ? i + t
                            : t;
                        })
                  );
                };
              },
              replace: function (n, r) {
                return function (t, e) {
                  return e(
                    (t + "").replace(
                      new RegExp("." === n ? "\\" + n : n, "g"),
                      r
                    )
                  );
                };
              },
              round: function () {
                var n =
                  0 < arguments.length && arguments[0] !== pn
                    ? arguments[0]
                    : 0;
                return function (t, e) {
                  return e(n ? t.toFixed(n) : Math.round(t));
                };
              },
              ceil: function () {
                return function (t, e) {
                  return e(Math.ceil(t));
                };
              },
              floor: function () {
                return function (t, e) {
                  return e(Math.floor(t));
                };
              },
              fraction: Ze,
              percentage: function () {
                var n = Ze(
                  0 < arguments.length && arguments[0] !== pn
                    ? arguments[0]
                    : 0,
                  1 < arguments.length && arguments[1] !== pn
                    ? arguments[1]
                    : 100
                );
                return function (t, e) {
                  n(t, function (t) {
                    e(100 * t);
                  });
                };
              },
              multiply: function (n) {
                return function (t, e) {
                  return e(t * n);
                };
              },
              divide: function (n) {
                return function (t, e) {
                  return e(t / n);
                };
              },
              split: function () {
                var n =
                  0 < arguments.length && arguments[0] !== pn
                    ? arguments[0]
                    : "";
                return function (t, e) {
                  return e((t + "").split(n));
                };
              },
              format: function (n) {
                return function (t, e) {
                  return e(n.replace(/\$0/gi, t));
                };
              },
              plural: function (n, r) {
                return function (t, e) {
                  return e(1 === t ? n : r);
                };
              },
              limit: function () {
                var n =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : 0,
                  r =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 1;
                return function (t, e) {
                  return e(Math.min(Math.max(t, n), r));
                };
              },
              reverse: function () {
                return function (t, e) {
                  return e(
                    Array.isArray(t)
                      ? t.reverse()
                      : (t + "").split("").reverse().join("")
                  );
                };
              },
              arrive: function (n, r) {
                var i =
                    2 < arguments.length && arguments[2] !== pn && arguments[2],
                  o =
                    !(3 < arguments.length && arguments[3] !== pn) ||
                    arguments[3],
                  a = null,
                  u = null,
                  l = null;
                return function (t, e) {
                  (t = parseFloat(t)),
                    null === a
                      ? e((a = t))
                      : (i && null !== u && a === t && (l.cancel(), (l = null)),
                        o && null !== u && 1 < t - l.getPosition()
                          ? (l.cancel(), (u = l = null), e((a = t)))
                          : (l
                              ? l.update(e, t)
                              : (l = wt("arrive", n, r)).update(e, a, t),
                            (u = t)));
                };
              },
              spring: function (n, r, i) {
                var o = null,
                  a = null;
                return function (t, e) {
                  (t = parseFloat(t)),
                    null === o
                      ? e((o = t))
                      : a
                      ? a.update(e, t)
                      : (a = wt("spring", n, r, i)).update(e, o, t);
                };
              },
              delay: function () {
                var u =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : "rtl",
                  l =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 50,
                  s =
                    2 < arguments.length && arguments[2] !== pn
                      ? arguments[2]
                      : 50,
                  c = null;
                return function (e, n) {
                  if (c) {
                    c =
                      "rtl" === u
                        ? c.slice(c.length - e.length, c.length)
                        : c.slice(0, e.length);
                    var r = (function (t) {
                      for (var e = [], n = 0; n < t; n++) e.push(n);
                      return e;
                    })(e.length);
                    if ("random" === u)
                      for (var t = r, i = t.length; i; i--) {
                        var o = Math.floor(Math.random() * i),
                          a = [t[o], t[i - 1]];
                        (t[i - 1] = a[0]), (t[o] = a[1]);
                      }
                    "rtl" === u && r.reverse(),
                      (function t() {
                        Je(r.shift(), c, e, n),
                          r.length &&
                            setTimeout(
                              t,
                              (function () {
                                var t =
                                    0 < arguments.length && arguments[0] !== pn
                                      ? arguments[0]
                                      : 0,
                                  e =
                                    1 < arguments.length && arguments[1] !== pn
                                      ? arguments[1]
                                      : 1;
                                return t + Math.random() * (e - t);
                              })(l, s)
                            );
                      })();
                  } else (c = Le(e)), n(Le(c));
                };
              },
              step: function (n) {
                var r = null,
                  i = null,
                  o = null;
                return function (t, e) {
                  (t = parseFloat(t)),
                    null === r
                      ? e((r = t))
                      : (null !== i && r === t && (o.cancel(), (o = null)),
                        o
                          ? o.update(e, t)
                          : (o = wt("step", n)).update(e, r, t),
                        (i = t));
                };
              },
              keys: function () {
                for (var t = arguments.length, r = Array(t), e = 0; e < t; e++)
                  r[e] = arguments[e];
                return function (t, e) {
                  var n = {};
                  t.forEach(function (t, e) {
                    n[r[e]] = t;
                  }),
                    e(n);
                };
              },
              duration: function () {
                for (var t = arguments.length, n = Array(t), e = 0; e < t; e++)
                  n[e] = arguments[e];
                return function (t, e) {
                  return e(V(t, n));
                };
              },
              substring: function (n, r) {
                return function (t, e) {
                  return e((t + "").substring(n, r));
                };
              },
              preset: function () {
                for (var t = arguments.length, r = Array(t), e = 0; e < t; e++)
                  r[e] = arguments[e];
                return function (t, e, n) {
                  return e(
                    t.map(function (t, e) {
                      return n.getPreset(r[e])(t, n.getConstants(), n);
                    })
                  );
                };
              },
            }),
            { x: "translateX", y: "translateY", z: "translateZ" }),
          fn = { x: "rotateX", y: "rotateY", z: "rotateZ" },
          dn = { both: "scale", x: "scaleX", y: "scaleY" },
          hn =
            (t(s.TRANSITION, {
              fade: function (t, e, n) {
                var r =
                    3 < arguments.length && arguments[3] !== pn
                      ? arguments[3]
                      : b,
                  i =
                    4 < arguments.length && arguments[4] !== pn
                      ? arguments[4]
                      : 0,
                  o =
                    5 < arguments.length && arguments[5] !== pn
                      ? arguments[5]
                      : 1;
                n < 0 && ((i = (n = [o, i])[0]), (o = n[1])),
                  (t.style.opacity = Ke(i, o, r(e)));
              },
              move: function (t, e, n) {
                var r =
                    3 < arguments.length && arguments[3] !== pn
                      ? arguments[3]
                      : b,
                  i =
                    4 < arguments.length && arguments[4] !== pn
                      ? arguments[4]
                      : "0",
                  o =
                    5 < arguments.length && arguments[5] !== pn
                      ? arguments[5]
                      : "100%",
                  a =
                    6 < arguments.length && arguments[6] !== pn
                      ? arguments[6]
                      : "y",
                  n = (n < 0 && ((i = (n = [o, i])[0]), (o = n[1])), w(i, _)),
                  i = w(o, _);
                y(t, cn[a], Ke(n.value, i.value, r(e)), n.units || i.units);
              },
              rotate: function (t, e, n) {
                var r =
                    3 < arguments.length && arguments[3] !== pn
                      ? arguments[3]
                      : b,
                  i =
                    4 < arguments.length && arguments[4] !== pn
                      ? arguments[4]
                      : "0",
                  o =
                    5 < arguments.length && arguments[5] !== pn
                      ? arguments[5]
                      : "90deg",
                  a =
                    6 < arguments.length && arguments[6] !== pn
                      ? arguments[6]
                      : "x",
                  n = (n < 0 && ((i = (n = [o, i])[0]), (o = n[1])), w(i, _)),
                  i = w(o, _);
                y(t, fn[a], Ke(n.value, i.value, r(e)), n.units || i.units);
              },
              scale: function (t, e, n) {
                var r =
                    3 < arguments.length && arguments[3] !== pn
                      ? arguments[3]
                      : b,
                  i =
                    4 < arguments.length && arguments[4] !== pn
                      ? arguments[4]
                      : 0,
                  o =
                    5 < arguments.length && arguments[5] !== pn
                      ? arguments[5]
                      : 1;
                n < 0 && ((i = (n = [o, i])[0]), (o = n[1])),
                  y(
                    t,
                    dn[
                      6 < arguments.length && arguments[6] !== pn
                        ? arguments[6]
                        : "both"
                    ],
                    Ke(i, o, r(e))
                  );
              },
              crossfade: function () {
                var t =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : 1,
                  e = arguments[1],
                  n = arguments[2];
                return {
                  intro: [
                    {
                      name: "fade",
                      parameters: [0, 1],
                      duration: 1e3 * t,
                      delay: D(e),
                    },
                  ],
                  outro: [
                    {
                      name: "fade",
                      parameters: [1, 0],
                      duration: 1e3 * t,
                      delay: D(n),
                    },
                  ],
                };
              },
              swap: function () {
                var t =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : "y",
                  e =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 1,
                  n =
                    2 < arguments.length && arguments[2] !== pn
                      ? arguments[2]
                      : 1,
                  r = arguments[3],
                  i = arguments[4];
                return {
                  intro: [
                    {
                      name: "move",
                      parameters: ["" + 100 * -e, "0%", t],
                      duration: 1e3 * n,
                      delay: D(r),
                    },
                  ],
                  outro: [
                    {
                      name: "move",
                      parameters: ["0%", "" + 100 * e, t],
                      duration: 1e3 * n,
                      delay: D(i),
                    },
                  ],
                };
              },
              revolve: function () {
                var t =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : "y",
                  e =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 1,
                  n =
                    2 < arguments.length && arguments[2] !== pn
                      ? arguments[2]
                      : 1,
                  r = arguments[3],
                  i = arguments[4];
                return {
                  intro: [
                    {
                      name: "rotate",
                      parameters: [90 * -e + "deg", "0deg", t],
                      duration: 1e3 * n,
                      delay: D(r),
                    },
                  ],
                  outro: [
                    {
                      name: "rotate",
                      parameters: ["0deg", 90 * e + "deg", t],
                      duration: 1e3 * n,
                      delay: D(i),
                    },
                  ],
                };
              },
              zoom: function () {
                var t =
                    0 < arguments.length && arguments[0] !== pn
                      ? arguments[0]
                      : 0,
                  e =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : 1,
                  n = arguments[2],
                  r = arguments[3];
                return {
                  intro: [
                    {
                      name: "scale",
                      parameters: [t, 1],
                      duration: 1e3 * e,
                      delay: D(n),
                    },
                  ],
                  outro: [
                    {
                      name: "scale",
                      parameters: [1, t],
                      duration: 1e3 * e,
                      delay: D(r),
                    },
                  ],
                };
              },
            }),
            {
              supported:
                void 0 !== (T = window) &&
                ((i = T.CSS && T.CSS.supports),
                (Qe = !!T.MSInputMethodContext && !!document.documentMode),
                (tn = i && CSS.supports("transform", "translateX(0)")),
                Qe ||
                  (i &&
                    tn &&
                    !!["MutationObserver", "requestAnimationFrame"].filter(
                      function (t) {
                        return t in T;
                      }
                    ).length)),
              options: {
                setConstant: function (t, e) {
                  on[t] = e;
                },
                setPreset: function (t, e) {
                  an[t] = e;
                },
              },
              helper: {
                interval: S,
                date: function (t) {
                  return t ? h(t) : Q();
                },
                duration: B,
              },
              data: {
                request: Wt,
                poll: function (t, e) {
                  return S(
                    function () {
                      Wt(t, e);
                    },
                    2 < arguments.length && arguments[2] !== pn
                      ? arguments[2]
                      : 6e4
                  );
                },
              },
              DOM: {
                create: Ye,
                destroy: function (t) {
                  t = (function (t, e) {
                    for (var n = 0, r = t.length; n < r; n++)
                      if (t[n].isRootElement(e)) return n;
                    return -1;
                  })(x, t);
                  return !(t < 0) && (x[t].destroy(), x.splice(t, 1), !0);
                },
                parse: function (t) {
                  for (
                    var e,
                      n,
                      r = void 0,
                      i = [],
                      r = (n = t.querySelectorAll(".tick:not([data-state])"))
                        .length;
                    r--;

                  )
                    (e = n[r]), i.push(Ye(e));
                  return i;
                },
                find: We,
              },
              count: {
                down: function () {
                  for (
                    var t, e, n = arguments.length, r = Array(n), i = 0;
                    i < n;
                    i++
                  )
                    r[i] = arguments[i];
                  return "number" == typeof r[0] && "string" == typeof r[1]
                    ? ((t = r[0]),
                      (e = r[1].toLowerCase()),
                      r.shift(),
                      (r[0] = B(t, e)),
                      (r[1] = r[1] || {}),
                      (r[1].units = e),
                      function (e) {
                        var n =
                          1 < arguments.length && arguments[1] !== pn
                            ? arguments[1]
                            : {};
                        if ("number" != typeof e)
                          throw 'Can\'t start counter, the "milliseconds" parameter is required';
                        var r,
                          i = (n = k(
                            {
                              units: "ثانية",
                              target: 0,
                              amount: 1e3,
                              interval: 1e3,
                            },
                            n
                          )).target,
                          o = Xe({ target: i, onended: function () {} });
                        return (
                          setTimeout(function () {
                            (o.timer = S(
                              function (t) {
                                (r = e - (t / n.interval) * n.amount) <= i
                                  ? ((o.value = n.target),
                                    o.onupdate(o.value / p[n.units]),
                                    o.timer.stop(),
                                    o.onended())
                                  : ((o.value = r),
                                    o.onupdate(o.value / p[n.units]));
                              },
                              n.interval,
                              { autostart: !1 }
                            )),
                              (o.complete = !0),
                              o.onload(),
                              o.timer.start();
                          }, 0),
                          o
                        );
                      }.apply(pn, r))
                    : "string" == typeof r[0] || o(r[0])
                    ? function (t) {
                        var n =
                          1 < arguments.length && arguments[1] !== pn
                            ? arguments[1]
                            : {};
                        if (void 0 === t)
                          throw 'Can\'t start counter, the "due" parameter is required';
                        var n = k(sn, n),
                          r = o(t) ? t : h(t),
                          i = Xe({ due: v(r), onended: function () {} });
                        return (
                          $e(n.server, function (e) {
                            i.offset = e;
                            (i.timer = S(
                              function () {
                                var t = q(e);
                                r - t <= 0
                                  ? ((i.value = new Array(n.format.length).fill(
                                      0
                                    )),
                                    i.onupdate(i.value),
                                    i.timer.stop(),
                                    i.onended())
                                  : ((i.value = f(t, r, n.format, n.cascade)),
                                    i.onupdate(i.value));
                              },
                              n.interval,
                              { autostart: !1 }
                            )),
                              (i.complete = !0),
                              i.onload(),
                              i.timer.start();
                          }),
                          i
                        );
                      }.apply(pn, r)
                    : null;
                },
                up: function (t) {
                  var n =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : {};
                  if (void 0 === t)
                    throw 'Can\'t start counter, the "since" parameter is required';
                  var n = k(sn, n),
                    r = o(t) ? t : h(t),
                    i = Xe({ since: v(r) });
                  return (
                    $e(n.server, function (e) {
                      i.offset = e;
                      (i.timer = S(
                        function () {
                          var t = q(e);
                          (i.value = f(r, t, n.format, n.cascade)),
                            i.onupdate(i.value);
                        },
                        n.interval,
                        { autostart: !1 }
                      )),
                        (i.complete = !0),
                        i.onload(),
                        i.timer.start();
                    }),
                    i
                  );
                },
                schedule: function (r) {
                  var i =
                    1 < arguments.length && arguments[1] !== pn
                      ? arguments[1]
                      : {};
                  if ("string" != typeof r)
                    throw 'Can\'t start scheduler, "schedule" is a required parameter';
                  var t,
                    e,
                    o,
                    a = (i = k(E({}, sn, { timezone: null }), i)).timezone
                      ? ((t = i.timezone),
                        (e = 6e4 * new Date().getTimezoneOffset()),
                        "Z" === t
                          ? e
                          : ("-" ===
                            (t = t.match(/\+|-|[\d]{2}|[\d]{2}/g)).shift()
                              ? -1
                              : 1) *
                              (36e5 * parseInt(t[0], 10) +
                                6e4 * parseInt(t[1], 10)) +
                            e)
                      : null,
                    u = Xe({
                      waiting: null,
                      nextScheduledDate: null,
                      previouslyScheduledDate: null,
                      onrepeat: function (t, e) {},
                      onresume: function (t) {},
                      onwait: function (t) {},
                    }),
                    l = pn;
                  return (
                    $e(i.server, function (n) {
                      u.offset = n;
                      (u.timer = S(
                        function () {
                          var t,
                            e = q(n);
                          null !== a &&
                            ((t = a), (e = new Date(e.getTime() + t))),
                            (o = (function (e, t) {
                              for (
                                var n = t
                                    .split(",")
                                    .map(xe)
                                    .map(function (t) {
                                      return ln(e, t);
                                    }),
                                  r = null,
                                  i = 0;
                                i < n.length;
                                i++
                              ) {
                                var o = n[i];
                                if (null === r && o.wait) return null;
                                (null === r ||
                                  (null === r.dist && null !== o.dist) ||
                                  (null !== o.dist && o.dist < r.dist)) &&
                                  (r = o);
                              }
                              return r.date;
                            })(e, r)),
                            (u.waiting = null === o),
                            u.waiting
                              ? (l === pn && (l = null),
                                (u.value = new Array(i.format.length).fill(0)),
                                u.nextScheduledDate &&
                                  (u.previouslyScheduledDate = v(
                                    u.nextScheduledDate
                                  )),
                                (u.nextScheduledDate =
                                  null === o ? null : v(o)),
                                u.onwait(
                                  u.previouslyScheduledDate
                                    ? v(u.previouslyScheduledDate)
                                    : null
                                ))
                              : ((u.nextScheduledDate = v(o)),
                                null === l && u.onresume(v(o)),
                                (null === l ||
                                  (l !== pn &&
                                    ((t = o), l.getTime() !== t.getTime()))) &&
                                  (u.onrepeat(v(o), l ? v(l) : null),
                                  l && (u.previouslyScheduledDate = v(l))),
                                (l = v(o)),
                                (u.value = f(e, o, i.format, i.cascade)),
                                u.onupdate(u.value));
                        },
                        i.interval,
                        { autostart: !1 }
                      )),
                        (u.complete = !0),
                        u.onload(),
                        u.timer.start();
                    }),
                    u
                  );
                },
              },
              plugin: {
                add: function (t, e, n) {
                  return "function" == typeof t
                    ? F(t.identifier.type, t.identifier.name, t)
                    : F(t, e, n);
                },
              },
            });
        for (en in s)
          (function (n) {
            if (!s.hasOwnProperty(n)) return;
            hn.plugin[
              ("add-" + s[n]).replace(/-./g, function (t) {
                return t.charAt(1).toUpperCase();
              })
            ] = function (t, e) {
              F(s[n], t, e);
            };
          })(en);
        return (U.exports = hn), U.exports;
      })()).push = r),
      e.forEach(r),
      (t.Tick = n),
      "loading" !== document.readyState
        ? setTimeout(function () {
            i();
          }, 0)
        : document.addEventListener("DOMContentLoaded", i));
  })(window, window.Tick || []);
