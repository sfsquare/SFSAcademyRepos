window.Modernizr = function(t, e, i) {
		function n(t) {
			v.cssText = t
		}

		function o(t, e) {
			return typeof t === e
		}

		function r(t, e) {
			return !!~("" + t).indexOf(e)
		}

		function s(t, e) {
			for (var n in t) {
				var o = t[n];
				if (!r(o, "-") && v[o] !== i) return "pfx" == e ? o : !0
			}
			return !1
		}

		function a(t, e, n) {
			for (var r in t) {
				var s = e[t[r]];
				if (s !== i) return n === !1 ? t[r] : o(s, "function") ? s.bind(n || e) :
					s
			}
			return !1
		}

		function l(t, e, i) {
			var n = t.charAt(0).toUpperCase() + t.slice(1),
				r = (t + " " + T.join(n + " ") + n).split(" ");
			return o(e, "string") || o(e, "undefined") ? s(r, e) : (r = (t + " " + k.join(
				n + " ") + n).split(" "), a(r, e, i))
		}

		function c() {
			f.input = function(i) {
				for (var n = 0, o = i.length; o > n; n++) E[i[n]] = i[n] in b;
				return E.list && (E.list = !!e.createElement("datalist") && !!t.HTMLDataListElement),
					E
			}(
				"autocomplete autofocus list placeholder max min multiple pattern required step"
				.split(" ")), f.inputtypes = function(t) {
				for (var n, o, r, s = 0, a = t.length; a > s; s++) b.setAttribute("type",
					o = t[s]), n = "text" !== b.type, n && (b.value = w, b.style.cssText =
					"position:absolute;visibility:hidden;", /^range$/.test(o) && b.style.WebkitAppearance !==
					i ? (g.appendChild(b), r = e.defaultView, n = r.getComputedStyle &&
						"textfield" !== r.getComputedStyle(b, null).WebkitAppearance && 0 !==
						b.offsetHeight, g.removeChild(b)) : /^(search|tel)$/.test(o) || (n =
						/^(url|email)$/.test(o) ? b.checkValidity && b.checkValidity() === !1 :
						b.value != w)), O[t[s]] = !!n;
				return O
			}(
				"search tel url email datetime date month week time datetime-local number range color"
				.split(" "))
		}
		var u, d, p = "2.8.3",
			f = {},
			h = !0,
			g = e.documentElement,
			m = "modernizr",
			y = e.createElement(m),
			v = y.style,
			b = e.createElement("input"),
			w = ":)",
			x = ({}.toString, " -webkit- -moz- -o- -ms- ".split(" ")),
			S = "Webkit Moz O ms",
			T = S.split(" "),
			k = S.toLowerCase().split(" "),
			C = {
				svg: "http://www.w3.org/2000/svg"
			},
			P = {},
			O = {},
			E = {},
			A = [],
			L = A.slice,
			$ = function(t, i, n, o) {
				var r, s, a, l, c = e.createElement("div"),
					u = e.body,
					d = u || e.createElement("body");
				if (parseInt(n, 10))
					for (; n--;) a = e.createElement("div"), a.id = o ? o[n] : m + (n + 1), c.appendChild(
						a);
				return r = ["&#173;", '<style id="s', m, '">', t, "</style>"].join(""), c.id =
					m, (u ? c : d).innerHTML += r, d.appendChild(c), u || (d.style.background =
						"", d.style.overflow = "hidden", l = g.style.overflow, g.style.overflow =
						"hidden", g.appendChild(d)), s = i(c, t), u ? c.parentNode.removeChild(c) :
					(d.parentNode.removeChild(d), g.style.overflow = l), !!s
			},
			_ = {}.hasOwnProperty;
		d = o(_, "undefined") || o(_.call, "undefined") ? function(t, e) {
			return e in t && o(t.constructor.prototype[e], "undefined")
		} : function(t, e) {
			return _.call(t, e)
		}, Function.prototype.bind || (Function.prototype.bind = function(t) {
			var e = this;
			if ("function" != typeof e) throw new TypeError;
			var i = L.call(arguments, 1),
				n = function() {
					if (this instanceof n) {
						var o = function() {};
						o.prototype = e.prototype;
						var r = new o,
							s = e.apply(r, i.concat(L.call(arguments)));
						return Object(s) === s ? s : r
					}
					return e.apply(t, i.concat(L.call(arguments)))
				};
			return n
		}), P.touch = function() {
			var i;
			return "ontouchstart" in t || t.DocumentTouch && e instanceof DocumentTouch ?
				i = !0 : $(["@media (", x.join("touch-enabled),("), m, ")",
					"{#modernizr{top:9px;position:absolute}}"
				].join(""), function(t) {
					i = 9 === t.offsetTop
				}), i
		}, P.history = function() {
			return !!t.history && !!history.pushState
		}, P.csstransforms = function() {
			return !!l("transform")
		}, P.csstransforms3d = function() {
			var t = !!l("perspective");
			return t && "webkitPerspective" in g.style && $(
				"@media (transform-3d),(-webkit-transform-3d){#modernizr{left:9px;position:absolute;height:3px;}}",
				function(e) {
					t = 9 === e.offsetLeft && 3 === e.offsetHeight
				}), t
		}, P.csstransitions = function() {
			return l("transition")
		}, P.video = function() {
			var t = e.createElement("video"),
				i = !1;
			try {
				(i = !!t.canPlayType) && (i = new Boolean(i), i.ogg = t.canPlayType(
					'video/ogg; codecs="theora"').replace(/^no$/, ""), i.h264 = t.canPlayType(
					'video/mp4; codecs="avc1.42E01E"').replace(/^no$/, ""), i.webm = t.canPlayType(
					'video/webm; codecs="vp8, vorbis"').replace(/^no$/, ""))
			} catch (n) {}
			return i
		}, P.svg = function() {
			return !!e.createElementNS && !!e.createElementNS(C.svg, "svg").createSVGRect
		};
		for (var z in P) d(P, z) && (u = z.toLowerCase(), f[u] = P[z](), A.push((f[u] ?
			"" : "no-") + u));
		return f.input || c(), f.addTest = function(t, e) {
				if ("object" == typeof t)
					for (var n in t) d(t, n) && f.addTest(n, t[n]);
				else {
					if (t = t.toLowerCase(), f[t] !== i) return f;
					e = "function" == typeof e ? e() : e, "undefined" != typeof h && h && (g.className +=
						" " + (e ? "" : "no-") + t), f[t] = e
				}
				return f
			}, n(""), y = b = null, f._version = p, f._prefixes = x, f._domPrefixes = k,
			f._cssomPrefixes = T, f.testProp = function(t) {
				return s([t])
			}, f.testAllProps = l, f.testStyles = $, f.prefixed = function(t, e, i) {
				return e ? l(t, e, i) : l(t, "pfx")
			}, g.className = g.className.replace(/(^|\s)no-js(\s|$)/, "$1$2") + (h ?
				" js " + A.join(" ") : ""), f
	}(this, this.document), Modernizr.addTest("placeholder", function() {
		return "placeholder" in (Modernizr.input || document.createElement("input")) &&
			"placeholder" in (Modernizr.textarea || document.createElement("textarea"))
	}),
	function(t, e) {
		"use strict";

		function i(t) {
			this.callback = t, this.ticking = !1
		}

		function n(e) {
			return e && "undefined" != typeof t && (e === t || e.nodeType)
		}

		function o(t) {
			if (arguments.length <= 0) throw new Error(
				"Missing arguments in extend function");
			var e, i, r = t || {};
			for (i = 1; i < arguments.length; i++) {
				var s = arguments[i] || {};
				for (e in s) r[e] = "object" != typeof r[e] || n(r[e]) ? r[e] || s[e] : o(
					r[e], s[e])
			}
			return r
		}

		function r(t) {
			return t === Object(t) ? t : {
				down: t,
				up: t
			}
		}

		function s(t, e) {
			e = o(e, s.options), this.lastKnownScrollY = 0, this.elem = t, this.debouncer =
				new i(this.update.bind(this)), this.tolerance = r(e.tolerance), this.classes =
				e.classes, this.offset = e.offset, this.scroller = e.scroller, this.initialised = !
				1, this.onPin = e.onPin, this.onUnpin = e.onUnpin, this.onTop = e.onTop,
				this.onNotTop = e.onNotTop
		}
		var a = {
			bind: !! function() {}.bind,
			classList: "classList" in e.documentElement,
			rAF: !!(t.requestAnimationFrame || t.webkitRequestAnimationFrame || t.mozRequestAnimationFrame)
		};
		t.requestAnimationFrame = t.requestAnimationFrame || t.webkitRequestAnimationFrame ||
			t.mozRequestAnimationFrame, i.prototype = {
				constructor: i,
				update: function() {
					this.callback && this.callback(), this.ticking = !1
				},
				requestTick: function() {
					this.ticking || (requestAnimationFrame(this.rafCallback || (this.rafCallback =
						this.update.bind(this))), this.ticking = !0)
				},
				handleEvent: function() {
					this.requestTick()
				}
			}, s.prototype = {
				constructor: s,
				init: function() {
					return s.cutsTheMustard ? (this.elem.classList.add(this.classes.initial),
						setTimeout(this.attachEvent.bind(this), 100), this) : void 0
				},
				destroy: function() {
					var t = this.classes;
					this.initialised = !1, this.elem.classList.remove(t.unpinned, t.pinned, t
						.top, t.initial), this.scroller.removeEventListener("scroll", this.debouncer, !
						1)
				},
				attachEvent: function() {
					this.initialised || (this.lastKnownScrollY = this.getScrollY(), this.initialised = !
						0, this.scroller.addEventListener("scroll", this.debouncer, !1), this.debouncer
						.handleEvent())
				},
				unpin: function() {
					var t = this.elem.classList,
						e = this.classes;
					(t.contains(e.pinned) || !t.contains(e.unpinned)) && (t.add(e.unpinned),
						t.remove(e.pinned), this.onUnpin && this.onUnpin.call(this))
				},
				pin: function() {
					var t = this.elem.classList,
						e = this.classes;
					t.contains(e.unpinned) && (t.remove(e.unpinned), t.add(e.pinned), this.onPin &&
						this.onPin.call(this))
				},
				top: function() {
					var t = this.elem.classList,
						e = this.classes;
					t.contains(e.top) || (t.add(e.top), t.remove(e.notTop), this.onTop &&
						this.onTop.call(this))
				},
				notTop: function() {
					var t = this.elem.classList,
						e = this.classes;
					t.contains(e.notTop) || (t.add(e.notTop), t.remove(e.top), this.onNotTop &&
						this.onNotTop.call(this))
				},
				getScrollY: function() {
					return void 0 !== this.scroller.pageYOffset ? this.scroller.pageYOffset :
						void 0 !== this.scroller.scrollTop ? this.scroller.scrollTop : (e.documentElement ||
							e.body.parentNode || e.body).scrollTop
				},
				getViewportHeight: function() {
					return t.innerHeight || e.documentElement.clientHeight || e.body.clientHeight
				},
				getDocumentHeight: function() {
					var t = e.body,
						i = e.documentElement;
					return Math.max(t.scrollHeight, i.scrollHeight, t.offsetHeight, i.offsetHeight,
						t.clientHeight, i.clientHeight)
				},
				getElementHeight: function(t) {
					return Math.max(t.scrollHeight, t.offsetHeight, t.clientHeight)
				},
				getScrollerHeight: function() {
					return this.scroller === t || this.scroller === e.body ? this.getDocumentHeight() :
						this.getElementHeight(this.scroller)
				},
				isOutOfBounds: function(t) {
					var e = 0 > t,
						i = t + this.getViewportHeight() > this.getScrollerHeight();
					return e || i
				},
				toleranceExceeded: function(t, e) {
					return Math.abs(t - this.lastKnownScrollY) >= this.tolerance[e]
				},
				shouldUnpin: function(t, e) {
					var i = t > this.lastKnownScrollY,
						n = t >= this.offset;
					return i && n && e
				},
				shouldPin: function(t, e) {
					var i = t < this.lastKnownScrollY,
						n = t <= this.offset;
					return i && e || n
				},
				update: function() {
					var t = this.getScrollY(),
						e = t > this.lastKnownScrollY ? "down" : "up",
						i = this.toleranceExceeded(t, e);
					this.isOutOfBounds(t) || (t <= this.offset ? this.top() : this.notTop(),
						this.shouldUnpin(t, i) ? this.unpin() : this.shouldPin(t, i) && this.pin(),
						this.lastKnownScrollY = t)
				}
			}, s.options = {
				tolerance: {
					up: 0,
					down: 0
				},
				offset: 0,
				scroller: t,
				classes: {
					pinned: "headroom--pinned",
					unpinned: "headroom--unpinned",
					top: "headroom--top",
					notTop: "headroom--not-top",
					initial: "headroom"
				}
			}, s.cutsTheMustard = "undefined" != typeof a && a.rAF && a.bind && a.classList,
			t.Headroom = s
	}(window, document),
	function(t) {
		t && (t.fn.headroom = function(e) {
			return this.each(function() {
				var i = t(this),
					n = i.data("headroom"),
					o = "object" == typeof e && e;
				o = t.extend(!0, {}, Headroom.options, o), n || (n = new Headroom(this,
					o), n.init(), i.data("headroom", n)), "string" == typeof e && n[e]()
			})
		}, t("[data-headroom]").each(function() {
			var e = t(this);
			e.headroom(e.data())
		}))
	}(window.Zepto || window.jQuery),
	function(t) {
		var e = {
			sectionContainer: "section",
			easing: "ease",
			animationTime: 1e3,
			pagination: !0,
			updateURL: !1,
			keyboard: !0,
			beforeMove: null,
			afterMove: null,
			loop: !1,
			responsiveFallback: !1,
			responsiveHeightFallback: !1,
			disableMouseMove: !1,
			moveUpKeys: [33, 38],
			moveDownKeys: [34, 40],
			backtoTopKeys: [36],
			gotoBottom: [35]
		};
		t.fn.swipeEvents = function() {
			return this.each(function() {
				function e(t) {
					var e = t.originalEvent.touches;
					e && e.length && (n = e[0].pageX, o = e[0].pageY, r.bind("touchmove",
						i))
				}

				function i(t) {
					var e = t.originalEvent.touches;
					if (e && e.length) {
						var s = n - e[0].pageX,
							a = o - e[0].pageY;
						s >= 50 && r.trigger("swipeLeft"), -50 >= s && r.trigger("swipeRight"),
							a >= 50 && r.trigger("swipeUp"), -50 >= a && r.trigger("swipeDown"), (
								Math.abs(s) >= 50 || Math.abs(a) >= 50) && r.unbind("touchmove", i),
							t.preventDefault()
					}
				}
				var n, o, r = t(this);
				r.bind("touchstart", e)
			})
		}, t.fn.onepage_scroll = function(i) {
			function n() {
				r.responsiveFallback && t(window).width() < r.responsiveFallback || r.responsiveHeightFallback &&
					t(window).height() > r.responsiveHeightFallback ? (t("body").addClass(
						"disabled-onepage-scroll"), t(document).unbind(
						"mousewheel DOMMouseScroll"), s.swipeEvents().unbind(
						"swipeDown swipeUp touchstart touchmove")) : (t("body").hasClass(
						"disabled-onepage-scroll") && (t("body").removeClass(
						"disabled-onepage-scroll"), t("html, body, .wrapper").animate({
						scrollTop: 0
					}, "fast")), r.disableMouseMove || t(document).bind(
						"mousewheel DOMMouseScroll", function(e) {
							current = t(r.sectionContainer + ".active");
							var i = e.originalEvent.wheelDelta || -e.originalEvent.detail;
							(current.height() >= current.find(".page-container").height() || 0 ===
								current.scrollTop() && i >= 0 || current[0].scrollHeight - current.scrollTop() ===
								current.height() && 0 > i) && (e.preventDefault(), o(e, i))
						}))
			}

			function o(t, e) {
				var i = e,
					n = (new Date).getTime();
				return n - u < d + r.animationTime ? void t.preventDefault() : (0 > i ? s
					.moveDown() : s.moveUp(), void(u = n))
			}
			var r = t.extend({}, e, i),
				s = t(this),
				a = t(r.sectionContainer),
				l = a.length,
				c = 0,
				u = 0,
				d = 500,
				p = "";
			if (t(".home-wrap").swipeEvents().bind("swipeDown", function(e) {
					t("body").hasClass("disabled-onepage-scroll") || e.preventDefault(), s.moveUp(),
						console.log("swipe down")
				}).bind("swipeUp", function(e) {
					t("body").hasClass("disabled-onepage-scroll") || e.preventDefault(), s.moveDown(),
						console.log("swipe up")
				}), t.fn.transformPage = function(e, i, n, o, r) {
					t("body").hasClass("menu-open") || t(".home-loading").length || (
						"function" == typeof e.beforeMove && e.beforeMove(n, o), $els = t(this)
						.add(".fg-wrap"), $els.velocity({
							translateZ: 0,
							translateY: i + "%"
						}, {
							mobileHA: !0,
							duration: e.animationTime,
							easing: [.53, .19, .44, .9],
							complete: function() {
								"function" == typeof e.afterMove && e.afterMove(n, o, r)
							}
						}))
				}, t.fn.moveDown = function() {
					if (!t("body").hasClass("menu-open") && !t(".home-loading").length) {
						var e = t(this),
							i = t(r.sectionContainer + ".active").data("index");
						if (current = t(r.sectionContainer + "[data-index='" + i + "']"), next =
							t(r.sectionContainer + "[data-index='" + (i + 1) + "']"), next.length <
							1) {
							if (r.loop !== !0) return;
							pos = 0, next = t(r.sectionContainer + "[data-index='1']")
						} else pos = 100 * i * -1; if (current.removeClass("active"), next.addClass(
								"active"), r.pagination === !0 && (t(
								".onepage-pagination li a[data-index='" + i + "']").removeClass(
								"active"), t(".onepage-pagination li a[data-index='" + next.data(
								"index") + "']").addClass("active")), t("body")[0].className = t(
								"body")[0].className.replace(/\bviewing-page-\d.*?\b/g, ""), t("body")
							.addClass("viewing-page-" + next.data("index")), history.replaceState &&
							r.updateURL === !0) {
							var n = window.location.href.substr(0, window.location.href.indexOf("#")) +
								"#" + (i + 1);
							history.pushState({}, document.title, n)
						}
						e.transformPage(r, pos, next.data("index"), next, "down")
					}
				}, t.fn.moveUp = function() {
					if (!t("body").hasClass("menu-open") && !t(".home-loading").length) {
						var e = t(this),
							i = t(r.sectionContainer + ".active").data("index");
						if (current = t(r.sectionContainer + "[data-index='" + i + "']"), next =
							t(r.sectionContainer + "[data-index='" + (i - 1) + "']"), next.length <
							1) {
							if (r.loop !== !0) return;
							pos = 100 * (l - 1) * -1, next = t(r.sectionContainer + "[data-index='" +
								l + "']")
						} else pos = 100 * (next.data("index") - 1) * -1; if (current.removeClass(
								"active"), next.addClass("active"), r.pagination === !0 && (t(
								".onepage-pagination li a[data-index='" + i + "']").removeClass(
								"active"), t(".onepage-pagination li a[data-index='" + next.data(
								"index") + "']").addClass("active")), t("body")[0].className = t(
								"body")[0].className.replace(/\bviewing-page-\d.*?\b/g, ""), t("body")
							.addClass("viewing-page-" + next.data("index")), history.replaceState &&
							r.updateURL === !0) {
							var n = window.location.href.substr(0, window.location.href.indexOf("#")) +
								"#" + (i - 1);
							history.pushState({}, document.title, n)
						}
						e.transformPage(r, pos, next.data("index"), next, "up")
					}
				}, t.fn.moveTo = function(e) {
					if (current = t(r.sectionContainer + ".active"), next = t(r.sectionContainer +
						"[data-index='" + e + "']"), next.length > 0) {
						if (current.removeClass("active"), next.addClass("active"), t(
								".onepage-pagination li a.active").removeClass("active"), t(
								".onepage-pagination li a[data-index='" + e + "']").addClass("active"),
							t("body")[0].className = t("body")[0].className.replace(
								/\bviewing-page-\d.*?\b/g, ""), t("body").addClass("viewing-page-" +
								next.data("index")), pos = 100 * (e - 1) * -1, history.replaceState &&
							r.updateURL === !0) {
							var i = window.location.href.substr(0, window.location.href.indexOf("#")) +
								"#" + (e - 1);
							history.pushState({}, document.title, i)
						}
						s.transformPage(r, pos, e, next)
					}
				}, t("html").css("overflow", "hidden").css("height", "100%"), t("body").css(
					"overflow", "hidden").css("height", "100%"), s.addClass("onepage-wrapper")
				.css("position", "absolute"), t.each(a, function(e) {
					t(this).addClass("ops-section").attr("data-index", e + 1), c += 100, r.pagination ===
						!0 && (p += "<li><a data-index='" + (e + 1) + "' href='#" + (e + 1) +
							"'></a></li>")
				}), r.pagination === !0 && (t("<ul class='onepage-pagination'>" + p +
						"</ul>").prependTo("body"), posTop = s.find(".onepage-pagination").height() /
					2 * -1, s.find(".onepage-pagination").css("margin-top", posTop)), "" !==
				window.location.hash && "#1" !== window.location.hash && t(r.sectionContainer +
					"[data-index='" + window.location.hash.replace("#", "") + "']").length >
				0) {
				if (init_index = window.location.hash.replace("#", ""), t(r.sectionContainer +
					"[data-index='" + init_index + "']").addClass("active"), t("body").addClass(
					"viewing-page-" + init_index), r.pagination === !0 && t(
					".onepage-pagination li a[data-index='" + init_index + "']").addClass(
					"active"), next = t(r.sectionContainer + "[data-index='" + init_index +
					"']"), next && (next.addClass("active"), r.pagination === !0 && t(
					".onepage-pagination li a[data-index='" + init_index + "']").addClass(
					"active"), t("body")[0].className = t("body")[0].className.replace(
					/\bviewing-page-\d.*?\b/g, ""), t("body").addClass("viewing-page-" +
					next.data("index")), history.replaceState && r.updateURL === !0)) {
					var f = window.location.href.substr(0, window.location.href.indexOf("#")) +
						"#" + init_index;
					history.pushState({}, document.title, f)
				}
				pos = 100 * (init_index - 1) * -1, s.transformPage(r, pos, init_index)
			} else t(r.sectionContainer + "[data-index='1']").addClass("active"), t(
				"body").addClass("viewing-page-1"), r.pagination === !0 && t(
				".onepage-pagination li a[data-index='1']").addClass("active");
			return r.pagination === !0 && t(".onepage-pagination li a").click(function() {
				var e = t(this).data("index");
				s.moveTo(e)
			}), r.disableMouseMove || t(document).bind("mousewheel DOMMouseScroll",
				function(e) {
					current = t(r.sectionContainer + ".active");
					var i = e.originalEvent.wheelDelta || -e.originalEvent.detail;
					(current.height() >= current.find(".page-container").height() || 0 ===
						current.scrollTop() && i >= 0 || current[0].scrollHeight - current.scrollTop() ===
						current.height() && 0 > i) && (e.preventDefault(), t("body").hasClass(
						"disabled-onepage-scroll") || o(e, i))
				}), (r.responsiveFallback || r.responsiveHeightFallback) && (t(window).resize(
				function() {
					n()
				}), n()), r.keyboard === !0 && t(document).keydown(function(e) {
				var i = e.target.tagName.toLowerCase();
				if (!t("body").hasClass("disabled-onepage-scroll") && "input" !== i &&
					"textarea" !== i)
					if (t.inArray(e.which, r.moveUpKeys) > -1) s.moveUp();
					else if (t.inArray(e.which, r.moveDownKeys) > -1) s.moveDown();
				else if (t.inArray(e.which, r.backtoTopKeys) > -1) s.moveTo(1);
				else if (t.inArray(e.which, r.gotoBottom) > -1) {
					var n = a.last().data("index");
					s.moveTo(n)
				}
			}), !1
		}, t.fn.destroy_onepage_scroll = function(i) {
			var n = t.extend({}, e, i),
				o = t(this),
				r = t(n.sectionContainer);
			t("html").css("overflow", "").css("height", ""), t("body").css("overflow",
				"").css("height", ""), o.removeClass("onepage-wrapper"), t.each(r,
				function() {
					t(this).removeClass("ops-section active").removeAttr("data-index").removeData(
						"index")
				}), o.swipeEvents().unbind("swipeDown swipeUp touchstart touchmove"), t(
				"body").removeClass("disabled-onepage-scroll"), t(
				".onepage-pagination li a").unbind("click"), t("ul.onepage-pagination").remove();
			var s = t("body").attr("class").split(/\s+/);
			t.each(s, function(e, i) {
				i.indexOf("viewing-page-") >= 0 && t("body").removeClass(i)
			}), t(document).unbind("mousewheel DOMMouseScroll"), t(window).unbind(
				"resize"), t(document).unbind("keydown")
		}
	}(window.jQuery),
	function(t, e) {
		"function" == typeof define && define.amd ? define([], e) : "object" ==
			typeof exports ? module.exports = e() : t.Layzr = e()
	}(this, function() {
		"use strict";

		function t(t) {
			this._lastScroll = 0, this._ticking = !1, t = t || {}, this._optionsContainer =
				document.querySelector(t.container) || window, this._optionsSelector = t.selector ||
				"[data-layzr]", this._optionsAttr = t.attr || "data-layzr", this._optionsAttrRetina =
				t.retinaAttr || "data-layzr-retina", this._optionsAttrBg = t.bgAttr ||
				"data-layzr-bg", this._optionsAttrHidden = t.hiddenAttr ||
				"data-layzr-hidden", this._optionsThreshold = t.threshold || 0, this._optionsCallback =
				t.callback || null, this._retina = window.devicePixelRatio > 1, this._srcAttr =
				this._retina ? this._optionsAttrRetina : this._optionsAttr, this._nodes =
				document.querySelectorAll(this._optionsSelector), this._handlerBind =
				this._requestScroll.bind(this), this._create()
		}
		return t.prototype._requestScroll = function() {
			this._lastScroll = this._optionsContainer === window ? window.pageYOffset :
				this._optionsContainer.scrollTop + this._getOffset(this._optionsContainer),
				this._requestTick()
		}, t.prototype._requestTick = function() {
			this._ticking || (requestAnimationFrame(this.update.bind(this)), this._ticking = !
				0)
		}, t.prototype._getOffset = function(t) {
			return t.getBoundingClientRect().top + window.pageYOffset
		}, t.prototype._getContainerHeight = function() {
			return this._optionsContainer.innerHeight || this._optionsContainer.offsetHeight
		}, t.prototype._create = function() {
			this._handlerBind(), this._optionsContainer.addEventListener("scroll",
				this._handlerBind, !1), this._optionsContainer.addEventListener("resize",
				this._handlerBind, !1)
		}, t.prototype._destroy = function() {
			this._optionsContainer.removeEventListener("scroll", this._handlerBind, !1),
				this._optionsContainer.removeEventListener("resize", this._handlerBind, !
					1)
		}, t.prototype._inViewport = function(t) {
			var e = this._lastScroll,
				i = e + this._getContainerHeight(),
				n = this._getOffset(t),
				o = n + this._getContainerHeight(),
				r = this._optionsThreshold / 100 * window.innerHeight;
			return o >= e - r && i + r >= n && !t.hasAttribute(this._optionsAttrHidden)
		}, t.prototype._reveal = function(t) {
			var e = t.getAttribute(this._srcAttr) || t.getAttribute(this._optionsAttr);
			t.hasAttribute(this._optionsAttrBg) ? t.style.backgroundImage = "url(" + e +
				")" : t.setAttribute("src", e), "function" == typeof this._optionsCallback &&
				this._optionsCallback.call(t), t.removeAttribute(this._optionsAttr), t.removeAttribute(
					this._optionsAttrRetina), t.removeAttribute(this._optionsAttrBg), t.removeAttribute(
					this._optionsAttrHidden)
		}, t.prototype.updateSelector = function() {
			this._nodes = document.querySelectorAll(this._optionsSelector)
		}, t.prototype.update = function() {
			for (var t = this._nodes.length, e = 0; t > e; e++) {
				var i = this._nodes[e];
				i.hasAttribute(this._optionsAttr) && this._inViewport(i) && this._reveal(
					i)
			}
			this._ticking = !1
		}, t
	}), ! function(t) {
		function e() {}

		function i(t) {
			function i(e) {
				e.prototype.option || (e.prototype.option = function(e) {
					t.isPlainObject(e) && (this.options = t.extend(!0, this.options, e))
				})
			}

			function o(e, i) {
				t.fn[e] = function(o) {
					if ("string" == typeof o) {
						for (var s = n.call(arguments, 1), a = 0, l = this.length; l > a; a++) {
							var c = this[a],
								u = t.data(c, e);
							if (u)
								if (t.isFunction(u[o]) && "_" !== o.charAt(0)) {
									var d = u[o].apply(u, s);
									if (void 0 !== d) return d
								} else r("no such method '" + o + "' for " + e + " instance");
							else r("cannot call methods on " + e +
								" prior to initialization; attempted to call '" + o + "'")
						}
						return this
					}
					return this.each(function() {
						var n = t.data(this, e);
						n ? (n.option(o), n._init()) : (n = new i(this, o), t.data(this, e,
							n))
					})
				}
			}
			if (t) {
				var r = "undefined" == typeof console ? e : function(t) {
					console.error(t)
				};
				return t.bridget = function(t, e) {
					i(e), o(t, e)
				}, t.bridget
			}
		}
		var n = Array.prototype.slice;
		"function" == typeof define && define.amd ? define(
			"jquery-bridget/jquery.bridget", ["jquery"], i) : i("object" == typeof exports ?
			require("jquery") : t.jQuery)
	}(window),
	function(t) {
		function e(e) {
			var i = t.event;
			return i.target = i.target || i.srcElement || e, i
		}
		var i = document.documentElement,
			n = function() {};
		i.addEventListener ? n = function(t, e, i) {
			t.addEventListener(e, i, !1)
		} : i.attachEvent && (n = function(t, i, n) {
			t[i + n] = n.handleEvent ? function() {
				var i = e(t);
				n.handleEvent.call(n, i)
			} : function() {
				var i = e(t);
				n.call(t, i)
			}, t.attachEvent("on" + i, t[i + n])
		});
		var o = function() {};
		i.removeEventListener ? o = function(t, e, i) {
			t.removeEventListener(e, i, !1)
		} : i.detachEvent && (o = function(t, e, i) {
			t.detachEvent("on" + e, t[e + i]);
			try {
				delete t[e + i]
			} catch (n) {
				t[e + i] = void 0
			}
		});
		var r = {
			bind: n,
			unbind: o
		};
		"function" == typeof define && define.amd ? define("eventie/eventie", r) :
			"object" == typeof exports ? module.exports = r : t.eventie = r
	}(this),
	function(t) {
		function e(t) {
			"function" == typeof t && (e.isReady ? t() : s.push(t))
		}

		function i(t) {
			var i = "readystatechange" === t.type && "complete" !== r.readyState;
			e.isReady || i || n()
		}

		function n() {
			e.isReady = !0;
			for (var t = 0, i = s.length; i > t; t++) {
				var n = s[t];
				n()
			}
		}

		function o(o) {
			return "complete" === r.readyState ? n() : (o.bind(r, "DOMContentLoaded", i),
				o.bind(r, "readystatechange", i), o.bind(t, "load", i)), e
		}
		var r = t.document,
			s = [];
		e.isReady = !1, "function" == typeof define && define.amd ? define(
				"doc-ready/doc-ready", ["eventie/eventie"], o) : "object" == typeof exports ?
			module.exports = o(require("eventie")) : t.docReady = o(t.eventie)
	}(window),
	function() {
		function t() {}

		function e(t, e) {
			for (var i = t.length; i--;)
				if (t[i].listener === e) return i;
			return -1
		}

		function i(t) {
			return function() {
				return this[t].apply(this, arguments)
			}
		}
		var n = t.prototype,
			o = this,
			r = o.EventEmitter;
		n.getListeners = function(t) {
				var e, i, n = this._getEvents();
				if (t instanceof RegExp) {
					e = {};
					for (i in n) n.hasOwnProperty(i) && t.test(i) && (e[i] = n[i])
				} else e = n[t] || (n[t] = []);
				return e
			}, n.flattenListeners = function(t) {
				var e, i = [];
				for (e = 0; e < t.length; e += 1) i.push(t[e].listener);
				return i
			}, n.getListenersAsObject = function(t) {
				var e, i = this.getListeners(t);
				return i instanceof Array && (e = {}, e[t] = i), e || i
			}, n.addListener = function(t, i) {
				var n, o = this.getListenersAsObject(t),
					r = "object" == typeof i;
				for (n in o) o.hasOwnProperty(n) && -1 === e(o[n], i) && o[n].push(r ? i : {
					listener: i,
					once: !1
				});
				return this
			}, n.on = i("addListener"), n.addOnceListener = function(t, e) {
				return this.addListener(t, {
					listener: e,
					once: !0
				})
			}, n.once = i("addOnceListener"), n.defineEvent = function(t) {
				return this.getListeners(t), this
			}, n.defineEvents = function(t) {
				for (var e = 0; e < t.length; e += 1) this.defineEvent(t[e]);
				return this
			}, n.removeListener = function(t, i) {
				var n, o, r = this.getListenersAsObject(t);
				for (o in r) r.hasOwnProperty(o) && (n = e(r[o], i), -1 !== n && r[o].splice(
					n, 1));
				return this
			}, n.off = i("removeListener"), n.addListeners = function(t, e) {
				return this.manipulateListeners(!1, t, e)
			}, n.removeListeners = function(t, e) {
				return this.manipulateListeners(!0, t, e)
			}, n.manipulateListeners = function(t, e, i) {
				var n, o, r = t ? this.removeListener : this.addListener,
					s = t ? this.removeListeners : this.addListeners;
				if ("object" != typeof e || e instanceof RegExp)
					for (n = i.length; n--;) r.call(this, e, i[n]);
				else
					for (n in e) e.hasOwnProperty(n) && (o = e[n]) && ("function" == typeof o ?
						r.call(this, n, o) : s.call(this, n, o));
				return this
			}, n.removeEvent = function(t) {
				var e, i = typeof t,
					n = this._getEvents();
				if ("string" === i) delete n[t];
				else if (t instanceof RegExp)
					for (e in n) n.hasOwnProperty(e) && t.test(e) && delete n[e];
				else delete this._events;
				return this
			}, n.removeAllListeners = i("removeEvent"), n.emitEvent = function(t, e) {
				var i, n, o, r, s = this.getListenersAsObject(t);
				for (o in s)
					if (s.hasOwnProperty(o))
						for (n = s[o].length; n--;) i = s[o][n], i.once === !0 && this.removeListener(
								t, i.listener), r = i.listener.apply(this, e || []), r === this._getOnceReturnValue() &&
							this.removeListener(t, i.listener);
				return this
			}, n.trigger = i("emitEvent"), n.emit = function(t) {
				var e = Array.prototype.slice.call(arguments, 1);
				return this.emitEvent(t, e)
			}, n.setOnceReturnValue = function(t) {
				return this._onceReturnValue = t, this
			}, n._getOnceReturnValue = function() {
				return this.hasOwnProperty("_onceReturnValue") ? this._onceReturnValue : !0
			}, n._getEvents = function() {
				return this._events || (this._events = {})
			}, t.noConflict = function() {
				return o.EventEmitter = r, t
			}, "function" == typeof define && define.amd ? define(
				"eventEmitter/EventEmitter", [], function() {
					return t
				}) : "object" == typeof module && module.exports ? module.exports = t : o.EventEmitter =
			t
	}.call(this),
	function(t) {
		function e(t) {
			if (t) {
				if ("string" == typeof n[t]) return t;
				t = t.charAt(0).toUpperCase() + t.slice(1);
				for (var e, o = 0, r = i.length; r > o; o++)
					if (e = i[o] + t, "string" == typeof n[e]) return e
			}
		}
		var i = "Webkit Moz ms Ms O".split(" "),
			n = document.documentElement.style;
		"function" == typeof define && define.amd ? define(
				"get-style-property/get-style-property", [], function() {
					return e
				}) : "object" == typeof exports ? module.exports = e : t.getStyleProperty =
			e
	}(window),
	function(t) {
		function e(t) {
			var e = parseFloat(t),
				i = -1 === t.indexOf("%") && !isNaN(e);
			return i && e
		}

		function i() {}

		function n() {
			for (var t = {
				width: 0,
				height: 0,
				innerWidth: 0,
				innerHeight: 0,
				outerWidth: 0,
				outerHeight: 0
			}, e = 0, i = s.length; i > e; e++) {
				var n = s[e];
				t[n] = 0
			}
			return t
		}

		function o(i) {
			function o() {
				if (!p) {
					p = !0;
					var n = t.getComputedStyle;
					if (c = function() {
						var t = n ? function(t) {
							return n(t, null)
						} : function(t) {
							return t.currentStyle
						};
						return function(e) {
							var i = t(e);
							return i || r("Style returned " + i +
								". Are you running this code in a hidden iframe on Firefox? See http://bit.ly/getsizebug1"
							), i
						}
					}(), u = i("boxSizing")) {
						var o = document.createElement("div");
						o.style.width = "200px", o.style.padding = "1px 2px 3px 4px", o.style.borderStyle =
							"solid", o.style.borderWidth = "1px 2px 3px 4px", o.style[u] =
							"border-box";
						var s = document.body || document.documentElement;
						s.appendChild(o);
						var a = c(o);
						d = 200 === e(a.width), s.removeChild(o)
					}
				}
			}

			function a(t) {
				if (o(), "string" == typeof t && (t = document.querySelector(t)), t &&
					"object" == typeof t && t.nodeType) {
					var i = c(t);
					if ("none" === i.display) return n();
					var r = {};
					r.width = t.offsetWidth, r.height = t.offsetHeight;
					for (var a = r.isBorderBox = !(!u || !i[u] || "border-box" !== i[u]), p =
						0, f = s.length; f > p; p++) {
						var h = s[p],
							g = i[h];
						g = l(t, g);
						var m = parseFloat(g);
						r[h] = isNaN(m) ? 0 : m
					}
					var y = r.paddingLeft + r.paddingRight,
						v = r.paddingTop + r.paddingBottom,
						b = r.marginLeft + r.marginRight,
						w = r.marginTop + r.marginBottom,
						x = r.borderLeftWidth + r.borderRightWidth,
						S = r.borderTopWidth + r.borderBottomWidth,
						T = a && d,
						k = e(i.width);
					k !== !1 && (r.width = k + (T ? 0 : y + x));
					var C = e(i.height);
					return C !== !1 && (r.height = C + (T ? 0 : v + S)), r.innerWidth = r.width -
						(y + x), r.innerHeight = r.height - (v + S), r.outerWidth = r.width + b,
						r.outerHeight = r.height + w, r
				}
			}

			function l(e, i) {
				if (t.getComputedStyle || -1 === i.indexOf("%")) return i;
				var n = e.style,
					o = n.left,
					r = e.runtimeStyle,
					s = r && r.left;
				return s && (r.left = e.currentStyle.left), n.left = i, i = n.pixelLeft,
					n.left = o, s && (r.left = s), i
			}
			var c, u, d, p = !1;
			return a
		}
		var r = "undefined" == typeof console ? i : function(t) {
				console.error(t)
			},
			s = ["paddingLeft", "paddingRight", "paddingTop", "paddingBottom",
				"marginLeft", "marginRight", "marginTop", "marginBottom", "borderLeftWidth",
				"borderRightWidth", "borderTopWidth", "borderBottomWidth"
			];
		"function" == typeof define && define.amd ? define("get-size/get-size", [
			"get-style-property/get-style-property"
		], o) : "object" == typeof exports ? module.exports = o(require(
			"desandro-get-style-property")) : t.getSize = o(t.getStyleProperty)
	}(window),
	function(t) {
		function e(t, e) {
			return t[s](e)
		}

		function i(t) {
			if (!t.parentNode) {
				var e = document.createDocumentFragment();
				e.appendChild(t)
			}
		}

		function n(t, e) {
			i(t);
			for (var n = t.parentNode.querySelectorAll(e), o = 0, r = n.length; r > o; o++)
				if (n[o] === t) return !0;
			return !1
		}

		function o(t, n) {
			return i(t), e(t, n)
		}
		var r, s = function() {
			if (t.matchesSelector) return "matchesSelector";
			for (var e = ["webkit", "moz", "ms", "o"], i = 0, n = e.length; n > i; i++) {
				var o = e[i],
					r = o + "MatchesSelector";
				if (t[r]) return r
			}
		}();
		if (s) {
			var a = document.createElement("div"),
				l = e(a, "div");
			r = l ? e : o
		} else r = n;
		"function" == typeof define && define.amd ? define(
				"matches-selector/matches-selector", [], function() {
					return r
				}) : "object" == typeof exports ? module.exports = r : window.matchesSelector =
			r
	}(Element.prototype),
	function(t) {
		function e(t, e) {
			for (var i in e) t[i] = e[i];
			return t
		}

		function i(t) {
			for (var e in t) return !1;
			return e = null, !0
		}

		function n(t) {
			return t.replace(/([A-Z])/g, function(t) {
				return "-" + t.toLowerCase()
			})
		}

		function o(t, o, r) {
			function a(t, e) {
				t && (this.element = t, this.layout = e, this.position = {
					x: 0,
					y: 0
				}, this._create())
			}
			var l = r("transition"),
				c = r("transform"),
				u = l && c,
				d = !!r("perspective"),
				p = {
					WebkitTransition: "webkitTransitionEnd",
					MozTransition: "transitionend",
					OTransition: "otransitionend",
					transition: "transitionend"
				}[l],
				f = ["transform", "transition", "transitionDuration", "transitionProperty"],
				h = function() {
					for (var t = {}, e = 0, i = f.length; i > e; e++) {
						var n = f[e],
							o = r(n);
						o && o !== n && (t[n] = o)
					}
					return t
				}();
			e(a.prototype, t.prototype), a.prototype._create = function() {
				this._transn = {
					ingProperties: {},
					clean: {},
					onEnd: {}
				}, this.css({
					position: "absolute"
				})
			}, a.prototype.handleEvent = function(t) {
				var e = "on" + t.type;
				this[e] && this[e](t)
			}, a.prototype.getSize = function() {
				this.size = o(this.element)
			}, a.prototype.css = function(t) {
				var e = this.element.style;
				for (var i in t) {
					var n = h[i] || i;
					e[n] = t[i]
				}
			}, a.prototype.getPosition = function() {
				var t = s(this.element),
					e = this.layout.options,
					i = e.isOriginLeft,
					n = e.isOriginTop,
					o = parseInt(t[i ? "left" : "right"], 10),
					r = parseInt(t[n ? "top" : "bottom"], 10);
				o = isNaN(o) ? 0 : o, r = isNaN(r) ? 0 : r;
				var a = this.layout.size;
				o -= i ? a.paddingLeft : a.paddingRight, r -= n ? a.paddingTop : a.paddingBottom,
					this.position.x = o, this.position.y = r
			}, a.prototype.layoutPosition = function() {
				var t = this.layout.size,
					e = this.layout.options,
					i = {};
				e.isOriginLeft ? (i.left = this.position.x + t.paddingLeft + "px", i.right =
						"") : (i.right = this.position.x + t.paddingRight + "px", i.left = ""),
					e.isOriginTop ? (i.top = this.position.y + t.paddingTop + "px", i.bottom =
						"") : (i.bottom = this.position.y + t.paddingBottom + "px", i.top = ""),
					this.css(i), this.emitEvent("layout", [this])
			};
			var g = d ? function(t, e) {
				return "translate3d(" + t + "px, " + e + "px, 0)"
			} : function(t, e) {
				return "translate(" + t + "px, " + e + "px)"
			};
			a.prototype._transitionTo = function(t, e) {
					this.getPosition();
					var i = this.position.x,
						n = this.position.y,
						o = parseInt(t, 10),
						r = parseInt(e, 10),
						s = o === this.position.x && r === this.position.y;
					if (this.setPosition(t, e), s && !this.isTransitioning) return void this.layoutPosition();
					var a = t - i,
						l = e - n,
						c = {},
						u = this.layout.options;
					a = u.isOriginLeft ? a : -a, l = u.isOriginTop ? l : -l, c.transform = g(
						a, l), this.transition({
						to: c,
						onTransitionEnd: {
							transform: this.layoutPosition
						},
						isCleaning: !0
					})
				}, a.prototype.goTo = function(t, e) {
					this.setPosition(t, e), this.layoutPosition()
				}, a.prototype.moveTo = u ? a.prototype._transitionTo : a.prototype.goTo,
				a.prototype.setPosition = function(t, e) {
					this.position.x = parseInt(t, 10), this.position.y = parseInt(e, 10)
				}, a.prototype._nonTransition = function(t) {
					this.css(t.to), t.isCleaning && this._removeStyles(t.to);
					for (var e in t.onTransitionEnd) t.onTransitionEnd[e].call(this)
				}, a.prototype._transition = function(t) {
					if (!parseFloat(this.layout.options.transitionDuration)) return void this
						._nonTransition(t);
					var e = this._transn;
					for (var i in t.onTransitionEnd) e.onEnd[i] = t.onTransitionEnd[i];
					for (i in t.to) e.ingProperties[i] = !0, t.isCleaning && (e.clean[i] = !0);
					if (t.from) {
						this.css(t.from);
						var n = this.element.offsetHeight;
						n = null
					}
					this.enableTransition(t.to), this.css(t.to), this.isTransitioning = !0
				};
			var m = c && n(c) + ",opacity";
			a.prototype.enableTransition = function() {
				this.isTransitioning || (this.css({
					transitionProperty: m,
					transitionDuration: this.layout.options.transitionDuration
				}), this.element.addEventListener(p, this, !1))
			}, a.prototype.transition = a.prototype[l ? "_transition" :
				"_nonTransition"], a.prototype.onwebkitTransitionEnd = function(t) {
				this.ontransitionend(t)
			}, a.prototype.onotransitionend = function(t) {
				this.ontransitionend(t)
			};
			var y = {
				"-webkit-transform": "transform",
				"-moz-transform": "transform",
				"-o-transform": "transform"
			};
			a.prototype.ontransitionend = function(t) {
				if (t.target === this.element) {
					var e = this._transn,
						n = y[t.propertyName] || t.propertyName;
					if (delete e.ingProperties[n], i(e.ingProperties) && this.disableTransition(),
						n in e.clean && (this.element.style[t.propertyName] = "", delete e.clean[
							n]), n in e.onEnd) {
						var o = e.onEnd[n];
						o.call(this), delete e.onEnd[n]
					}
					this.emitEvent("transitionEnd", [this])
				}
			}, a.prototype.disableTransition = function() {
				this.removeTransitionStyles(), this.element.removeEventListener(p, this, !
					1), this.isTransitioning = !1
			}, a.prototype._removeStyles = function(t) {
				var e = {};
				for (var i in t) e[i] = "";
				this.css(e)
			};
			var v = {
				transitionProperty: "",
				transitionDuration: ""
			};
			return a.prototype.removeTransitionStyles = function() {
				this.css(v)
			}, a.prototype.removeElem = function() {
				this.element.parentNode.removeChild(this.element), this.emitEvent(
					"remove", [this])
			}, a.prototype.remove = function() {
				if (!l || !parseFloat(this.layout.options.transitionDuration)) return void this
					.removeElem();
				var t = this;
				this.on("transitionEnd", function() {
					return t.removeElem(), !0
				}), this.hide()
			}, a.prototype.reveal = function() {
				delete this.isHidden, this.css({
					display: ""
				});
				var t = this.layout.options;
				this.transition({
					from: t.hiddenStyle,
					to: t.visibleStyle,
					isCleaning: !0
				})
			}, a.prototype.hide = function() {
				this.isHidden = !0, this.css({
					display: ""
				});
				var t = this.layout.options;
				this.transition({
					from: t.visibleStyle,
					to: t.hiddenStyle,
					isCleaning: !0,
					onTransitionEnd: {
						opacity: function() {
							this.isHidden && this.css({
								display: "none"
							})
						}
					}
				})
			}, a.prototype.destroy = function() {
				this.css({
					position: "",
					left: "",
					right: "",
					top: "",
					bottom: "",
					transition: "",
					transform: ""
				})
			}, a
		}
		var r = t.getComputedStyle,
			s = r ? function(t) {
				return r(t, null)
			} : function(t) {
				return t.currentStyle
			};
		"function" == typeof define && define.amd ? define("outlayer/item", [
			"eventEmitter/EventEmitter", "get-size/get-size",
			"get-style-property/get-style-property"
		], o) : "object" == typeof exports ? module.exports = o(require(
			"wolfy87-eventemitter"), require("get-size"), require(
			"desandro-get-style-property")) : (t.Outlayer = {}, t.Outlayer.Item = o(t.EventEmitter,
			t.getSize, t.getStyleProperty))
	}(window),
	function(t) {
		function e(t, e) {
			for (var i in e) t[i] = e[i];
			return t
		}

		function i(t) {
			return "[object Array]" === d.call(t)
		}

		function n(t) {
			var e = [];
			if (i(t)) e = t;
			else if (t && "number" == typeof t.length)
				for (var n = 0, o = t.length; o > n; n++) e.push(t[n]);
			else e.push(t);
			return e
		}

		function o(t, e) {
			var i = f(e, t); - 1 !== i && e.splice(i, 1)
		}

		function r(t) {
			return t.replace(/(.)([A-Z])/g, function(t, e, i) {
				return e + "-" + i
			}).toLowerCase()
		}

		function s(i, s, d, f, h, g) {
			function m(t, i) {
				if ("string" == typeof t && (t = a.querySelector(t)), !t || !p(t)) return
					void(l && l.error("Bad " + this.constructor.namespace + " element: " +
						t));
				this.element = t, this.options = e({}, this.constructor.defaults), this.option(
					i);
				var n = ++y;
				this.element.outlayerGUID = n, v[n] = this, this._create(), this.options.isInitLayout &&
					this.layout()
			}
			var y = 0,
				v = {};
			return m.namespace = "outlayer", m.Item = g, m.defaults = {
					containerStyle: {
						position: "relative"
					},
					isInitLayout: !0,
					isOriginLeft: !0,
					isOriginTop: !0,
					isResizeBound: !0,
					isResizingContainer: !0,
					transitionDuration: "0.4s",
					hiddenStyle: {
						opacity: 0,
						transform: "scale(0.001)"
					},
					visibleStyle: {
						opacity: 1,
						transform: "scale(1)"
					}
				}, e(m.prototype, d.prototype), m.prototype.option = function(t) {
					e(this.options, t)
				}, m.prototype._create = function() {
					this.reloadItems(), this.stamps = [], this.stamp(this.options.stamp), e(
							this.element.style, this.options.containerStyle), this.options.isResizeBound &&
						this.bindResize()
				}, m.prototype.reloadItems = function() {
					this.items = this._itemize(this.element.children)
				}, m.prototype._itemize = function(t) {
					for (var e = this._filterFindItemElements(t), i = this.constructor.Item,
						n = [], o = 0, r = e.length; r > o; o++) {
						var s = e[o],
							a = new i(s, this);
						n.push(a)
					}
					return n
				}, m.prototype._filterFindItemElements = function(t) {
					t = n(t);
					for (var e = this.options.itemSelector, i = [], o = 0, r = t.length; r >
						o; o++) {
						var s = t[o];
						if (p(s))
							if (e) {
								h(s, e) && i.push(s);
								for (var a = s.querySelectorAll(e), l = 0, c = a.length; c > l; l++) i
									.push(a[l])
							} else i.push(s)
					}
					return i
				}, m.prototype.getItemElements = function() {
					for (var t = [], e = 0, i = this.items.length; i > e; e++) t.push(this.items[
						e].element);
					return t
				}, m.prototype.layout = function() {
					this._resetLayout(), this._manageStamps();
					var t = void 0 !== this.options.isLayoutInstant ? this.options.isLayoutInstant :
						!this._isLayoutInited;
					this.layoutItems(this.items, t), this._isLayoutInited = !0
				}, m.prototype._init = m.prototype.layout, m.prototype._resetLayout =
				function() {
					this.getSize()
				}, m.prototype.getSize = function() {
					this.size = f(this.element)
				}, m.prototype._getMeasurement = function(t, e) {
					var i, n = this.options[t];
					n ? ("string" == typeof n ? i = this.element.querySelector(n) : p(n) && (
						i = n), this[t] = i ? f(i)[e] : n) : this[t] = 0
				}, m.prototype.layoutItems = function(t, e) {
					t = this._getItemsForLayout(t), this._layoutItems(t, e), this._postLayout()
				}, m.prototype._getItemsForLayout = function(t) {
					for (var e = [], i = 0, n = t.length; n > i; i++) {
						var o = t[i];
						o.isIgnored || e.push(o)
					}
					return e
				}, m.prototype._layoutItems = function(t, e) {
					function i() {
						n.emitEvent("layoutComplete", [n, t])
					}
					var n = this;
					if (!t || !t.length) return void i();
					this._itemsOn(t, "layout", i);
					for (var o = [], r = 0, s = t.length; s > r; r++) {
						var a = t[r],
							l = this._getItemLayoutPosition(a);
						l.item = a, l.isInstant = e || a.isLayoutInstant, o.push(l)
					}
					this._processLayoutQueue(o)
				}, m.prototype._getItemLayoutPosition = function() {
					return {
						x: 0,
						y: 0
					}
				}, m.prototype._processLayoutQueue = function(t) {
					for (var e = 0, i = t.length; i > e; e++) {
						var n = t[e];
						this._positionItem(n.item, n.x, n.y, n.isInstant)
					}
				}, m.prototype._positionItem = function(t, e, i, n) {
					n ? t.goTo(e, i) : t.moveTo(e, i)
				}, m.prototype._postLayout = function() {
					this.resizeContainer()
				}, m.prototype.resizeContainer = function() {
					if (this.options.isResizingContainer) {
						var t = this._getContainerSize();
						t && (this._setContainerMeasure(t.width, !0), this._setContainerMeasure(
							t.height, !1))
					}
				}, m.prototype._getContainerSize = u, m.prototype._setContainerMeasure =
				function(t, e) {
					if (void 0 !== t) {
						var i = this.size;
						i.isBorderBox && (t += e ? i.paddingLeft + i.paddingRight + i.borderLeftWidth +
							i.borderRightWidth : i.paddingBottom + i.paddingTop + i.borderTopWidth +
							i.borderBottomWidth), t = Math.max(t, 0), this.element.style[e ?
							"width" : "height"] = t + "px"
					}
				}, m.prototype._itemsOn = function(t, e, i) {
					function n() {
						return o++, o === r && i.call(s), !0
					}
					for (var o = 0, r = t.length, s = this, a = 0, l = t.length; l > a; a++) {
						var c = t[a];
						c.on(e, n)
					}
				}, m.prototype.ignore = function(t) {
					var e = this.getItem(t);
					e && (e.isIgnored = !0)
				}, m.prototype.unignore = function(t) {
					var e = this.getItem(t);
					e && delete e.isIgnored
				}, m.prototype.stamp = function(t) {
					if (t = this._find(t)) {
						this.stamps = this.stamps.concat(t);
						for (var e = 0, i = t.length; i > e; e++) {
							var n = t[e];
							this.ignore(n)
						}
					}
				}, m.prototype.unstamp = function(t) {
					if (t = this._find(t))
						for (var e = 0, i = t.length; i > e; e++) {
							var n = t[e];
							o(n, this.stamps), this.unignore(n)
						}
				}, m.prototype._find = function(t) {
					return t ? ("string" == typeof t && (t = this.element.querySelectorAll(t)),
						t = n(t)) : void 0
				}, m.prototype._manageStamps = function() {
					if (this.stamps && this.stamps.length) {
						this._getBoundingRect();
						for (var t = 0, e = this.stamps.length; e > t; t++) {
							var i = this.stamps[t];
							this._manageStamp(i)
						}
					}
				}, m.prototype._getBoundingRect = function() {
					var t = this.element.getBoundingClientRect(),
						e = this.size;
					this._boundingRect = {
						left: t.left + e.paddingLeft + e.borderLeftWidth,
						top: t.top + e.paddingTop + e.borderTopWidth,
						right: t.right - (e.paddingRight + e.borderRightWidth),
						bottom: t.bottom - (e.paddingBottom + e.borderBottomWidth)
					}
				}, m.prototype._manageStamp = u, m.prototype._getElementOffset = function(
					t) {
					var e = t.getBoundingClientRect(),
						i = this._boundingRect,
						n = f(t),
						o = {
							left: e.left - i.left - n.marginLeft,
							top: e.top - i.top - n.marginTop,
							right: i.right - e.right - n.marginRight,
							bottom: i.bottom - e.bottom - n.marginBottom
						};
					return o
				}, m.prototype.handleEvent = function(t) {
					var e = "on" + t.type;
					this[e] && this[e](t)
				}, m.prototype.bindResize = function() {
					this.isResizeBound || (i.bind(t, "resize", this), this.isResizeBound = !0)
				}, m.prototype.unbindResize = function() {
					this.isResizeBound && i.unbind(t, "resize", this), this.isResizeBound = !
						1
				}, m.prototype.onresize = function() {
					function t() {
						e.resize(), delete e.resizeTimeout
					}
					this.resizeTimeout && clearTimeout(this.resizeTimeout);
					var e = this;
					this.resizeTimeout = setTimeout(t, 100)
				}, m.prototype.resize = function() {
					this.isResizeBound && this.needsResizeLayout() && this.layout()
				}, m.prototype.needsResizeLayout = function() {
					var t = f(this.element),
						e = this.size && t;
					return e && t.innerWidth !== this.size.innerWidth
				}, m.prototype.addItems = function(t) {
					var e = this._itemize(t);
					return e.length && (this.items = this.items.concat(e)), e
				}, m.prototype.appended = function(t) {
					var e = this.addItems(t);
					e.length && (this.layoutItems(e, !0), this.reveal(e))
				}, m.prototype.prepended = function(t) {
					var e = this._itemize(t);
					if (e.length) {
						var i = this.items.slice(0);
						this.items = e.concat(i), this._resetLayout(), this._manageStamps(),
							this.layoutItems(e, !0), this.reveal(e), this.layoutItems(i)
					}
				}, m.prototype.reveal = function(t) {
					var e = t && t.length;
					if (e)
						for (var i = 0; e > i; i++) {
							var n = t[i];
							n.reveal()
						}
				}, m.prototype.hide = function(t) {
					var e = t && t.length;
					if (e)
						for (var i = 0; e > i; i++) {
							var n = t[i];
							n.hide()
						}
				}, m.prototype.getItem = function(t) {
					for (var e = 0, i = this.items.length; i > e; e++) {
						var n = this.items[e];
						if (n.element === t) return n
					}
				}, m.prototype.getItems = function(t) {
					if (t && t.length) {
						for (var e = [], i = 0, n = t.length; n > i; i++) {
							var o = t[i],
								r = this.getItem(o);
							r && e.push(r)
						}
						return e
					}
				}, m.prototype.remove = function(t) {
					t = n(t);
					var e = this.getItems(t);
					if (e && e.length) {
						this._itemsOn(e, "remove", function() {
							this.emitEvent("removeComplete", [this, e])
						});
						for (var i = 0, r = e.length; r > i; i++) {
							var s = e[i];
							s.remove(), o(s, this.items)
						}
					}
				}, m.prototype.destroy = function() {
					var t = this.element.style;
					t.height = "", t.position = "", t.width = "";
					for (var e = 0, i = this.items.length; i > e; e++) {
						var n = this.items[e];
						n.destroy()
					}
					this.unbindResize();
					var o = this.element.outlayerGUID;
					delete v[o], delete this.element.outlayerGUID, c && c.removeData(this.element,
						this.constructor.namespace)
				}, m.data = function(t) {
					var e = t && t.outlayerGUID;
					return e && v[e]
				}, m.create = function(t, i) {
					function n() {
						m.apply(this, arguments)
					}
					return Object.create ? n.prototype = Object.create(m.prototype) : e(n.prototype,
							m.prototype), n.prototype.constructor = n, n.defaults = e({}, m.defaults),
						e(n.defaults, i), n.prototype.settings = {}, n.namespace = t, n.data = m
						.data, n.Item = function() {
							g.apply(this, arguments)
						}, n.Item.prototype = new g, s(function() {
							for (var e = r(t), i = a.querySelectorAll(".js-" + e), o = "data-" + e +
								"-options", s = 0, u = i.length; u > s; s++) {
								var d, p = i[s],
									f = p.getAttribute(o);
								try {
									d = f && JSON.parse(f)
								} catch (h) {
									l && l.error("Error parsing " + o + " on " + p.nodeName.toLowerCase() +
										(p.id ? "#" + p.id : "") + ": " + h);
									continue
								}
								var g = new n(p, d);
								c && c.data(p, t, g)
							}
						}), c && c.bridget && c.bridget(t, n), n
				}, m.Item = g, m
		}
		var a = t.document,
			l = t.console,
			c = t.jQuery,
			u = function() {},
			d = Object.prototype.toString,
			p = "function" == typeof HTMLElement || "object" == typeof HTMLElement ?
			function(t) {
				return t instanceof HTMLElement
			} : function(t) {
				return t && "object" == typeof t && 1 === t.nodeType && "string" == typeof t
					.nodeName
			},
			f = Array.prototype.indexOf ? function(t, e) {
				return t.indexOf(e)
			} : function(t, e) {
				for (var i = 0, n = t.length; n > i; i++)
					if (t[i] === e) return i;
				return -1
			};
		"function" == typeof define && define.amd ? define("outlayer/outlayer", [
			"eventie/eventie", "doc-ready/doc-ready", "eventEmitter/EventEmitter",
			"get-size/get-size", "matches-selector/matches-selector", "./item"
		], s) : "object" == typeof exports ? module.exports = s(require("eventie"),
			require("doc-ready"), require("wolfy87-eventemitter"), require("get-size"),
			require("desandro-matches-selector"), require("./item")) : t.Outlayer = s(t
			.eventie, t.docReady, t.EventEmitter, t.getSize, t.matchesSelector, t.Outlayer
			.Item)
	}(window),
	function(t) {
		function e(t, e) {
			var n = t.create("masonry");
			return n.prototype._resetLayout = function() {
				this.getSize(), this._getMeasurement("columnWidth", "outerWidth"), this._getMeasurement(
					"gutter", "outerWidth"), this.measureColumns();
				var t = this.cols;
				for (this.colYs = []; t--;) this.colYs.push(0);
				this.maxY = 0
			}, n.prototype.measureColumns = function() {
				if (this.getContainerWidth(), !this.columnWidth) {
					var t = this.items[0],
						i = t && t.element;
					this.columnWidth = i && e(i).outerWidth || this.containerWidth
				}
				this.columnWidth += this.gutter, this.cols = Math.floor((this.containerWidth +
					this.gutter) / this.columnWidth), this.cols = Math.max(this.cols, 1)
			}, n.prototype.getContainerWidth = function() {
				var t = this.options.isFitWidth ? this.element.parentNode : this.element,
					i = e(t);
				this.containerWidth = i && i.innerWidth
			}, n.prototype._getItemLayoutPosition = function(t) {
				t.getSize();
				var e = t.size.outerWidth % this.columnWidth,
					n = e && 1 > e ? "round" : "ceil",
					o = Math[n](t.size.outerWidth / this.columnWidth);
				o = Math.min(o, this.cols);
				for (var r = this._getColGroup(o), s = Math.min.apply(Math, r), a = i(r,
						s), l = {
						x: this.columnWidth * a,
						y: s
					}, c = s + t.size.outerHeight, u = this.cols + 1 - r.length, d = 0; u >
					d; d++) this.colYs[a + d] = c;
				return l
			}, n.prototype._getColGroup = function(t) {
				if (2 > t) return this.colYs;
				for (var e = [], i = this.cols + 1 - t, n = 0; i > n; n++) {
					var o = this.colYs.slice(n, n + t);
					e[n] = Math.max.apply(Math, o)
				}
				return e
			}, n.prototype._manageStamp = function(t) {
				var i = e(t),
					n = this._getElementOffset(t),
					o = this.options.isOriginLeft ? n.left : n.right,
					r = o + i.outerWidth,
					s = Math.floor(o / this.columnWidth);
				s = Math.max(0, s);
				var a = Math.floor(r / this.columnWidth);
				a -= r % this.columnWidth ? 0 : 1, a = Math.min(this.cols - 1, a);
				for (var l = (this.options.isOriginTop ? n.top : n.bottom) + i.outerHeight,
					c = s; a >= c; c++) this.colYs[c] = Math.max(l, this.colYs[c])
			}, n.prototype._getContainerSize = function() {
				this.maxY = Math.max.apply(Math, this.colYs);
				var t = {
					height: this.maxY
				};
				return this.options.isFitWidth && (t.width = this._getContainerFitWidth()),
					t
			}, n.prototype._getContainerFitWidth = function() {
				for (var t = 0, e = this.cols; --e && 0 === this.colYs[e];) t++;
				return (this.cols - t) * this.columnWidth - this.gutter
			}, n.prototype.needsResizeLayout = function() {
				var t = this.containerWidth;
				return this.getContainerWidth(), t !== this.containerWidth
			}, n
		}
		var i = Array.prototype.indexOf ? function(t, e) {
			return t.indexOf(e)
		} : function(t, e) {
			for (var i = 0, n = t.length; n > i; i++) {
				var o = t[i];
				if (o === e) return i
			}
			return -1
		};
		"function" == typeof define && define.amd ? define(["outlayer/outlayer",
			"get-size/get-size"
		], e) : "object" == typeof exports ? module.exports = e(require("outlayer"),
			require("get-size")) : t.Masonry = e(t.Outlayer, t.getSize)
	}(window),
	function(t) {
		"use strict";
		"function" == typeof define && define.amd ? define(["jquery"], t) :
			"undefined" != typeof exports ? module.exports = t(require("jquery")) : t(
				jQuery)
	}(function(t) {
		"use strict";
		var e = window.Slick || {};
		e = function() {
			function e(e, n) {
				var o, r, s, a = this;
				if (a.defaults = {
						accessibility: !0,
						arrows: !0,
						prevArrow: '<button type="button" data-role="none" class="slick-prev" aria-label="previous">Previous</button>',
						nextArrow: '<button type="button" data-role="none" class="slick-next" aria-label="next">Next</button>',
						cssEase: "ease",
						easing: "linear",
						edgeFriction: .35,
						focusOnSelect: !1,
						initialSlide: 0,
						mobileFirst: !1,
						respondTo: "window",
						responsive: null,
						slide: "",
						slidesToShow: 1,
						slidesToScroll: 1,
						speed: 500,
						waitForAnimate: !0,
						zIndex: 1e3
					}, a.initials = {
						animating: !1,
						dragging: !1,
						currentDirection: 0,
						currentLeft: null,
						currentSlide: 0,
						direction: 1,
						listWidth: null,
						listHeight: null,
						loadIndex: 0,
						$nextArrow: null,
						$prevArrow: null,
						slideCount: null,
						slideWidth: null,
						$slideTrack: null,
						$slides: null,
						sliding: !1,
						slideOffset: 0,
						$list: null,
						touchObject: {},
						transformsEnabled: !1,
						unslicked: !1
					}, t.extend(a, a.initials), a.activeBreakpoint = null, a.animType =
					null, a.animProp = null, a.breakpoints = [], a.breakpointSettings = [],
					a.cssTransitions = !1, a.hidden = "hidden", a.paused = !1, a.positionProp =
					null, a.respondTo = null, a.rowCount = 1, a.shouldClick = !0, a.$slider =
					t(e), a.$slidesCache = null, a.transformType = null, a.transitionType =
					null, a.visibilityChange = "visibilitychange", a.windowWidth = 0, a.windowTimer =
					null, o = t(e).data("slick") || {}, a.options = t.extend({}, a.defaults,
						o, n), a.currentSlide = a.options.initialSlide, a.originalSettings = a
					.options, r = a.options.responsive || null, r && r.length > -1) {
					a.respondTo = a.options.respondTo || "window";
					for (s in r) r.hasOwnProperty(s) && (a.breakpoints.push(r[s].breakpoint),
						a.breakpointSettings[r[s].breakpoint] = r[s].settings);
					a.breakpoints.sort(function(t, e) {
						return a.options.mobileFirst === !0 ? t - e : e - t
					})
				}
				"undefined" != typeof document.mozHidden ? (a.hidden = "mozHidden", a.visibilityChange =
						"mozvisibilitychange") : "undefined" != typeof document.webkitHidden &&
					(a.hidden = "webkitHidden", a.visibilityChange =
						"webkitvisibilitychange"), a.changeSlide = t.proxy(a.changeSlide, a),
					a.clickHandler = t.proxy(a.clickHandler, a), a.selectHandler = t.proxy(
						a.selectHandler, a), a.setPosition = t.proxy(a.setPosition, a), a.dragHandler =
					t.proxy(a.dragHandler, a), a.keyHandler = t.proxy(a.keyHandler, a), a.instanceUid =
					i++, a.htmlExpr = /^(?:\s*(<[\w\W]+>)[^>]*)$/, a.init(!0), a.checkResponsive(!
						0)
			}
			var i = 0;
			return e
		}(), e.prototype.addSlide = e.prototype.slickAdd = function(e, i, n) {
			var o = this;
			if ("boolean" == typeof i) n = i, i = null;
			else if (0 > i || i >= o.slideCount) return !1;
			o.unload(), "number" == typeof i ? 0 === i && 0 === o.$slides.length ? t(e)
				.appendTo(o.$slideTrack) : n ? t(e).insertBefore(o.$slides.eq(i)) : t(e).insertAfter(
					o.$slides.eq(i)) : n === !0 ? t(e).prependTo(o.$slideTrack) : t(e).appendTo(
					o.$slideTrack), o.$slides = o.$slideTrack.children(this.options.slide),
				o.$slideTrack.children(this.options.slide).detach(), o.$slideTrack.append(
					o.$slides), o.$slides.each(function(e, i) {
					t(i).attr("data-slick-index", e)
				}), o.$slidesCache = o.$slides, o.reinit()
		}, e.prototype.animateSlide = function(t, e) {
			var i = {},
				n = this;
			n.$slideTrack.off(transEndEventName + ".slick"), n.applyTransition(), t =
				Math.ceil(t), i[n.animType] = Modernizr.csstransforms3d ? "translate3d(" +
				t + "px, 0px, 0px)" : "translate(" + t + "px, 0px)", n.$slideTrack.css(i),
				e && (n.$slideTrack.one(transEndEventName + ".slick", function() {
					n.disableTransition(), e.call()
				}), Modernizr.csstransitions || (n.disableTransition(), e.call()))
		}, e.prototype.applyTransition = function() {
			var t = this,
				e = {};
			e[t.transitionType] = t.transformType + " " + t.options.speed + "ms " + t.options
				.cssEase, t.$slideTrack.css(e)
		}, e.prototype.buildArrows = function() {
			var e = this;
			e.options.arrows === !0 && e.slideCount > e.options.slidesToShow && (e.$prevArrow =
				t(e.options.prevArrow), e.$nextArrow = t(e.options.nextArrow), e.htmlExpr
				.test(e.options.prevArrow) && e.$prevArrow.appendTo(e.options.appendArrows),
				e.htmlExpr.test(e.options.nextArrow) && e.$nextArrow.appendTo(e.options.appendArrows),
				e.$prevArrow.addClass("slick-disabled"))
		}, e.prototype.buildOut = function() {
			var e = this;
			e.$slides = e.$slider.children(":not(.slick-cloned)").addClass(
					"slick-slide"), e.slideCount = e.$slides.length, e.$slides.each(function(
					e, i) {
					t(i).attr("data-slick-index", e).data("originalStyling", t(i).attr(
						"style") || "")
				}), e.$slidesCache = e.$slides, e.$slider.addClass("slick-slider"), e.$slideTrack =
				0 === e.slideCount ? t('<div class="slick-track"/>').appendTo(e.$slider) :
				e.$slides.wrapAll('<div class="slick-track"/>').parent(), e.$list = e.$slideTrack
				.wrap('<div aria-live="polite" class="slick-list"/>').parent(), e.$slideTrack
				.css("opacity", 0), t("img[data-lazy]", e.$slider).not("[src]").addClass(
					"slick-loading"), e.buildArrows(), e.options.accessibility === !0 && e.$list
				.prop("tabIndex", 0), e.setSlideClasses("number" == typeof this.currentSlide ?
					this.currentSlide : 0)
		}, e.prototype.checkResponsive = function(e) {
			var i, n, o, r = this,
				s = !1,
				a = r.$slider.width(),
				l = window.innerWidth || t(window).width();
			if ("window" === r.respondTo ? o = l : "slider" === r.respondTo ? o = a :
				"min" === r.respondTo && (o = Math.min(l, a)), r.originalSettings.responsive &&
				r.originalSettings.responsive.length > -1 && null !== r.originalSettings.responsive
			) {
				n = null;
				for (i in r.breakpoints) r.breakpoints.hasOwnProperty(i) && (r.originalSettings
					.mobileFirst === !1 ? o < r.breakpoints[i] && (n = r.breakpoints[i]) :
					o > r.breakpoints[i] && (n = r.breakpoints[i]));
				null !== n ? null !== r.activeBreakpoint ? n !== r.activeBreakpoint && (r
					.activeBreakpoint = n, "unslick" === r.breakpointSettings[n] ? r.unslick(
						n) : (r.options = t.extend({}, r.originalSettings, r.breakpointSettings[
						n]), e === !0 && (r.currentSlide = r.options.initialSlide), r.refresh(
						e)), s = n) : (r.activeBreakpoint = n, "unslick" === r.breakpointSettings[
					n] ? r.unslick(n) : (r.options = t.extend({}, r.originalSettings, r.breakpointSettings[
					n]), e === !0 && (r.currentSlide = r.options.initialSlide), r.refresh(
					e)), s = n) : null !== r.activeBreakpoint && (r.activeBreakpoint = null,
					r.options = r.originalSettings, e === !0 && (r.currentSlide = r.options
						.initialSlide), r.refresh(e), s = n), e || s === !1 || r.$slider.trigger(
					"breakpoint", [r, s])
			}
		}, e.prototype.changeSlide = function(e, i) {
			var n, o, r, s = this,
				a = t(e.target);
			switch (a.is("a") && e.preventDefault(), a.is("li") || (a = a.closest("li")),
				r = s.slideCount % s.options.slidesToScroll !== 0, n = r ? 0 : (s.slideCount -
					s.currentSlide) % s.options.slidesToScroll, e.data.message) {
				case "previous":
					o = 0 === n ? s.options.slidesToScroll : s.options.slidesToShow - n, s.slideCount >
						s.options.slidesToShow && s.slideHandler(s.currentSlide - o, !1, i);
					break;
				case "next":
					o = 0 === n ? s.options.slidesToScroll : n, s.slideCount > s.options.slidesToShow &&
						s.slideHandler(s.currentSlide + o, !1, i);
					break;
				case "index":
					var l = 0 === e.data.index ? 0 : e.data.index || a.index() * s.options.slidesToScroll;
					s.slideHandler(s.checkNavigable(l), !1, i), a.children().trigger("focus");
					break;
				default:
					return
			}
		}, e.prototype.checkNavigable = function(t) {
			var e, i, n = this;
			if (e = n.getNavigableIndexes(), i = 0, t > e[e.length - 1]) t = e[e.length -
				1];
			else
				for (var o in e) {
					if (t < e[o]) {
						t = i;
						break
					}
					i = e[o]
				}
			return t
		}, e.prototype.cleanUpEvents = function() {
			var e = this;
			e.$list.off("click.slick", e.clickHandler), t(document).off(e.visibilityChange,
					e.visibility), e.$list.off("mouseenter.slick", t.proxy(e.setPaused, e, !
					0)), e.$list.off("mouseleave.slick", t.proxy(e.setPaused, e, !1)), e.options
				.accessibility === !0 && e.$list.off("keydown.slick", e.keyHandler), e.options
				.focusOnSelect === !0 && t(e.$slideTrack).children().off("click.slick", e
					.selectHandler), t(window).off("orientationchange.slick.slick-" + e.instanceUid,
					e.orientationChange), t(window).off("resize.slick.slick-" + e.instanceUid,
					e.resize), t(window).off("load.slick.slick-" + e.instanceUid, e.setPosition),
				t(document).off("ready.slick.slick-" + e.instanceUid, e.setPosition)
		}, e.prototype.clickHandler = function(t) {
			var e = this;
			e.shouldClick === !1 && (t.stopImmediatePropagation(), t.stopPropagation(),
				t.preventDefault())
		}, e.prototype.destroy = function(e) {
			var i = this;
			i.touchObject = {}, i.cleanUpEvents(), t(".slick-cloned", i.$slider).detach(),
				i.$prevArrow && "object" != typeof i.options.prevArrow && i.$prevArrow.remove(),
				i.$nextArrow && "object" != typeof i.options.nextArrow && i.$nextArrow.remove(),
				i.$slides && (i.$slides.removeClass(
						"slick-slide slick-active slick-visible").removeAttr("aria-hidden").removeAttr(
						"data-slick-index").each(function() {
						t(this).attr("style", t(this).data("originalStyling"))
					}), i.$slideTrack.children(this.options.slide).detach(), i.$slideTrack.detach(),
					i.$list.detach(), i.$slider.append(i.$slides)), i.$slider.removeClass(
					"slick-slider"), i.$slider.removeClass("slick-initialized"), i.unslicked = !
				0, e || i.$slider.trigger("destroy", [i])
		}, e.prototype.disableTransition = function() {
			var t = this,
				e = {};
			e[t.transitionType] = "", t.$slideTrack.css(e)
		}, e.prototype.getCurrent = e.prototype.slickCurrentSlide = function() {
			var t = this;
			return t.currentSlide
		}, e.prototype.getLeft = function(t) {
			var e, i, n = this,
				o = 0;
			return n.slideOffset = 0, i = n.$slides.first().outerHeight(), t + n.options
				.slidesToShow > n.slideCount && (n.slideOffset = (t + n.options.slidesToShow -
						n.slideCount) * n.slideWidth, o = (t + n.options.slidesToShow - n.slideCount) *
					i), n.slideCount <= n.options.slidesToShow && (n.slideOffset = 0), e = t *
				n.slideWidth * -1 + n.slideOffset
		}, e.prototype.getOption = e.prototype.slickGetOption = function(t) {
			var e = this;
			return e.options[t]
		}, e.prototype.getNavigableIndexes = function() {
			var t, e = this,
				i = 0,
				n = 0,
				o = [];
			for (t = e.slideCount; t > i;) o.push(i), i = n + e.options.slidesToScroll,
				n += e.options.slidesToScroll <= e.options.slidesToShow ? e.options.slidesToScroll :
				e.options.slidesToShow;
			return o
		}, e.prototype.getSlick = function() {
			return this
		}, e.prototype.getSlideCount = function() {
			var t = this;
			return t.options.slidesToScroll
		}, e.prototype.goTo = e.prototype.slickGoTo = function(t, e) {
			var i = this;
			i.changeSlide({
				data: {
					message: "index",
					index: parseInt(t)
				}
			}, e)
		}, e.prototype.init = function(e) {
			var i = this;
			t(i.$slider).hasClass("slick-initialized") || (t(i.$slider).addClass(
					"slick-initialized"), i.buildOut(), i.setProps(), i.startLoad(), i.loadSlider(),
				i.initializeEvents()), e && i.$slider.trigger("init", [i])
		}, e.prototype.initArrowEvents = function() {
			var t = this;
			t.options.arrows === !0 && t.slideCount > t.options.slidesToShow && (t.$prevArrow
				.on("click.slick", {
					message: "previous"
				}, t.changeSlide), t.$nextArrow.on("click.slick", {
					message: "next"
				}, t.changeSlide))
		}, e.prototype.initializeEvents = function() {
			var e = this;
			e.initArrowEvents(), e.$list.on("click.slick", e.clickHandler), t(document)
				.on(e.visibilityChange, t.proxy(e.visibility, e)), e.$list.on(
					"mouseenter.slick", t.proxy(e.setPaused, e, !0)), e.$list.on(
					"mouseleave.slick", t.proxy(e.setPaused, e, !1)), e.options.accessibility ===
				!0 && e.$list.on("keydown.slick", e.keyHandler), e.options.focusOnSelect ===
				!0 && t(e.$slideTrack).children().on("click.slick", e.selectHandler), t(
					window).on("orientationchange.slick.slick-" + e.instanceUid, t.proxy(e.orientationChange,
					e)), t(window).on("resize.slick.slick-" + e.instanceUid, t.proxy(e.resize,
					e)), t(window).on("load.slick.slick-" + e.instanceUid, e.setPosition), t(
					document).on("ready.slick.slick-" + e.instanceUid, e.setPosition)
		}, e.prototype.initUI = function() {
			var t = this;
			t.options.arrows === !0 && t.slideCount > t.options.slidesToShow && (t.$prevArrow
				.show(), t.$nextArrow.show())
		}, e.prototype.keyHandler = function(t) {
			var e = this;
			37 === t.keyCode && e.options.accessibility === !0 ? e.changeSlide({
				data: {
					message: "previous"
				}
			}) : 39 === t.keyCode && e.options.accessibility === !0 && e.changeSlide({
				data: {
					message: "next"
				}
			})
		}, e.prototype.loadSlider = function() {
			var t = this;
			t.setPosition(), t.$slideTrack.css({
					opacity: 1
				}), t.$slider.removeClass("slick-loading"), t.initUI(), "progressive" ===
				t.options.lazyLoad && t.progressiveLazyLoad()
		}, e.prototype.next = e.prototype.slickNext = function() {
			var t = this;
			t.changeSlide({
				data: {
					message: "next"
				}
			})
		}, e.prototype.orientationChange = function() {
			var t = this;
			t.checkResponsive(), t.setPosition()
		}, e.prototype.pause = e.prototype.slickPause = function() {
			var t = this;
			t.paused = !0
		}, e.prototype.play = e.prototype.slickPlay = function() {
			var t = this;
			t.paused = !1
		}, e.prototype.postSlide = function(t) {
			var e = this;
			e.$slider.trigger("afterChange", [e, t]), e.animating = !1, e.setPosition()
		}, e.prototype.prev = e.prototype.slickPrev = function() {
			var t = this;
			t.changeSlide({
				data: {
					message: "previous"
				}
			})
		}, e.prototype.preventDefault = function(t) {
			t.preventDefault()
		}, e.prototype.progressiveLazyLoad = function(e) {
			var i, n, o, r = this;
			if (i = t('.slide[data-lazy="true"]', r.$slider).length, i > 0) {
				if (e || (e = r.options.initialSlide), e == r.$slides.length && (e = 0),
					o = t(".slide[data-lazy]", r.$slider).eq(e), n = o.find(o.find(
						".video-slide").length ? ".video-slide .video-poster" : ".inside"),
					"false" == n.attr("data-lazy")) return;
				t("<img/>").attr("src", n.attr("data-lrg")).load(function() {
					t(this).remove(), o.attr("data-lazy", "false"), n.css(
						"background-image", "url(" + n.attr("data-lrg") + ")"), r.progressiveLazyLoad(
						e + 1)
				})
			}
		}, e.prototype.refresh = function(e) {
			var i = this,
				n = i.currentSlide;
			i.destroy(!0), t.extend(i, i.initials), i.init(), e || i.changeSlide({
				data: {
					message: "index",
					index: n
				}
			}, !1)
		}, e.prototype.reinit = function() {
			var e = this;
			e.$slides = e.$slideTrack.children(e.options.slide).addClass("slick-slide"),
				e.slideCount = e.$slides.length, e.currentSlide >= e.slideCount && 0 !==
				e.currentSlide && (e.currentSlide = e.currentSlide - e.options.slidesToScroll),
				e.slideCount <= e.options.slidesToShow && (e.currentSlide = 0), e.setProps(),
				e.updateArrows(), e.initArrowEvents(), e.options.focusOnSelect === !0 &&
				t(e.$slideTrack).children().on("click.slick", e.selectHandler), e.setSlideClasses(
					0), e.setPosition(), e.$slider.trigger("reInit", [e])
		}, e.prototype.resize = function() {
			var e = this;
			t(window).width() !== e.windowWidth && (clearTimeout(e.windowDelay), e.windowDelay =
				window.setTimeout(function() {
					e.windowWidth = t(window).width(), e.checkResponsive(), e.unslicked ||
						e.setPosition()
				}, 50))
		}, e.prototype.removeSlide = e.prototype.slickRemove = function(t, e, i) {
			var n = this;
			return "boolean" == typeof t ? (e = t, t = e === !0 ? 0 : n.slideCount - 1) :
				t = e === !0 ? --t : t, n.slideCount < 1 || 0 > t || t > n.slideCount - 1 ?
				!1 : (n.unload(), i === !0 ? n.$slideTrack.children().remove() : n.$slideTrack
					.children(this.options.slide).eq(t).remove(), n.$slides = n.$slideTrack.children(
						this.options.slide), n.$slideTrack.children(this.options.slide).detach(),
					n.$slideTrack.append(n.$slides), n.$slidesCache = n.$slides, void n.reinit()
				)
		}, e.prototype.setCSS = function(t) {
			var e, i, n = this,
				o = {};
			e = "left" == n.positionProp ? Math.ceil(t) + "px" : "0px", i = "top" == n
				.positionProp ? Math.ceil(t) + "px" : "0px", o[n.positionProp] = t, n.transformsEnabled ===
				!1 ? n.$slideTrack.css(o) : (o = {}, n.cssTransitions === !1 ? (o[n.animType] =
					"translate(" + e + ", " + i + ")", n.$slideTrack.css(o)) : (o[n.animType] =
					"translate3d(" + e + ", " + i + ", 0px)", n.$slideTrack.css(o)))
		}, e.prototype.setDimensions = function() {
			var t = this;
			t.listWidth = t.$list.width(), t.listHeight = t.$list.height(), t.slideWidth =
				Math.ceil(t.listWidth / t.options.slidesToShow), t.$slideTrack.width(Math
					.ceil(t.slideWidth * t.$slideTrack.children(".slick-slide").length));
			var e = t.$slides.first().outerWidth(!0) - t.$slides.first().width();
			t.$slideTrack.children(".slick-slide").width(t.slideWidth - e)
		}, e.prototype.setOption = e.prototype.slickSetOption = function(t, e, i) {
			var n = this;
			n.options[t] = e, i === !0 && (n.unload(), n.reinit())
		}, e.prototype.setPosition = function() {
			var t = this;
			t.setDimensions(), t.setCSS(t.getLeft(t.currentSlide)), t.$slider.trigger(
				"setPosition", [t])
		}, e.prototype.setProps = function() {
			var t = this,
				e = document.body.style;
			t.positionProp = "left", (void 0 !== e.WebkitTransition || void 0 !== e.MozTransition ||
					void 0 !== e.msTransition) && (t.cssTransitions = !0), void 0 !== e.OTransform &&
				(t.animType = "OTransform", t.transformType = "-o-transform", t.transitionType =
					"OTransition", void 0 === e.perspectiveProperty && void 0 === e.webkitPerspective &&
					(t.animType = !1)), void 0 !== e.MozTransform && (t.animType =
					"MozTransform", t.transformType = "-moz-transform", t.transitionType =
					"MozTransition", void 0 === e.perspectiveProperty && void 0 === e.MozPerspective &&
					(t.animType = !1)), void 0 !== e.webkitTransform && (t.animType =
					"webkitTransform", t.transformType = "-webkit-transform", t.transitionType =
					"webkitTransition", void 0 === e.perspectiveProperty && void 0 === e.webkitPerspective &&
					(t.animType = !1)), void 0 !== e.msTransform && (t.animType =
					"msTransform", t.transformType = "-ms-transform", t.transitionType =
					"msTransition", void 0 === e.msTransform && (t.animType = !1)), void 0 !==
				e.transform && t.animType !== !1 && (t.animType = "transform", t.transformType =
					"transform", t.transitionType = "transition"), t.transformsEnabled =
				null !== t.animType && t.animType !== !1
		}, e.prototype.setSlideClasses = function(t) {
			var e, i, n, o = this;
			o.$slider.find(".slick-slide").removeClass("slick-active").attr(
					"aria-hidden", "true"), e = o.$slider.find(".slick-slide"), t >= 0 && t <=
				o.slideCount - o.options.slidesToShow ? o.$slides.slice(t, t + o.options.slidesToShow)
				.addClass("slick-active").attr("aria-hidden", "false") : e.length <= o.options
				.slidesToShow ? e.addClass("slick-active").attr("aria-hidden", "false") :
				(n = o.slideCount % o.options.slidesToShow, i = t, o.options.slidesToShow ==
					o.options.slidesToScroll && o.slideCount - t < o.options.slidesToShow ?
					e.slice(i - (o.options.slidesToShow - n), i + n).addClass("slick-active")
					.attr("aria-hidden", "false") : e.slice(i, i + o.options.slidesToShow).addClass(
						"slick-active").attr("aria-hidden", "false")), "ondemand" === o.options
				.lazyLoad && o.lazyLoad()
		}, e.prototype.selectHandler = function(e) {
			var i = this,
				n = t(e.target).is(".slick-slide") ? t(e.target) : t(e.target).parents(
					".slick-slide"),
				o = parseInt(n.attr("data-slick-index"));
			return o || (o = 0), i.slideCount <= i.options.slidesToShow ? (i.$slider.find(
					".slick-slide").removeClass("slick-active").attr("aria-hidden", "true"),
				void i.$slides.eq(o).addClass("slick-active").attr("aria-hidden",
					"false")) : void i.slideHandler(o)
		}, e.prototype.slideHandler = function(t, e, i) {
			var n, o, r, s, a = null,
				l = this;
			if (e = e || !1, !(l.animating === !0 && l.options.waitForAnimate === !0 ||
				l.slideCount <= l.options.slidesToShow)) {
				if (n = t, a = l.getLeft(n), s = l.getLeft(l.currentSlide), l.currentLeft =
					s, 0 > t) return n = l.currentSlide, void(i !== !0 ? l.animateSlide(s,
					function() {
						l.postSlide(n)
					}) : l.postSlide(n));
				if (0 > t || t > l.slideCount - l.options.slidesToScroll) return n = l.currentSlide,
					void(i !== !0 ? l.animateSlide(s, function() {
						l.postSlide(n)
					}) : l.postSlide(n));
				o = 0 > n ? l.slideCount % l.options.slidesToScroll !== 0 ? l.slideCount -
					l.slideCount % l.options.slidesToScroll : l.slideCount + n : n >= l.slideCount ?
					l.slideCount % l.options.slidesToScroll !== 0 ? 0 : n - l.slideCount : n,
					l.animating = !0, l.$slider.trigger("beforeChange", [l, l.currentSlide,
						o
					]), r = l.currentSlide, l.currentSlide = o, l.setSlideClasses(l.currentSlide),
					l.updateArrows(), i !== !0 ? l.animateSlide(a, function() {
						l.postSlide(o)
					}) : l.postSlide(o)
			}
		}, e.prototype.startLoad = function() {
			var t = this;
			t.options.arrows === !0 && t.slideCount > t.options.slidesToShow && (t.$prevArrow
				.hide(), t.$nextArrow.hide()), t.$slider.addClass("slick-loading")
		}, e.prototype.unload = function() {
			var e = this;
			t(".slick-cloned", e.$slider).remove(), e.$prevArrow && "object" != typeof e
				.options.prevArrow && e.$prevArrow.remove(), e.$nextArrow && "object" !=
				typeof e.options.nextArrow && e.$nextArrow.remove(), e.$slides.removeClass(
					"slick-slide slick-active slick-visible").attr("aria-hidden", "true").css(
					"width", "")
		}, e.prototype.unslick = function(t) {
			var e = this;
			e.$slider.trigger("unslick", [e, t]), e.destroy()
		}, e.prototype.updateArrows = function() {
			var t = this;
			t.options.arrows === !0 && t.slideCount > t.options.slidesToShow && (t.$prevArrow
				.removeClass("slick-disabled"), t.$nextArrow.removeClass(
					"slick-disabled"), 0 === t.currentSlide ? (t.$prevArrow.addClass(
					"slick-disabled"), t.$nextArrow.removeClass("slick-disabled")) : t.currentSlide >=
				t.slideCount - t.options.slidesToShow && (t.$nextArrow.addClass(
					"slick-disabled"), t.$prevArrow.removeClass("slick-disabled")))
		}, e.prototype.visibility = function() {
			var t = this;
			document[t.hidden] && (t.paused = !0)
		}, t.fn.slick = function() {
			var t, i = this,
				n = arguments[0],
				o = Array.prototype.slice.call(arguments, 1),
				r = i.length,
				s = 0;
			for (s; r > s; s++)
				if ("object" == typeof n || "undefined" == typeof n ? i[s].slick = new e(
						i[s], n) : t = i[s].slick[n].apply(i[s].slick, o), "undefined" !=
					typeof t) return t;
			return i
		}
	}), ! function(t, e) {
		"function" == typeof define && define.amd ? define([], function() {
			return t.svg4everybody = e()
		}) : "object" == typeof exports ? module.exports = e() : t.svg4everybody = e()
	}(this, function() {
		function t(t, e) {
			if (e) {
				var i = !t.getAttribute("viewBox") && e.getAttribute("viewBox"),
					n = document.createDocumentFragment(),
					o = e.cloneNode(!0);
				for (i && t.setAttribute("viewBox", i); o.childNodes.length;) n.appendChild(
					o.firstChild);
				t.appendChild(n)
			}
		}

		function e(e) {
			e.onreadystatechange = function() {
				if (4 === e.readyState) {
					var i = document.createElement("x");
					i.innerHTML = e.responseText, e.s.splice(0).map(function(e) {
						t(e[0], i.querySelector("#" + e[1].replace(/(\W)/g, "\\$1")))
					})
				}
			}, e.onreadystatechange()
		}

		function i(i) {
			function n() {
				for (var i; i = o[0];) {
					var c = i.parentNode;
					if (c && /svg/i.test(c.nodeName)) {
						var u = i.getAttribute("xlink:href");
						if (r && (!s || s(u, c, i))) {
							var d = u.split("#"),
								p = d[0],
								f = d[1];
							if (c.removeChild(i), p.length) {
								var h = l[p] = l[p] || new XMLHttpRequest;
								h.s || (h.s = [], h.open("GET", p), h.send()), h.s.push([c, f]), e(h)
							} else t(c, document.getElementById(f))
						}
					}
				}
				a(n, 17)
			}
			i = i || {};
			var o = document.getElementsByTagName("use"),
				r = "polyfill" in i ? i.polyfill :
				/\bEdge\/12\b|\bTrident\/[567]\b|\bVersion\/7.0 Safari\b/.test(navigator.userAgent) ||
				(navigator.userAgent.match(/AppleWebKit\/(\d+)/) || [])[1] < 537,
				s = i.validate,
				a = window.requestAnimationFrame || setTimeout,
				l = {};
			r && n()
		}
		return i
	}), ! function(t) {
		function e(t) {
			var e = t.length,
				n = i.type(t);
			return "function" === n || i.isWindow(t) ? !1 : 1 === t.nodeType && e ? !0 :
				"array" === n || 0 === e || "number" == typeof e && e > 0 && e - 1 in t
		}
		if (!t.jQuery) {
			var i = function(t, e) {
				return new i.fn.init(t, e)
			};
			i.isWindow = function(t) {
				return null != t && t == t.window
			}, i.type = function(t) {
				return null == t ? t + "" : "object" == typeof t || "function" == typeof t ?
					o[s.call(t)] || "object" : typeof t
			}, i.isArray = Array.isArray || function(t) {
				return "array" === i.type(t)
			}, i.isPlainObject = function(t) {
				var e;
				if (!t || "object" !== i.type(t) || t.nodeType || i.isWindow(t)) return !1;
				try {
					if (t.constructor && !r.call(t, "constructor") && !r.call(t.constructor.prototype,
						"isPrototypeOf")) return !1
				} catch (n) {
					return !1
				}
				for (e in t);
				return void 0 === e || r.call(t, e)
			}, i.each = function(t, i, n) {
				var o, r = 0,
					s = t.length,
					a = e(t);
				if (n) {
					if (a)
						for (; s > r && (o = i.apply(t[r], n), o !== !1); r++);
					else
						for (r in t)
							if (o = i.apply(t[r], n), o === !1) break
				} else if (a)
					for (; s > r && (o = i.call(t[r], r, t[r]), o !== !1); r++);
				else
					for (r in t)
						if (o = i.call(t[r], r, t[r]), o === !1) break; return t
			}, i.data = function(t, e, o) {
				if (void 0 === o) {
					var r = t[i.expando],
						s = r && n[r];
					if (void 0 === e) return s;
					if (s && e in s) return s[e]
				} else if (void 0 !== e) {
					var r = t[i.expando] || (t[i.expando] = ++i.uuid);
					return n[r] = n[r] || {}, n[r][e] = o, o
				}
			}, i.removeData = function(t, e) {
				var o = t[i.expando],
					r = o && n[o];
				r && i.each(e, function(t, e) {
					delete r[e]
				})
			}, i.extend = function() {
				var t, e, n, o, r, s, a = arguments[0] || {},
					l = 1,
					c = arguments.length,
					u = !1;
				for ("boolean" == typeof a && (u = a, a = arguments[l] || {}, l++),
					"object" != typeof a && "function" !== i.type(a) && (a = {}), l === c &&
					(a = this, l--); c > l; l++)
					if (null != (r = arguments[l]))
						for (o in r) t = a[o], n = r[o], a !== n && (u && n && (i.isPlainObject(
								n) || (e = i.isArray(n))) ? (e ? (e = !1, s = t && i.isArray(t) ? t : []) :
								s = t && i.isPlainObject(t) ? t : {}, a[o] = i.extend(u, s, n)) :
							void 0 !== n && (a[o] = n));
				return a
			}, i.queue = function(t, n, o) {
				function r(t, i) {
					var n = i || [];
					return null != t && (e(Object(t)) ? ! function(t, e) {
						for (var i = +e.length, n = 0, o = t.length; i > n;) t[o++] = e[n++];
						if (i !== i)
							for (; void 0 !== e[n];) t[o++] = e[n++];
						return t.length = o, t
					}(n, "string" == typeof t ? [t] : t) : [].push.call(n, t)), n
				}
				if (t) {
					n = (n || "fx") + "queue";
					var s = i.data(t, n);
					return o ? (!s || i.isArray(o) ? s = i.data(t, n, r(o)) : s.push(o), s) :
						s || []
				}
			}, i.dequeue = function(t, e) {
				i.each(t.nodeType ? [t] : t, function(t, n) {
					e = e || "fx";
					var o = i.queue(n, e),
						r = o.shift();
					"inprogress" === r && (r = o.shift()), r && ("fx" === e && o.unshift(
						"inprogress"), r.call(n, function() {
						i.dequeue(n, e)
					}))
				})
			}, i.fn = i.prototype = {
				init: function(t) {
					if (t.nodeType) return this[0] = t, this;
					throw new Error("Not a DOM node.")
				},
				offset: function() {
					var e = this[0].getBoundingClientRect ? this[0].getBoundingClientRect() : {
						top: 0,
						left: 0
					};
					return {
						top: e.top + (t.pageYOffset || document.scrollTop || 0) - (document.clientTop ||
							0),
						left: e.left + (t.pageXOffset || document.scrollLeft || 0) - (document.clientLeft ||
							0)
					}
				},
				position: function() {
					function t() {
						for (var t = this.offsetParent || document; t && "html" === !t.nodeType
							.toLowerCase && "static" === t.style.position;) t = t.offsetParent;
						return t || document
					}
					var e = this[0],
						t = t.apply(e),
						n = this.offset(),
						o = /^(?:body|html)$/i.test(t.nodeName) ? {
							top: 0,
							left: 0
						} : i(t).offset();
					return n.top -= parseFloat(e.style.marginTop) || 0, n.left -= parseFloat(
						e.style.marginLeft) || 0, t.style && (o.top += parseFloat(t.style.borderTopWidth) ||
						0, o.left += parseFloat(t.style.borderLeftWidth) || 0), {
						top: n.top - o.top,
						left: n.left - o.left
					}
				}
			};
			var n = {};
			i.expando = "velocity" + (new Date).getTime(), i.uuid = 0;
			for (var o = {}, r = o.hasOwnProperty, s = o.toString, a =
				"Boolean Number String Function Array Date RegExp Object Error".split(" "),
				l = 0; l < a.length; l++) o["[object " + a[l] + "]"] = a[l].toLowerCase();
			i.fn.init.prototype = i.fn, t.Velocity = {
				Utilities: i
			}
		}
	}(window),
	function(t) {
		"object" == typeof module && "object" == typeof module.exports ? module.exports =
			t() : "function" == typeof define && define.amd ? define(t) : t()
	}(function() {
		return function(t, e, i, n) {
			function o(t) {
				for (var e = -1, i = t ? t.length : 0, n = []; ++e < i;) {
					var o = t[e];
					o && n.push(o)
				}
				return n
			}

			function r(t) {
				return g.isWrapped(t) ? t = [].slice.call(t) : g.isNode(t) && (t = [t]),
					t
			}

			function s(t) {
				var e = p.data(t, "velocity");
				return null === e ? n : e
			}

			function a(t) {
				return function(e) {
					return Math.round(e * t) * (1 / t)
				}
			}

			function l(t, i, n, o) {
				function r(t, e) {
					return 1 - 3 * e + 3 * t
				}

				function s(t, e) {
					return 3 * e - 6 * t
				}

				function a(t) {
					return 3 * t
				}

				function l(t, e, i) {
					return ((r(e, i) * t + s(e, i)) * t + a(e)) * t
				}

				function c(t, e, i) {
					return 3 * r(e, i) * t * t + 2 * s(e, i) * t + a(e)
				}

				function u(e, i) {
					for (var o = 0; g > o; ++o) {
						var r = c(i, t, n);
						if (0 === r) return i;
						var s = l(i, t, n) - e;
						i -= s / r
					}
					return i
				}

				function d() {
					for (var e = 0; b > e; ++e) T[e] = l(e * w, t, n)
				}

				function p(e, i, o) {
					var r, s, a = 0;
					do s = i + (o - i) / 2, r = l(s, t, n) - e, r > 0 ? o = s : i = s; while (
						Math.abs(r) > y && ++a < v);
					return s
				}

				function f(e) {
					for (var i = 0, o = 1, r = b - 1; o != r && T[o] <= e; ++o) i += w;
					--o;
					var s = (e - T[o]) / (T[o + 1] - T[o]),
						a = i + s * w,
						l = c(a, t, n);
					return l >= m ? u(e, a) : 0 == l ? a : p(e, i, i + w)
				}

				function h() {
					k = !0, (t != i || n != o) && d()
				}
				var g = 4,
					m = .001,
					y = 1e-7,
					v = 10,
					b = 11,
					w = 1 / (b - 1),
					x = "Float32Array" in e;
				if (4 !== arguments.length) return !1;
				for (var S = 0; 4 > S; ++S)
					if ("number" != typeof arguments[S] || isNaN(arguments[S]) || !isFinite(
						arguments[S])) return !1;
				t = Math.min(t, 1), n = Math.min(n, 1), t = Math.max(t, 0), n = Math.max(
					n, 0);
				var T = x ? new Float32Array(b) : new Array(b),
					k = !1,
					C = function(e) {
						return k || h(), t === i && n === o ? e : 0 === e ? 0 : 1 === e ? 1 :
							l(f(e), i, o)
					};
				C.getControlPoints = function() {
					return [{
						x: t,
						y: i
					}, {
						x: n,
						y: o
					}]
				};
				var P = "generateBezier(" + [t, i, n, o] + ")";
				return C.toString = function() {
					return P
				}, C
			}

			function c(t, e) {
				var i = t;
				return g.isString(t) ? b.Easings[t] || (i = !1) : i = g.isArray(t) && 1 ===
					t.length ? a.apply(null, t) : g.isArray(t) && 2 === t.length ? w.apply(
						null, t.concat([e])) : g.isArray(t) && 4 === t.length ? l.apply(null,
						t) : !1, i === !1 && (i = b.Easings[b.defaults.easing] ? b.defaults.easing :
						v), i
			}

			function u(t) {
				if (t) {
					var e = (new Date).getTime(),
						i = b.State.calls.length;
					i > 1e4 && (b.State.calls = o(b.State.calls));
					for (var r = 0; i > r; r++)
						if (b.State.calls[r]) {
							var a = b.State.calls[r],
								l = a[0],
								c = a[2],
								f = a[3],
								h = !!f,
								m = null;
							f || (f = b.State.calls[r][3] = e - 16);
							for (var y = Math.min((e - f) / c.duration, 1), v = 0, w = l.length; w >
								v; v++) {
								var S = l[v],
									k = S.element;
								if (s(k)) {
									var C = !1;
									if (c.display !== n && null !== c.display && "none" !== c.display) {
										if ("flex" === c.display) {
											var P = ["-webkit-box", "-moz-box", "-ms-flexbox", "-webkit-flex"];
											p.each(P, function(t, e) {
												x.setPropertyValue(k, "display", e)
											})
										}
										x.setPropertyValue(k, "display", c.display)
									}
									c.visibility !== n && "hidden" !== c.visibility && x.setPropertyValue(
										k, "visibility", c.visibility);
									for (var O in S)
										if ("element" !== O) {
											var E, A = S[O],
												L = g.isString(A.easing) ? b.Easings[A.easing] : A.easing;
											if (1 === y) E = A.endValue;
											else {
												var $ = A.endValue - A.startValue;
												if (E = A.startValue + $ * L(y, c, $), !h && E === A.currentValue)
													continue
											} if (A.currentValue = E, "tween" === O) m = E;
											else {
												if (x.Hooks.registered[O]) {
													var _ = x.Hooks.getRoot(O),
														z = s(k).rootPropertyValueCache[_];
													z && (A.rootPropertyValue = z)
												}
												var V = x.setPropertyValue(k, O, A.currentValue + (0 ===
													parseFloat(E) ? "" : A.unitType), A.rootPropertyValue, A.scrollData);
												x.Hooks.registered[O] && (s(k).rootPropertyValueCache[_] = x.Normalizations
													.registered[_] ? x.Normalizations.registered[_]("extract",
														null, V[1]) : V[1]), "transform" === V[0] && (C = !0)
											}
										}
									c.mobileHA && s(k).transformCache.translate3d === n && (s(k).transformCache
										.translate3d = "(0px, 0px, 0px)", C = !0), C && x.flushTransformCache(
										k)
								}
							}
							c.display !== n && "none" !== c.display && (b.State.calls[r][2].display = !
								1), c.visibility !== n && "hidden" !== c.visibility && (b.State.calls[
								r][2].visibility = !1), c.progress && c.progress.call(a[1], a[1], y,
								Math.max(0, f + c.duration - e), f, m), 1 === y && d(r)
						}
				}
				b.State.isTicking && T(u)
			}

			function d(t, e) {
				if (!b.State.calls[t]) return !1;
				for (var i = b.State.calls[t][0], o = b.State.calls[t][1], r = b.State.calls[
					t][2], a = b.State.calls[t][4], l = !1, c = 0, u = i.length; u > c; c++) {
					var d = i[c].element;
					if (e || r.loop || ("none" === r.display && x.setPropertyValue(d,
						"display", r.display), "hidden" === r.visibility && x.setPropertyValue(
						d, "visibility", r.visibility)), r.loop !== !0 && (p.queue(d)[1] ===
						n || !/\.velocityQueueEntryFlag/i.test(p.queue(d)[1])) && s(d)) {
						s(d).isAnimating = !1, s(d).rootPropertyValueCache = {};
						var f = !1;
						p.each(x.Lists.transforms3D, function(t, e) {
								var i = /^scale/.test(e) ? 1 : 0,
									o = s(d).transformCache[e];
								s(d).transformCache[e] !== n && new RegExp("^\\(" + i + "[^.]").test(
									o) && (f = !0, delete s(d).transformCache[e])
							}), r.mobileHA && (f = !0, delete s(d).transformCache.translate3d), f &&
							x.flushTransformCache(d), x.Values.removeClass(d,
								"velocity-animating")
					}
					if (!e && r.complete && !r.loop && c === u - 1) try {
						r.complete.call(o, o)
					} catch (h) {
						setTimeout(function() {
							throw h
						}, 1)
					}
					a && r.loop !== !0 && a(o), s(d) && r.loop === !0 && !e && (p.each(s(d)
						.tweensContainer, function(t, e) {
							/^rotate/.test(t) && 360 === parseFloat(e.endValue) && (e.endValue =
									0, e.startValue = 360), /^backgroundPosition/.test(t) && 100 ===
								parseFloat(e.endValue) && "%" === e.unitType && (e.endValue = 0, e
									.startValue = 100)
						}), b(d, "reverse", {
						loop: !0,
						delay: r.delay
					})), r.queue !== !1 && p.dequeue(d, r.queue)
				}
				b.State.calls[t] = !1;
				for (var g = 0, m = b.State.calls.length; m > g; g++)
					if (b.State.calls[g] !== !1) {
						l = !0;
						break
					}
				l === !1 && (b.State.isTicking = !1, delete b.State.calls, b.State.calls = [])
			}
			var p, f = function() {
					if (i.documentMode) return i.documentMode;
					for (var t = 7; t > 4; t--) {
						var e = i.createElement("div");
						if (e.innerHTML = "<!--[if IE " + t + "]><span></span><![endif]-->", e.getElementsByTagName(
							"span").length) return e = null, t
					}
					return n
				}(),
				h = function() {
					var t = 0;
					return e.webkitRequestAnimationFrame || e.mozRequestAnimationFrame ||
						function(e) {
							var i, n = (new Date).getTime();
							return i = Math.max(0, 16 - (n - t)), t = n + i, setTimeout(function() {
								e(n + i)
							}, i)
						}
				}(),
				g = {
					isString: function(t) {
						return "string" == typeof t
					},
					isArray: Array.isArray || function(t) {
						return "[object Array]" === Object.prototype.toString.call(t)
					},
					isFunction: function(t) {
						return "[object Function]" === Object.prototype.toString.call(t)
					},
					isNode: function(t) {
						return t && t.nodeType
					},
					isNodeList: function(t) {
						return "object" == typeof t &&
							/^\[object (HTMLCollection|NodeList|Object)\]$/.test(Object.prototype
								.toString.call(t)) && t.length !== n && (0 === t.length || "object" ==
								typeof t[0] && t[0].nodeType > 0)
					},
					isWrapped: function(t) {
						return t && (t.jquery || e.Zepto && e.Zepto.zepto.isZ(t))
					},
					isSVG: function(t) {
						return e.SVGElement && t instanceof e.SVGElement
					},
					isEmptyObject: function(t) {
						for (var e in t) return !1;
						return !0
					}
				},
				m = !1;
			if (t.fn && t.fn.jquery ? (p = t, m = !0) : p = e.Velocity.Utilities, 8 >=
				f && !m) throw new Error(
				"Velocity: IE8 and below require jQuery to be loaded before Velocity.");
			if (7 >= f) return void(jQuery.fn.velocity = jQuery.fn.animate);
			var y = 400,
				v = "swing",
				b = {
					State: {
						isMobile: /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i
							.test(navigator.userAgent),
						isAndroid: /Android/i.test(navigator.userAgent),
						isGingerbread: /Android 2\.3\.[3-7]/i.test(navigator.userAgent),
						isChrome: e.chrome,
						isFirefox: /Firefox/i.test(navigator.userAgent),
						prefixElement: i.createElement("div"),
						prefixMatches: {},
						scrollAnchor: null,
						scrollPropertyLeft: null,
						scrollPropertyTop: null,
						isTicking: !1,
						calls: []
					},
					CSS: {},
					Utilities: p,
					Redirects: {},
					Easings: {},
					Promise: e.Promise,
					defaults: {
						queue: "",
						duration: y,
						easing: v,
						begin: n,
						complete: n,
						progress: n,
						display: n,
						visibility: n,
						loop: !1,
						delay: !1,
						mobileHA: !0,
						_cacheValues: !0
					},
					init: function(t) {
						p.data(t, "velocity", {
							isSVG: g.isSVG(t),
							isAnimating: !1,
							computedStyle: null,
							tweensContainer: null,
							rootPropertyValueCache: {},
							transformCache: {}
						})
					},
					hook: null,
					mock: !1,
					version: {
						major: 1,
						minor: 2,
						patch: 2
					},
					debug: !1
				};
			e.pageYOffset !== n ? (b.State.scrollAnchor = e, b.State.scrollPropertyLeft =
				"pageXOffset", b.State.scrollPropertyTop = "pageYOffset") : (b.State.scrollAnchor =
				i.documentElement || i.body.parentNode || i.body, b.State.scrollPropertyLeft =
				"scrollLeft", b.State.scrollPropertyTop = "scrollTop");
			var w = function() {
				function t(t) {
					return -t.tension * t.x - t.friction * t.v
				}

				function e(e, i, n) {
					var o = {
						x: e.x + n.dx * i,
						v: e.v + n.dv * i,
						tension: e.tension,
						friction: e.friction
					};
					return {
						dx: o.v,
						dv: t(o)
					}
				}

				function i(i, n) {
					var o = {
							dx: i.v,
							dv: t(i)
						},
						r = e(i, .5 * n, o),
						s = e(i, .5 * n, r),
						a = e(i, n, s),
						l = 1 / 6 * (o.dx + 2 * (r.dx + s.dx) + a.dx),
						c = 1 / 6 * (o.dv + 2 * (r.dv + s.dv) + a.dv);
					return i.x = i.x + l * n, i.v = i.v + c * n, i
				}
				return function n(t, e, o) {
					var r, s, a, l = {
							x: -1,
							v: 0,
							tension: null,
							friction: null
						},
						c = [0],
						u = 0,
						d = 1e-4,
						p = .016;
					for (t = parseFloat(t) || 500, e = parseFloat(e) || 20, o = o || null,
						l.tension = t, l.friction = e, r = null !== o, r ? (u = n(t, e), s =
							u / o * p) : s = p; a = i(a || l, s), c.push(1 + a.x), u += 16, Math
						.abs(a.x) > d && Math.abs(a.v) > d;);
					return r ? function(t) {
						return c[t * (c.length - 1) | 0]
					} : u
				}
			}();
			b.Easings = {
				linear: function(t) {
					return t
				},
				swing: function(t) {
					return .5 - Math.cos(t * Math.PI) / 2
				},
				spring: function(t) {
					return 1 - Math.cos(4.5 * t * Math.PI) * Math.exp(6 * -t)
				}
			}, p.each([
				["ease", [.25, .1, .25, 1]],
				["ease-in", [.42, 0, 1, 1]],
				["ease-out", [0, 0, .58, 1]],
				["ease-in-out", [.42, 0, .58, 1]],
				["easeInSine", [.47, 0, .745, .715]],
				["easeOutSine", [.39, .575, .565, 1]],
				["easeInOutSine", [.445, .05, .55, .95]],
				["easeInQuad", [.55, .085, .68, .53]],
				["easeOutQuad", [.25, .46, .45, .94]],
				["easeInOutQuad", [.455, .03, .515, .955]],
				["easeInCubic", [.55, .055, .675, .19]],
				["easeOutCubic", [.215, .61, .355, 1]],
				["easeInOutCubic", [.645, .045, .355, 1]],
				["easeInQuart", [.895, .03, .685, .22]],
				["easeOutQuart", [.165, .84, .44, 1]],
				["easeInOutQuart", [.77, 0, .175, 1]],
				["easeInQuint", [.755, .05, .855, .06]],
				["easeOutQuint", [.23, 1, .32, 1]],
				["easeInOutQuint", [.86, 0, .07, 1]],
				["easeInExpo", [.95, .05, .795, .035]],
				["easeOutExpo", [.19, 1, .22, 1]],
				["easeInOutExpo", [1, 0, 0, 1]],
				["easeInCirc", [.6, .04, .98, .335]],
				["easeOutCirc", [.075, .82, .165, 1]],
				["easeInOutCirc", [.785, .135, .15, .86]]
			], function(t, e) {
				b.Easings[e[0]] = l.apply(null, e[1])
			});
			var x = b.CSS = {
				RegEx: {
					isHex: /^#([A-f\d]{3}){1,2}$/i,
					valueUnwrap: /^[A-z]+\((.*)\)$/i,
					wrappedValueAlreadyExtracted: /[0-9.]+ [0-9.]+ [0-9.]+( [0-9.]+)?/,
					valueSplit: /([A-z]+\(.+\))|(([A-z0-9#-.]+?)(?=\s|$))/gi
				},
				Lists: {
					colors: ["fill", "stroke", "stopColor", "color", "backgroundColor",
						"borderColor", "borderTopColor", "borderRightColor",
						"borderBottomColor", "borderLeftColor", "outlineColor"
					],
					transformsBase: ["translateX", "translateY", "scale", "scaleX",
						"scaleY", "skewX", "skewY", "rotateZ"
					],
					transforms3D: ["transformPerspective", "translateZ", "scaleZ",
						"rotateX", "rotateY"
					]
				},
				Hooks: {
					templates: {
						textShadow: ["Color X Y Blur", "black 0px 0px 0px"],
						boxShadow: ["Color X Y Blur Spread", "black 0px 0px 0px 0px"],
						clip: ["Top Right Bottom Left", "0px 0px 0px 0px"],
						backgroundPosition: ["X Y", "0% 0%"],
						transformOrigin: ["X Y Z", "50% 50% 0px"],
						perspectiveOrigin: ["X Y", "50% 50%"]
					},
					registered: {},
					register: function() {
						for (var t = 0; t < x.Lists.colors.length; t++) {
							var e = "color" === x.Lists.colors[t] ? "0 0 0 1" : "255 255 255 1";
							x.Hooks.templates[x.Lists.colors[t]] = ["Red Green Blue Alpha", e]
						}
						var i, n, o;
						if (f)
							for (i in x.Hooks.templates) {
								n = x.Hooks.templates[i], o = n[0].split(" ");
								var r = n[1].match(x.RegEx.valueSplit);
								"Color" === o[0] && (o.push(o.shift()), r.push(r.shift()), x.Hooks.templates[
									i] = [o.join(" "), r.join(" ")])
							}
						for (i in x.Hooks.templates) {
							n = x.Hooks.templates[i], o = n[0].split(" ");
							for (var t in o) {
								var s = i + o[t],
									a = t;
								x.Hooks.registered[s] = [i, a]
							}
						}
					},
					getRoot: function(t) {
						var e = x.Hooks.registered[t];
						return e ? e[0] : t
					},
					cleanRootPropertyValue: function(t, e) {
						return x.RegEx.valueUnwrap.test(e) && (e = e.match(x.RegEx.valueUnwrap)[
							1]), x.Values.isCSSNullValue(e) && (e = x.Hooks.templates[t][1]), e
					},
					extractValue: function(t, e) {
						var i = x.Hooks.registered[t];
						if (i) {
							var n = i[0],
								o = i[1];
							return e = x.Hooks.cleanRootPropertyValue(n, e), e.toString().match(
								x.RegEx.valueSplit)[o]
						}
						return e
					},
					injectValue: function(t, e, i) {
						var n = x.Hooks.registered[t];
						if (n) {
							var o, r, s = n[0],
								a = n[1];
							return i = x.Hooks.cleanRootPropertyValue(s, i), o = i.toString().match(
								x.RegEx.valueSplit), o[a] = e, r = o.join(" ")
						}
						return i
					}
				},
				Normalizations: {
					registered: {
						clip: function(t, e, i) {
							switch (t) {
								case "name":
									return "clip";
								case "extract":
									var n;
									return x.RegEx.wrappedValueAlreadyExtracted.test(i) ? n = i : (n =
										i.toString().match(x.RegEx.valueUnwrap), n = n ? n[1].replace(
											/,(\s+)?/g, " ") : i), n;
								case "inject":
									return "rect(" + i + ")"
							}
						},
						blur: function(t, e, i) {
							switch (t) {
								case "name":
									return b.State.isFirefox ? "filter" : "-webkit-filter";
								case "extract":
									var n = parseFloat(i);
									if (!n && 0 !== n) {
										var o = i.toString().match(/blur\(([0-9]+[A-z]+)\)/i);
										n = o ? o[1] : 0
									}
									return n;
								case "inject":
									return parseFloat(i) ? "blur(" + i + ")" : "none"
							}
						},
						opacity: function(t, e, i) {
							if (8 >= f) switch (t) {
								case "name":
									return "filter";
								case "extract":
									var n = i.toString().match(/alpha\(opacity=(.*)\)/i);
									return i = n ? n[1] / 100 : 1;
								case "inject":
									return e.style.zoom = 1, parseFloat(i) >= 1 ? "" :
										"alpha(opacity=" + parseInt(100 * parseFloat(i), 10) + ")"
							} else switch (t) {
								case "name":
									return "opacity";
								case "extract":
									return i;
								case "inject":
									return i
							}
						}
					},
					register: function() {
						9 >= f || b.State.isGingerbread || (x.Lists.transformsBase = x.Lists.transformsBase
							.concat(x.Lists.transforms3D));
						for (var t = 0; t < x.Lists.transformsBase.length; t++)! function() {
							var e = x.Lists.transformsBase[t];
							x.Normalizations.registered[e] = function(t, i, o) {
								switch (t) {
									case "name":
										return "transform";
									case "extract":
										return s(i) === n || s(i).transformCache[e] === n ? /^scale/i.test(
											e) ? 1 : 0 : s(i).transformCache[e].replace(/[()]/g, "");
									case "inject":
										var r = !1;
										switch (e.substr(0, e.length - 1)) {
											case "translate":
												r = !/(%|px|em|rem|vw|vh|\d)$/i.test(o);
												break;
											case "scal":
											case "scale":
												b.State.isAndroid && s(i).transformCache[e] === n && 1 > o &&
													(o = 1), r = !/(\d)$/i.test(o);
												break;
											case "skew":
												r = !/(deg|\d)$/i.test(o);
												break;
											case "rotate":
												r = !/(deg|\d)$/i.test(o)
										}
										return r || (s(i).transformCache[e] = "(" + o + ")"), s(i).transformCache[
											e]
								}
							}
						}();
						for (var t = 0; t < x.Lists.colors.length; t++)! function() {
							var e = x.Lists.colors[t];
							x.Normalizations.registered[e] = function(t, i, o) {
								switch (t) {
									case "name":
										return e;
									case "extract":
										var r;
										if (x.RegEx.wrappedValueAlreadyExtracted.test(o)) r = o;
										else {
											var s, a = {
												black: "rgb(0, 0, 0)",
												blue: "rgb(0, 0, 255)",
												gray: "rgb(128, 128, 128)",
												green: "rgb(0, 128, 0)",
												red: "rgb(255, 0, 0)",
												white: "rgb(255, 255, 255)"
											};
											/^[A-z]+$/i.test(o) ? s = a[o] !== n ? a[o] : a.black : x.RegEx
												.isHex.test(o) ? s = "rgb(" + x.Values.hexToRgb(o).join(" ") +
												")" : /^rgba?\(/i.test(o) || (s = a.black), r = (s || o).toString()
												.match(x.RegEx.valueUnwrap)[1].replace(/,(\s+)?/g, " ")
										}
										return 8 >= f || 3 !== r.split(" ").length || (r += " 1"), r;
									case "inject":
										return 8 >= f ? 4 === o.split(" ").length && (o = o.split(/\s+/)
											.slice(0, 3).join(" ")) : 3 === o.split(" ").length && (o +=
											" 1"), (8 >= f ? "rgb" : "rgba") + "(" + o.replace(/\s+/g,
											",").replace(/\.(\d)+(?=,)/g, "") + ")"
								}
							}
						}()
					}
				},
				Names: {
					camelCase: function(t) {
						return t.replace(/-(\w)/g, function(t, e) {
							return e.toUpperCase()
						})
					},
					SVGAttribute: function(t) {
						var e = "width|height|x|y|cx|cy|r|rx|ry|x1|x2|y1|y2";
						return (f || b.State.isAndroid && !b.State.isChrome) && (e +=
							"|transform"), new RegExp("^(" + e + ")$", "i").test(t)
					},
					prefixCheck: function(t) {
						if (b.State.prefixMatches[t]) return [b.State.prefixMatches[t], !0];
						for (var e = ["", "Webkit", "Moz", "ms", "O"], i = 0, n = e.length; n >
							i; i++) {
							var o;
							if (o = 0 === i ? t : e[i] + t.replace(/^\w/, function(t) {
								return t.toUpperCase()
							}), g.isString(b.State.prefixElement.style[o])) return b.State.prefixMatches[
								t] = o, [o, !0]
						}
						return [t, !1]
					}
				},
				Values: {
					hexToRgb: function(t) {
						var e, i = /^#?([a-f\d])([a-f\d])([a-f\d])$/i,
							n = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i;
						return t = t.replace(i, function(t, e, i, n) {
							return e + e + i + i + n + n
						}), e = n.exec(t), e ? [parseInt(e[1], 16), parseInt(e[2], 16),
							parseInt(e[3], 16)
						] : [0, 0, 0]
					},
					isCSSNullValue: function(t) {
						return 0 == t || /^(none|auto|transparent|(rgba\(0, ?0, ?0, ?0\)))$/i
							.test(t)
					},
					getUnitType: function(t) {
						return /^(rotate|skew)/i.test(t) ? "deg" :
							/(^(scale|scaleX|scaleY|scaleZ|alpha|flexGrow|flexHeight|zIndex|fontWeight)$)|((opacity|red|green|blue|alpha)$)/i
							.test(t) ? "" : "px"
					},
					getDisplayType: function(t) {
						var e = t && t.tagName.toString().toLowerCase();
						return
							/^(b|big|i|small|tt|abbr|acronym|cite|code|dfn|em|kbd|strong|samp|var|a|bdo|br|img|map|object|q|script|span|sub|sup|button|input|label|select|textarea)$/i
							.test(e) ? "inline" : /^(li)$/i.test(e) ? "list-item" : /^(tr)$/i.test(
								e) ? "table-row" : /^(table)$/i.test(e) ? "table" : /^(tbody)$/i.test(
								e) ? "table-row-group" : "block"
					},
					addClass: function(t, e) {
						t.classList ? t.classList.add(e) : t.className += (t.className.length ?
							" " : "") + e
					},
					removeClass: function(t, e) {
						t.classList ? t.classList.remove(e) : t.className = t.className.toString()
							.replace(new RegExp("(^|\\s)" + e.split(" ").join("|") + "(\\s|$)",
								"gi"), " ")
					}
				},
				getPropertyValue: function(t, i, o, r) {
					function a(t, i) {
						function o() {
							c && x.setPropertyValue(t, "display", "none")
						}
						var l = 0;
						if (8 >= f) l = p.css(t, i);
						else {
							var c = !1;
							if (/^(width|height)$/.test(i) && 0 === x.getPropertyValue(t,
								"display") && (c = !0, x.setPropertyValue(t, "display", x.Values.getDisplayType(
								t))), !r) {
								if ("height" === i && "border-box" !== x.getPropertyValue(t,
									"boxSizing").toString().toLowerCase()) {
									var u = t.offsetHeight - (parseFloat(x.getPropertyValue(t,
										"borderTopWidth")) || 0) - (parseFloat(x.getPropertyValue(t,
										"borderBottomWidth")) || 0) - (parseFloat(x.getPropertyValue(t,
										"paddingTop")) || 0) - (parseFloat(x.getPropertyValue(t,
										"paddingBottom")) || 0);
									return o(), u
								}
								if ("width" === i && "border-box" !== x.getPropertyValue(t,
									"boxSizing").toString().toLowerCase()) {
									var d = t.offsetWidth - (parseFloat(x.getPropertyValue(t,
										"borderLeftWidth")) || 0) - (parseFloat(x.getPropertyValue(t,
										"borderRightWidth")) || 0) - (parseFloat(x.getPropertyValue(t,
										"paddingLeft")) || 0) - (parseFloat(x.getPropertyValue(t,
										"paddingRight")) || 0);
									return o(), d
								}
							}
							var h;
							h = s(t) === n ? e.getComputedStyle(t, null) : s(t).computedStyle ?
								s(t).computedStyle : s(t).computedStyle = e.getComputedStyle(t,
									null), "borderColor" === i && (i = "borderTopColor"), l = 9 === f &&
								"filter" === i ? h.getPropertyValue(i) : h[i], ("" === l || null ===
									l) && (l = t.style[i]), o()
						} if ("auto" === l && /^(top|right|bottom|left)$/i.test(i)) {
							var g = a(t, "position");
							("fixed" === g || "absolute" === g && /top|left/i.test(i)) && (l =
								p(t).position()[i] + "px")
						}
						return l
					}
					var l;
					if (x.Hooks.registered[i]) {
						var c = i,
							u = x.Hooks.getRoot(c);
						o === n && (o = x.getPropertyValue(t, x.Names.prefixCheck(u)[0])), x.Normalizations
							.registered[u] && (o = x.Normalizations.registered[u]("extract", t,
								o)), l = x.Hooks.extractValue(c, o)
					} else if (x.Normalizations.registered[i]) {
						var d, h;
						d = x.Normalizations.registered[i]("name", t), "transform" !== d && (
								h = a(t, x.Names.prefixCheck(d)[0]), x.Values.isCSSNullValue(h) &&
								x.Hooks.templates[i] && (h = x.Hooks.templates[i][1])), l = x.Normalizations
							.registered[i]("extract", t, h)
					}
					if (!/^[\d-]/.test(l))
						if (s(t) && s(t).isSVG && x.Names.SVGAttribute(i))
							if (/^(height|width)$/i.test(i)) try {
								l = t.getBBox()[i]
							} catch (g) {
								l = 0
							} else l = t.getAttribute(i);
							else l = a(t, x.Names.prefixCheck(i)[0]);
					return x.Values.isCSSNullValue(l) && (l = 0), b.debug >= 2 && console.log(
						"Get " + i + ": " + l), l
				},
				setPropertyValue: function(t, i, n, o, r) {
					var a = i;
					if ("scroll" === i) r.container ? r.container["scroll" + r.direction] =
						n : "Left" === r.direction ? e.scrollTo(n, r.alternateValue) : e.scrollTo(
							r.alternateValue, n);
					else if (x.Normalizations.registered[i] && "transform" === x.Normalizations
						.registered[i]("name", t)) x.Normalizations.registered[i]("inject", t,
						n), a = "transform", n = s(t).transformCache[i];
					else {
						if (x.Hooks.registered[i]) {
							var l = i,
								c = x.Hooks.getRoot(i);
							o = o || x.getPropertyValue(t, c), n = x.Hooks.injectValue(l, n, o),
								i = c
						}
						if (x.Normalizations.registered[i] && (n = x.Normalizations.registered[
								i]("inject", t, n), i = x.Normalizations.registered[i]("name", t)),
							a = x.Names.prefixCheck(i)[0], 8 >= f) try {
							t.style[a] = n
						} catch (u) {
							b.debug && console.log("Browser does not support [" + n + "] for [" +
								a + "]")
						} else s(t) && s(t).isSVG && x.Names.SVGAttribute(i) ? t.setAttribute(
							i, n) : t.style[a] = n;
						b.debug >= 2 && console.log("Set " + i + " (" + a + "): " + n)
					}
					return [a, n]
				},
				flushTransformCache: function(t) {
					function e(e) {
						return parseFloat(x.getPropertyValue(t, e))
					}
					var i = "";
					if ((f || b.State.isAndroid && !b.State.isChrome) && s(t).isSVG) {
						var n = {
							translate: [e("translateX"), e("translateY")],
							skewX: [e("skewX")],
							skewY: [e("skewY")],
							scale: 1 !== e("scale") ? [e("scale"), e("scale")] : [e("scaleX"),
								e("scaleY")
							],
							rotate: [e("rotateZ"), 0, 0]
						};
						p.each(s(t).transformCache, function(t) {
							/^translate/i.test(t) ? t = "translate" : /^scale/i.test(t) ? t =
								"scale" : /^rotate/i.test(t) && (t = "rotate"), n[t] && (i += t +
									"(" + n[t].join(" ") + ") ", delete n[t])
						})
					} else {
						var o, r;
						p.each(s(t).transformCache, function(e) {
							return o = s(t).transformCache[e], "transformPerspective" === e ?
								(r = o, !0) : (9 === f && "rotateZ" === e && (e = "rotate"), void(
									i += e + o + " "))
						}), r && (i = "perspective" + r + " " + i)
					}
					x.setPropertyValue(t, "transform", i)
				}
			};
			x.Hooks.register(), x.Normalizations.register(), b.hook = function(t, e, i) {
				var o = n;
				return t = r(t), p.each(t, function(t, r) {
					if (s(r) === n && b.init(r), i === n) o === n && (o = b.CSS.getPropertyValue(
						r, e));
					else {
						var a = b.CSS.setPropertyValue(r, e, i);
						"transform" === a[0] && b.CSS.flushTransformCache(r), o = a
					}
				}), o
			};
			var S = function() {
				function t() {
					return a ? O.promise || null : l
				}

				function o() {
					function t() {
						function t(t, e) {
							var i = n,
								o = n,
								s = n;
							return g.isArray(t) ? (i = t[0], !g.isArray(t[1]) && /^[\d-]/.test(
								t[1]) || g.isFunction(t[1]) || x.RegEx.isHex.test(t[1]) ? s = t[
								1] : (g.isString(t[1]) && !x.RegEx.isHex.test(t[1]) || g.isArray(
								t[1])) && (o = e ? t[1] : c(t[1], a.duration), t[2] !== n && (s =
								t[2]))) : i = t, e || (o = o || a.easing), g.isFunction(i) && (i =
								i.call(r, k, T)), g.isFunction(s) && (s = s.call(r, k, T)), [i ||
								0, o, s
							]
						}

						function d(t, e) {
							var i, n;
							return n = (e || "0").toString().toLowerCase().replace(/[%A-z]+$/,
								function(t) {
									return i = t, ""
								}), i || (i = x.Values.getUnitType(t)), [n, i]
						}

						function f() {
							var t = {
									myParent: r.parentNode || i.body,
									position: x.getPropertyValue(r, "position"),
									fontSize: x.getPropertyValue(r, "fontSize")
								},
								n = t.position === V.lastPosition && t.myParent === V.lastParent,
								o = t.fontSize === V.lastFontSize;
							V.lastParent = t.myParent, V.lastPosition = t.position, V.lastFontSize =
								t.fontSize;
							var a = 100,
								l = {};
							if (o && n) l.emToPx = V.lastEmToPx, l.percentToPxWidth = V.lastPercentToPxWidth,
								l.percentToPxHeight = V.lastPercentToPxHeight;
							else {
								var c = s(r).isSVG ? i.createElementNS(
									"http://www.w3.org/2000/svg", "rect") : i.createElement("div");
								b.init(c), t.myParent.appendChild(c), p.each(["overflow",
										"overflowX", "overflowY"
									], function(t, e) {
										b.CSS.setPropertyValue(c, e, "hidden")
									}), b.CSS.setPropertyValue(c, "position", t.position), b.CSS.setPropertyValue(
										c, "fontSize", t.fontSize), b.CSS.setPropertyValue(c,
										"boxSizing", "content-box"), p.each(["minWidth", "maxWidth",
										"width", "minHeight", "maxHeight", "height"
									], function(t, e) {
										b.CSS.setPropertyValue(c, e, a + "%")
									}), b.CSS.setPropertyValue(c, "paddingLeft", a + "em"), l.percentToPxWidth =
									V.lastPercentToPxWidth = (parseFloat(x.getPropertyValue(c,
										"width", null, !0)) || 1) / a, l.percentToPxHeight = V.lastPercentToPxHeight =
									(parseFloat(x.getPropertyValue(c, "height", null, !0)) || 1) / a,
									l.emToPx = V.lastEmToPx = (parseFloat(x.getPropertyValue(c,
										"paddingLeft")) || 1) / a, t.myParent.removeChild(c)
							}
							return null === V.remToPx && (V.remToPx = parseFloat(x.getPropertyValue(
									i.body, "fontSize")) || 16), null === V.vwToPx && (V.vwToPx =
									parseFloat(e.innerWidth) / 100, V.vhToPx = parseFloat(e.innerHeight) /
									100), l.remToPx = V.remToPx, l.vwToPx = V.vwToPx, l.vhToPx = V.vhToPx,
								b.debug >= 1 && console.log("Unit ratios: " + JSON.stringify(l),
									r), l
						}
						if (a.begin && 0 === k) try {
							a.begin.call(h, h)
						} catch (y) {
							setTimeout(function() {
								throw y
							}, 1)
						}
						if ("scroll" === E) {
							var w, S, C, P = /^x$/i.test(a.axis) ? "Left" : "Top",
								A = parseFloat(a.offset) || 0;
							a.container ? g.isWrapped(a.container) || g.isNode(a.container) ? (
									a.container = a.container[0] || a.container, w = a.container[
										"scroll" + P], C = w + p(r).position()[P.toLowerCase()] + A) : a
								.container = null : (w = b.State.scrollAnchor[b.State[
									"scrollProperty" + P]], S = b.State.scrollAnchor[b.State[
									"scrollProperty" + ("Left" === P ? "Top" : "Left")]], C = p(r).offset()[
									P.toLowerCase()] + A), l = {
									scroll: {
										rootPropertyValue: !1,
										startValue: w,
										currentValue: w,
										endValue: C,
										unitType: "",
										easing: a.easing,
										scrollData: {
											container: a.container,
											direction: P,
											alternateValue: S
										}
									},
									element: r
								}, b.debug && console.log("tweensContainer (scroll): ", l.scroll,
									r)
						} else if ("reverse" === E) {
							if (!s(r).tweensContainer) return void p.dequeue(r, a.queue);
							"none" === s(r).opts.display && (s(r).opts.display = "auto"),
								"hidden" === s(r).opts.visibility && (s(r).opts.visibility =
									"visible"), s(r).opts.loop = !1, s(r).opts.begin = null, s(r).opts
								.complete = null, v.easing || delete a.easing, v.duration ||
								delete a.duration, a = p.extend({}, s(r).opts, a);
							var L = p.extend(!0, {}, s(r).tweensContainer);
							for (var $ in L)
								if ("element" !== $) {
									var _ = L[$].startValue;
									L[$].startValue = L[$].currentValue = L[$].endValue, L[$].endValue =
										_, g.isEmptyObject(v) || (L[$].easing = a.easing), b.debug &&
										console.log("reverse tweensContainer (" + $ + "): " + JSON.stringify(
											L[$]), r)
								}
							l = L
						} else if ("start" === E) {
							var L;
							s(r).tweensContainer && s(r).isAnimating === !0 && (L = s(r).tweensContainer),
								p.each(m, function(e, i) {
									if (RegExp("^" + x.Lists.colors.join("$|^") + "$").test(e)) {
										var o = t(i, !0),
											r = o[0],
											s = o[1],
											a = o[2];
										if (x.RegEx.isHex.test(r)) {
											for (var l = ["Red", "Green", "Blue"], c = x.Values.hexToRgb(r),
												u = a ? x.Values.hexToRgb(a) : n, d = 0; d < l.length; d++) {
												var p = [c[d]];
												s && p.push(s), u !== n && p.push(u[d]), m[e + l[d]] = p
											}
											delete m[e]
										}
									}
								});
							for (var z in m) {
								var H = t(m[z]),
									D = H[0],
									I = H[1],
									R = H[2];
								z = x.Names.camelCase(z);
								var X = x.Hooks.getRoot(z),
									j = !1;
								if (s(r).isSVG || "tween" === X || x.Names.prefixCheck(X)[1] !== !
									1 || x.Normalizations.registered[X] !== n) {
									(a.display !== n && null !== a.display && "none" !== a.display ||
										a.visibility !== n && "hidden" !== a.visibility) &&
									/opacity|filter/.test(z) && !R && 0 !== D && (R = 0), a._cacheValues &&
										L && L[z] ? (R === n && (R = L[z].endValue + L[z].unitType), j =
											s(r).rootPropertyValueCache[X]) : x.Hooks.registered[z] ? R ===
										n ? (j = x.getPropertyValue(r, X), R = x.getPropertyValue(r, z,
											j)) : j = x.Hooks.templates[X][1] : R === n && (R = x.getPropertyValue(
											r, z));
									var F, M, N, W = !1;
									if (F = d(z, R), R = F[0], N = F[1], F = d(z, D), D = F[0].replace(
											/^([+-\/*])=/, function(t, e) {
												return W = e, ""
											}), M = F[1], R = parseFloat(R) || 0, D = parseFloat(D) || 0,
										"%" === M && (/^(fontSize|lineHeight)$/.test(z) ? (D /= 100, M =
												"em") : /^scale/.test(z) ? (D /= 100, M = "") :
											/(Red|Green|Blue)$/i.test(z) && (D = D / 100 * 255, M = "")),
										/[\/*]/.test(W)) M = N;
									else if (N !== M && 0 !== R)
										if (0 === D) M = N;
										else {
											o = o || f();
											var q = /margin|padding|left|right|width|text|word|letter/i.test(
												z) || /X$/.test(z) || "x" === z ? "x" : "y";
											switch (N) {
												case "%":
													R *= "x" === q ? o.percentToPxWidth : o.percentToPxHeight;
													break;
												case "px":
													break;
												default:
													R *= o[N + "ToPx"]
											}
											switch (M) {
												case "%":
													R *= 1 / ("x" === q ? o.percentToPxWidth : o.percentToPxHeight);
													break;
												case "px":
													break;
												default:
													R *= 1 / o[M + "ToPx"]
											}
										}
									switch (W) {
										case "+":
											D = R + D;
											break;
										case "-":
											D = R - D;
											break;
										case "*":
											D = R * D;
											break;
										case "/":
											D = R / D
									}
									l[z] = {
										rootPropertyValue: j,
										startValue: R,
										currentValue: R,
										endValue: D,
										unitType: M,
										easing: I
									}, b.debug && console.log("tweensContainer (" + z + "): " + JSON
										.stringify(l[z]), r)
								} else b.debug && console.log("Skipping [" + X +
									"] due to a lack of browser support.")
							}
							l.element = r
						}
						l.element && (x.Values.addClass(r, "velocity-animating"), Y.push(l),
							"" === a.queue && (s(r).tweensContainer = l, s(r).opts = a), s(r).isAnimating = !
							0, k === T - 1 ? (b.State.calls.push([Y, h, a, null, O.resolver]),
								b.State.isTicking === !1 && (b.State.isTicking = !0, u())) : k++)
					}
					var o, r = this,
						a = p.extend({}, b.defaults, v),
						l = {};
					switch (s(r) === n && b.init(r), parseFloat(a.delay) && a.queue !== !1 &&
						p.queue(r, a.queue, function(t) {
							b.velocityQueueEntryFlag = !0, s(r).delayTimer = {
								setTimeout: setTimeout(t, parseFloat(a.delay)),
								next: t
							}
						}), a.duration.toString().toLowerCase()) {
						case "fast":
							a.duration = 200;
							break;
						case "normal":
							a.duration = y;
							break;
						case "slow":
							a.duration = 600;
							break;
						default:
							a.duration = parseFloat(a.duration) || 1
					}
					b.mock !== !1 && (b.mock === !0 ? a.duration = a.delay = 1 : (a.duration *=
							parseFloat(b.mock) || 1, a.delay *= parseFloat(b.mock) || 1)), a.easing =
						c(a.easing, a.duration), a.begin && !g.isFunction(a.begin) && (a.begin =
							null), a.progress && !g.isFunction(a.progress) && (a.progress = null),
						a.complete && !g.isFunction(a.complete) && (a.complete = null), a.display !==
						n && null !== a.display && (a.display = a.display.toString().toLowerCase(),
							"auto" === a.display && (a.display = b.CSS.Values.getDisplayType(r))
						), a.visibility !== n && null !== a.visibility && (a.visibility = a.visibility
							.toString().toLowerCase()), a.mobileHA = a.mobileHA && b.State.isMobile &&
						!b.State.isGingerbread, a.queue === !1 ? a.delay ? setTimeout(t, a.delay) :
						t() : p.queue(r, a.queue, function(e, i) {
							return i === !0 ? (O.promise && O.resolver(h), !0) : (b.velocityQueueEntryFlag = !
								0, void t(e))
						}), "" !== a.queue && "fx" !== a.queue || "inprogress" === p.queue(r)[
							0] || p.dequeue(r)
				}
				var a, l, f, h, m, v, w = arguments[0] && (arguments[0].p || p.isPlainObject(
					arguments[0].properties) && !arguments[0].properties.names || g.isString(
					arguments[0].properties));
				if (g.isWrapped(this) ? (a = !1, f = 0, h = this, l = this) : (a = !0, f =
						1, h = w ? arguments[0].elements || arguments[0].e : arguments[0]), h =
					r(h)) {
					w ? (m = arguments[0].properties || arguments[0].p, v = arguments[0].options ||
						arguments[0].o) : (m = arguments[f], v = arguments[f + 1]);
					var T = h.length,
						k = 0;
					if (!/^(stop|finish)$/i.test(m) && !p.isPlainObject(v)) {
						var C = f + 1;
						v = {};
						for (var P = C; P < arguments.length; P++) g.isArray(arguments[P]) ||
							!/^(fast|normal|slow)$/i.test(arguments[P]) && !/^\d/.test(arguments[
								P]) ? g.isString(arguments[P]) || g.isArray(arguments[P]) ? v.easing =
							arguments[P] : g.isFunction(arguments[P]) && (v.complete = arguments[
								P]) : v.duration = arguments[P]
					}
					var O = {
						promise: null,
						resolver: null,
						rejecter: null
					};
					a && b.Promise && (O.promise = new b.Promise(function(t, e) {
						O.resolver = t, O.rejecter = e
					}));
					var E;
					switch (m) {
						case "scroll":
							E = "scroll";
							break;
						case "reverse":
							E = "reverse";
							break;
						case "finish":
						case "stop":
							p.each(h, function(t, e) {
								s(e) && s(e).delayTimer && (clearTimeout(s(e).delayTimer.setTimeout),
									s(e).delayTimer.next && s(e).delayTimer.next(), delete s(e).delayTimer
								)
							});
							var A = [];
							return p.each(b.State.calls, function(t, e) {
								e && p.each(e[1], function(i, o) {
									var r = v === n ? "" : v;
									return r === !0 || e[2].queue === r || v === n && e[2].queue ===
										!1 ? void p.each(h, function(i, n) {
											n === o && ((v === !0 || g.isString(v)) && (p.each(p.queue(n,
													g.isString(v) ? v : ""), function(t, e) {
													g.isFunction(e) && e(null, !0)
												}), p.queue(n, g.isString(v) ? v : "", [])), "stop" === m ?
												(s(n) && s(n).tweensContainer && r !== !1 && p.each(s(n).tweensContainer,
													function(t, e) {
														e.endValue = e.currentValue
													}), A.push(t)) : "finish" === m && (e[2].duration = 1))
										}) : !0
								})
							}), "stop" === m && (p.each(A, function(t, e) {
								d(e, !0)
							}), O.promise && O.resolver(h)), t();
						default:
							if (!p.isPlainObject(m) || g.isEmptyObject(m)) {
								if (g.isString(m) && b.Redirects[m]) {
									var L = p.extend({}, v),
										$ = L.duration,
										_ = L.delay || 0;
									return L.backwards === !0 && (h = p.extend(!0, [], h).reverse()), p
										.each(h, function(t, e) {
											parseFloat(L.stagger) ? L.delay = _ + parseFloat(L.stagger) * t :
												g.isFunction(L.stagger) && (L.delay = _ + L.stagger.call(e, t,
													T)), L.drag && (L.duration = parseFloat($) || (
														/^(callout|transition)/.test(m) ? 1e3 : y), L.duration = Math
													.max(L.duration * (L.backwards ? 1 - t / T : (t + 1) / T), .75 *
														L.duration, 200)), b.Redirects[m].call(e, e, L || {}, t, T, h,
													O.promise ? O : n)
										}), t()
								}
								var z = "Velocity: First argument (" + m +
									") was not a property map, a known action, or a registered redirect. Aborting.";
								return O.promise ? O.rejecter(new Error(z)) : console.log(z), t()
							}
							E = "start"
					}
					var V = {
							lastParent: null,
							lastPosition: null,
							lastFontSize: null,
							lastPercentToPxWidth: null,
							lastPercentToPxHeight: null,
							lastEmToPx: null,
							remToPx: null,
							vwToPx: null,
							vhToPx: null
						},
						Y = [];
					p.each(h, function(t, e) {
						g.isNode(e) && o.call(e)
					});
					var H, L = p.extend({}, b.defaults, v);
					if (L.loop = parseInt(L.loop), H = 2 * L.loop - 1, L.loop)
						for (var D = 0; H > D; D++) {
							var I = {
								delay: L.delay,
								progress: L.progress
							};
							D === H - 1 && (I.display = L.display, I.visibility = L.visibility, I
								.complete = L.complete), S(h, "reverse", I)
						}
					return t()
				}
			};
			b = p.extend(S, b), b.animate = S;
			var T = e.requestAnimationFrame || h;
			return b.State.isMobile || i.hidden === n || i.addEventListener(
				"visibilitychange", function() {
					i.hidden ? (T = function(t) {
						return setTimeout(function() {
							t(!0)
						}, 16)
					}, u()) : T = e.requestAnimationFrame || h
				}), t.Velocity = b, t !== e && (t.fn.velocity = S, t.fn.velocity.defaults =
				b.defaults), p.each(["Down", "Up"], function(t, e) {
				b.Redirects["slide" + e] = function(t, i, o, r, s, a) {
					var l = p.extend({}, i),
						c = l.begin,
						u = l.complete,
						d = {
							height: "",
							marginTop: "",
							marginBottom: "",
							paddingTop: "",
							paddingBottom: ""
						},
						f = {};
					l.display === n && (l.display = "Down" === e ? "inline" === b.CSS.Values
							.getDisplayType(t) ? "inline-block" : "block" : "none"), l.begin =
						function() {
							c && c.call(s, s);
							for (var i in d) {
								f[i] = t.style[i];
								var n = b.CSS.getPropertyValue(t, i);
								d[i] = "Down" === e ? [n, 0] : [0, n]
							}
							f.overflow = t.style.overflow, t.style.overflow = "hidden"
						}, l.complete = function() {
							for (var e in f) t.style[e] = f[e];
							u && u.call(s, s), a && a.resolver(s)
						}, b(t, d, l)
				}
			}), p.each(["In", "Out"], function(t, e) {
				b.Redirects["fade" + e] = function(t, i, o, r, s, a) {
					var l = p.extend({}, i),
						c = {
							opacity: "In" === e ? 1 : 0
						},
						u = l.complete;
					l.complete = o !== r - 1 ? l.begin = null : function() {
						u && u.call(s, s), a && a.resolver(s)
					}, l.display === n && (l.display = "In" === e ? "auto" : "none"), b(
						this, c, l)
				}
			}), b
		}(window.jQuery || window.Zepto || window, window, document)
	}),
	function(t) {
		"function" == typeof require && "object" == typeof exports ? module.exports =
			t() : "function" == typeof define && define.amd ? define(["velocity"], t) :
			t()
	}(function() {
		return function(t, e, i, n) {
			function o(t, e) {
				var i = [];
				return t && e ? (s.each([t, e], function(t, e) {
					var n = [];
					s.each(e, function(t, e) {
						for (; e.toString().length < 5;) e = "0" + e;
						n.push(e)
					}), i.push(n.join(""))
				}), parseFloat(i[0]) > parseFloat(i[1])) : !1
			}
			if (!t.Velocity || !t.Velocity.Utilities) return void(e.console && console
				.log("Velocity UI Pack: Velocity must be loaded first. Aborting."));
			var r = t.Velocity,
				s = r.Utilities,
				a = r.version,
				l = {
					major: 1,
					minor: 1,
					patch: 0
				};
			if (o(l, a)) {
				var c =
					"Velocity UI Pack: You need to update Velocity (jquery.velocity.js) to a newer version. Visit http://github.com/julianshapiro/velocity.";
				throw alert(c), new Error(c)
			}
			r.RegisterEffect = r.RegisterUI = function(t, e) {
				function i(t, e, i, n) {
					var o, a = 0;
					s.each(t.nodeType ? [t] : t, function(t, e) {
						n && (i += t * n), o = e.parentNode, s.each(["height", "paddingTop",
							"paddingBottom", "marginTop", "marginBottom"
						], function(t, i) {
							a += parseFloat(r.CSS.getPropertyValue(e, i))
						})
					}), r.animate(o, {
						height: ("In" === e ? "+" : "-") + "=" + a
					}, {
						queue: !1,
						easing: "ease-in-out",
						duration: i * ("In" === e ? .6 : 1)
					})
				}
				return r.Redirects[t] = function(o, a, l, c, u, d) {
					function p() {
						a.display !== n && "none" !== a.display || !/Out$/.test(t) || s.each(
							u.nodeType ? [u] : u, function(t, e) {
								r.CSS.setPropertyValue(e, "display", "none")
							}), a.complete && a.complete.call(u, u), d && d.resolver(u || o)
					}
					var f = l === c - 1;
					e.defaultDuration = "function" == typeof e.defaultDuration ? e.defaultDuration
						.call(u, u) : parseFloat(e.defaultDuration);
					for (var h = 0; h < e.calls.length; h++) {
						var g = e.calls[h],
							m = g[0],
							y = a.duration || e.defaultDuration || 1e3,
							v = g[1],
							b = g[2] || {},
							w = {};
						if (w.duration = y * (v || 1), w.queue = a.queue || "", w.easing = b.easing ||
							"ease", w.delay = parseFloat(b.delay) || 0, w._cacheValues = b._cacheValues ||
							!0, 0 === h) {
							if (w.delay += parseFloat(a.delay) || 0, 0 === l && (w.begin =
								function() {
									a.begin && a.begin.call(u, u);
									var e = t.match(/(In|Out)$/);
									e && "In" === e[0] && m.opacity !== n && s.each(u.nodeType ? [u] :
										u, function(t, e) {
											r.CSS.setPropertyValue(e, "opacity", 0)
										}), a.animateParentHeight && e && i(u, e[0], y + w.delay, a.stagger)
								}), null !== a.display)
								if (a.display !== n && "none" !== a.display) w.display = a.display;
								else if (/In$/.test(t)) {
								var x = r.CSS.Values.getDisplayType(o);
								w.display = "inline" === x ? "inline-block" : x
							}
							a.visibility && "hidden" !== a.visibility && (w.visibility = a.visibility)
						}
						h === e.calls.length - 1 && (w.complete = function() {
							if (e.reset) {
								for (var t in e.reset) {
									var i = e.reset[t];
									r.CSS.Hooks.registered[t] !== n || "string" != typeof i &&
										"number" != typeof i || (e.reset[t] = [e.reset[t], e.reset[t]])
								}
								var s = {
									duration: 0,
									queue: !1
								};
								f && (s.complete = p), r.animate(o, e.reset, s)
							} else f && p()
						}, "hidden" === a.visibility && (w.visibility = a.visibility)), r.animate(
							o, m, w)
					}
				}, r
			}, r.RegisterEffect.packagedEffects = {
				"callout.bounce": {
					defaultDuration: 550,
					calls: [
						[{
							translateY: -30
						}, .25],
						[{
							translateY: 0
						}, .125],
						[{
							translateY: -15
						}, .125],
						[{
							translateY: 0
						}, .25]
					]
				},
				"callout.shake": {
					defaultDuration: 800,
					calls: [
						[{
							translateX: -11
						}, .125],
						[{
							translateX: 11
						}, .125],
						[{
							translateX: -11
						}, .125],
						[{
							translateX: 11
						}, .125],
						[{
							translateX: -11
						}, .125],
						[{
							translateX: 11
						}, .125],
						[{
							translateX: -11
						}, .125],
						[{
							translateX: 0
						}, .125]
					]
				},
				"callout.flash": {
					defaultDuration: 1100,
					calls: [
						[{
							opacity: [0, "easeInOutQuad", 1]
						}, .25],
						[{
							opacity: [1, "easeInOutQuad"]
						}, .25],
						[{
							opacity: [0, "easeInOutQuad"]
						}, .25],
						[{
							opacity: [1, "easeInOutQuad"]
						}, .25]
					]
				},
				"callout.pulse": {
					defaultDuration: 825,
					calls: [
						[{
							scaleX: 1.1,
							scaleY: 1.1
						}, .5, {
							easing: "easeInExpo"
						}],
						[{
							scaleX: 1,
							scaleY: 1
						}, .5]
					]
				},
				"callout.swing": {
					defaultDuration: 950,
					calls: [
						[{
							rotateZ: 15
						}, .2],
						[{
							rotateZ: -10
						}, .2],
						[{
							rotateZ: 5
						}, .2],
						[{
							rotateZ: -5
						}, .2],
						[{
							rotateZ: 0
						}, .2]
					]
				},
				"callout.tada": {
					defaultDuration: 1e3,
					calls: [
						[{
							scaleX: .9,
							scaleY: .9,
							rotateZ: -3
						}, .1],
						[{
							scaleX: 1.1,
							scaleY: 1.1,
							rotateZ: 3
						}, .1],
						[{
							scaleX: 1.1,
							scaleY: 1.1,
							rotateZ: -3
						}, .1],
						["reverse", .125],
						["reverse", .125],
						["reverse", .125],
						["reverse", .125],
						["reverse", .125],
						[{
							scaleX: 1,
							scaleY: 1,
							rotateZ: 0
						}, .2]
					]
				},
				"transition.fadeIn": {
					defaultDuration: 500,
					calls: [
						[{
							opacity: [1, 0]
						}]
					]
				},
				"transition.fadeOut": {
					defaultDuration: 500,
					calls: [
						[{
							opacity: [0, 1]
						}]
					]
				},
				"transition.flipXIn": {
					defaultDuration: 700,
					calls: [
						[{
							opacity: [1, 0],
							transformPerspective: [800, 800],
							rotateY: [0, -55]
						}]
					],
					reset: {
						transformPerspective: 0
					}
				},
				"transition.flipXOut": {
					defaultDuration: 700,
					calls: [
						[{
							opacity: [0, 1],
							transformPerspective: [800, 800],
							rotateY: 55
						}]
					],
					reset: {
						transformPerspective: 0,
						rotateY: 0
					}
				},
				"transition.flipYIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							transformPerspective: [800, 800],
							rotateX: [0, -45]
						}]
					],
					reset: {
						transformPerspective: 0
					}
				},
				"transition.flipYOut": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [0, 1],
							transformPerspective: [800, 800],
							rotateX: 25
						}]
					],
					reset: {
						transformPerspective: 0,
						rotateX: 0
					}
				},
				"transition.flipBounceXIn": {
					defaultDuration: 900,
					calls: [
						[{
							opacity: [.725, 0],
							transformPerspective: [400, 400],
							rotateY: [-10, 90]
						}, .5],
						[{
							opacity: .8,
							rotateY: 10
						}, .25],
						[{
							opacity: 1,
							rotateY: 0
						}, .25]
					],
					reset: {
						transformPerspective: 0
					}
				},
				"transition.flipBounceXOut": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [.9, 1],
							transformPerspective: [400, 400],
							rotateY: -10
						}, .5],
						[{
							opacity: 0,
							rotateY: 90
						}, .5]
					],
					reset: {
						transformPerspective: 0,
						rotateY: 0
					}
				},
				"transition.flipBounceYIn": {
					defaultDuration: 850,
					calls: [
						[{
							opacity: [.725, 0],
							transformPerspective: [400, 400],
							rotateX: [-10, 90]
						}, .5],
						[{
							opacity: .8,
							rotateX: 10
						}, .25],
						[{
							opacity: 1,
							rotateX: 0
						}, .25]
					],
					reset: {
						transformPerspective: 0
					}
				},
				"transition.flipBounceYOut": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [.9, 1],
							transformPerspective: [400, 400],
							rotateX: -15
						}, .5],
						[{
							opacity: 0,
							rotateX: 90
						}, .5]
					],
					reset: {
						transformPerspective: 0,
						rotateX: 0
					}
				},
				"transition.swoopIn": {
					defaultDuration: 850,
					calls: [
						[{
							opacity: [1, 0],
							transformOriginX: ["100%", "50%"],
							transformOriginY: ["100%", "100%"],
							scaleX: [1, 0],
							scaleY: [1, 0],
							translateX: [0, -700],
							translateZ: 0
						}]
					],
					reset: {
						transformOriginX: "50%",
						transformOriginY: "50%"
					}
				},
				"transition.swoopOut": {
					defaultDuration: 850,
					calls: [
						[{
							opacity: [0, 1],
							transformOriginX: ["50%", "100%"],
							transformOriginY: ["100%", "100%"],
							scaleX: 0,
							scaleY: 0,
							translateX: -700,
							translateZ: 0
						}]
					],
					reset: {
						transformOriginX: "50%",
						transformOriginY: "50%",
						scaleX: 1,
						scaleY: 1,
						translateX: 0
					}
				},
				"transition.whirlIn": {
					defaultDuration: 850,
					calls: [
						[{
								opacity: [1, 0],
								transformOriginX: ["50%", "50%"],
								transformOriginY: ["50%", "50%"],
								scaleX: [1, 0],
								scaleY: [1, 0],
								rotateY: [0, 160]
							},
							1, {
								easing: "easeInOutSine"
							}
						]
					]
				},
				"transition.whirlOut": {
					defaultDuration: 750,
					calls: [
						[{
								opacity: [0, "easeInOutQuint", 1],
								transformOriginX: ["50%", "50%"],
								transformOriginY: ["50%", "50%"],
								scaleX: 0,
								scaleY: 0,
								rotateY: 160
							},
							1, {
								easing: "swing"
							}
						]
					],
					reset: {
						scaleX: 1,
						scaleY: 1,
						rotateY: 0
					}
				},
				"transition.shrinkIn": {
					defaultDuration: 750,
					calls: [
						[{
							opacity: [1, 0],
							transformOriginX: ["50%", "50%"],
							transformOriginY: ["50%", "50%"],
							scaleX: [1, 1.5],
							scaleY: [1, 1.5],
							translateZ: 0
						}]
					]
				},
				"transition.shrinkOut": {
					defaultDuration: 600,
					calls: [
						[{
							opacity: [0, 1],
							transformOriginX: ["50%", "50%"],
							transformOriginY: ["50%", "50%"],
							scaleX: 1.3,
							scaleY: 1.3,
							translateZ: 0
						}]
					],
					reset: {
						scaleX: 1,
						scaleY: 1
					}
				},
				"transition.expandIn": {
					defaultDuration: 700,
					calls: [
						[{
							opacity: [1, 0],
							transformOriginX: ["50%", "50%"],
							transformOriginY: ["50%", "50%"],
							scaleX: [1, .625],
							scaleY: [1, .625],
							translateZ: 0
						}]
					]
				},
				"transition.expandOut": {
					defaultDuration: 700,
					calls: [
						[{
							opacity: [0, 1],
							transformOriginX: ["50%", "50%"],
							transformOriginY: ["50%", "50%"],
							scaleX: .5,
							scaleY: .5,
							translateZ: 0
						}]
					],
					reset: {
						scaleX: 1,
						scaleY: 1
					}
				},
				"transition.bounceIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							scaleX: [1.05, .3],
							scaleY: [1.05, .3]
						}, .4],
						[{
							scaleX: .9,
							scaleY: .9,
							translateZ: 0
						}, .2],
						[{
							scaleX: 1,
							scaleY: 1
						}, .5]
					]
				},
				"transition.bounceOut": {
					defaultDuration: 800,
					calls: [
						[{
							scaleX: .95,
							scaleY: .95
						}, .35],
						[{
							scaleX: 1.1,
							scaleY: 1.1,
							translateZ: 0
						}, .35],
						[{
							opacity: [0, 1],
							scaleX: .3,
							scaleY: .3
						}, .3]
					],
					reset: {
						scaleX: 1,
						scaleY: 1
					}
				},
				"transition.bounceUpIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							translateY: [-30, 1e3]
						}, .6, {
							easing: "easeOutCirc"
						}],
						[{
							translateY: 10
						}, .2],
						[{
							translateY: 0
						}, .2]
					]
				},
				"transition.bounceUpOut": {
					defaultDuration: 1e3,
					calls: [
						[{
							translateY: 20
						}, .2],
						[{
							opacity: [0, "easeInCirc", 1],
							translateY: -1e3
						}, .8]
					],
					reset: {
						translateY: 0
					}
				},
				"transition.bounceDownIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							translateY: [30, -1e3]
						}, .6, {
							easing: "easeOutCirc"
						}],
						[{
							translateY: -10
						}, .2],
						[{
							translateY: 0
						}, .2]
					]
				},
				"transition.bounceDownOut": {
					defaultDuration: 1e3,
					calls: [
						[{
							translateY: -20
						}, .2],
						[{
							opacity: [0, "easeInCirc", 1],
							translateY: 1e3
						}, .8]
					],
					reset: {
						translateY: 0
					}
				},
				"transition.bounceLeftIn": {
					defaultDuration: 750,
					calls: [
						[{
							opacity: [1, 0],
							translateX: [30, -1250]
						}, .6, {
							easing: "easeOutCirc"
						}],
						[{
							translateX: -10
						}, .2],
						[{
							translateX: 0
						}, .2]
					]
				},
				"transition.bounceLeftOut": {
					defaultDuration: 750,
					calls: [
						[{
							translateX: 30
						}, .2],
						[{
							opacity: [0, "easeInCirc", 1],
							translateX: -1250
						}, .8]
					],
					reset: {
						translateX: 0
					}
				},
				"transition.bounceRightIn": {
					defaultDuration: 750,
					calls: [
						[{
							opacity: [1, 0],
							translateX: [-30, 1250]
						}, .6, {
							easing: "easeOutCirc"
						}],
						[{
							translateX: 10
						}, .2],
						[{
							translateX: 0
						}, .2]
					]
				},
				"transition.bounceRightOut": {
					defaultDuration: 750,
					calls: [
						[{
							translateX: -30
						}, .2],
						[{
							opacity: [0, "easeInCirc", 1],
							translateX: 1250
						}, .8]
					],
					reset: {
						translateX: 0
					}
				},
				"transition.slideUpIn": {
					defaultDuration: 900,
					calls: [
						[{
							opacity: [1, 0],
							translateY: [0, 20],
							translateZ: 0
						}]
					]
				},
				"transition.slideUpOut": {
					defaultDuration: 900,
					calls: [
						[{
							opacity: [0, 1],
							translateY: -20,
							translateZ: 0
						}]
					],
					reset: {
						translateY: 0
					}
				},
				"transition.slideDownIn": {
					defaultDuration: 900,
					calls: [
						[{
							opacity: [1, 0],
							translateY: [0, -20],
							translateZ: 0
						}]
					]
				},
				"transition.slideDownOut": {
					defaultDuration: 900,
					calls: [
						[{
							opacity: [0, 1],
							translateY: 20,
							translateZ: 0
						}]
					],
					reset: {
						translateY: 0
					}
				},
				"transition.slideLeftIn": {
					defaultDuration: 1e3,
					calls: [
						[{
							opacity: [1, 0],
							translateX: [0, -20],
							translateZ: 0
						}]
					]
				},
				"transition.slideLeftOut": {
					defaultDuration: 1050,
					calls: [
						[{
							opacity: [0, 1],
							translateX: -20,
							translateZ: 0
						}]
					],
					reset: {
						translateX: 0
					}
				},
				"transition.slideRightIn": {
					defaultDuration: 1e3,
					calls: [
						[{
							opacity: [1, 0],
							translateX: [0, 20],
							translateZ: 0
						}]
					]
				},
				"transition.slideRightOut": {
					defaultDuration: 1050,
					calls: [
						[{
							opacity: [0, 1],
							translateX: 20,
							translateZ: 0
						}]
					],
					reset: {
						translateX: 0
					}
				},
				"transition.slideUpBigIn": {
					defaultDuration: 850,
					calls: [
						[{
							opacity: [1, 0],
							translateY: [0, 75],
							translateZ: 0
						}]
					]
				},
				"transition.slideUpBigOut": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [0, 1],
							translateY: -75,
							translateZ: 0
						}]
					],
					reset: {
						translateY: 0
					}
				},
				"transition.slideDownBigIn": {
					defaultDuration: 850,
					calls: [
						[{
							opacity: [1, 0],
							translateY: [0, -75],
							translateZ: 0
						}]
					]
				},
				"transition.slideDownBigOut": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [0, 1],
							translateY: 75,
							translateZ: 0
						}]
					],
					reset: {
						translateY: 0
					}
				},
				"transition.slideLeftBigIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							translateX: [0, -75],
							translateZ: 0
						}]
					]
				},
				"transition.slideLeftBigOut": {
					defaultDuration: 750,
					calls: [
						[{
							opacity: [0, 1],
							translateX: -75,
							translateZ: 0
						}]
					],
					reset: {
						translateX: 0
					}
				},
				"transition.slideRightBigIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							translateX: [0, 75],
							translateZ: 0
						}]
					]
				},
				"transition.slideRightBigOut": {
					defaultDuration: 750,
					calls: [
						[{
							opacity: [0, 1],
							translateX: 75,
							translateZ: 0
						}]
					],
					reset: {
						translateX: 0
					}
				},
				"transition.perspectiveUpIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							transformPerspective: [800, 800],
							transformOriginX: [0, 0],
							transformOriginY: ["100%", "100%"],
							rotateX: [0, -180]
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%"
					}
				},
				"transition.perspectiveUpOut": {
					defaultDuration: 850,
					calls: [
						[{
							opacity: [0, 1],
							transformPerspective: [800, 800],
							transformOriginX: [0, 0],
							transformOriginY: ["100%", "100%"],
							rotateX: -180
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%",
						rotateX: 0
					}
				},
				"transition.perspectiveDownIn": {
					defaultDuration: 800,
					calls: [
						[{
							opacity: [1, 0],
							transformPerspective: [800, 800],
							transformOriginX: [0, 0],
							transformOriginY: [0, 0],
							rotateX: [0, 180]
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%"
					}
				},
				"transition.perspectiveDownOut": {
					defaultDuration: 850,
					calls: [
						[{
							opacity: [0, 1],
							transformPerspective: [800, 800],
							transformOriginX: [0, 0],
							transformOriginY: [0, 0],
							rotateX: 180
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%",
						rotateX: 0
					}
				},
				"transition.perspectiveLeftIn": {
					defaultDuration: 950,
					calls: [
						[{
							opacity: [1, 0],
							transformPerspective: [2e3, 2e3],
							transformOriginX: [0, 0],
							transformOriginY: [0, 0],
							rotateY: [0, -180]
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%"
					}
				},
				"transition.perspectiveLeftOut": {
					defaultDuration: 950,
					calls: [
						[{
							opacity: [0, 1],
							transformPerspective: [2e3, 2e3],
							transformOriginX: [0, 0],
							transformOriginY: [0, 0],
							rotateY: -180
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%",
						rotateY: 0
					}
				},
				"transition.perspectiveRightIn": {
					defaultDuration: 950,
					calls: [
						[{
							opacity: [1, 0],
							transformPerspective: [2e3, 2e3],
							transformOriginX: ["100%", "100%"],
							transformOriginY: [0, 0],
							rotateY: [0, 180]
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%"
					}
				},
				"transition.perspectiveRightOut": {
					defaultDuration: 950,
					calls: [
						[{
							opacity: [0, 1],
							transformPerspective: [2e3, 2e3],
							transformOriginX: ["100%", "100%"],
							transformOriginY: [0, 0],
							rotateY: 180
						}]
					],
					reset: {
						transformPerspective: 0,
						transformOriginX: "50%",
						transformOriginY: "50%",
						rotateY: 0
					}
				}
			};
			for (var u in r.RegisterEffect.packagedEffects) r.RegisterEffect(u, r.RegisterEffect
				.packagedEffects[u]);
			r.RunSequence = function(t) {
				var e = s.extend(!0, [], t);
				e.length > 1 && (s.each(e.reverse(), function(t, i) {
					var n = e[t + 1];
					if (n) {
						var o = i.o || i.options,
							a = n.o || n.options,
							l = o && o.sequenceQueue === !1 ? "begin" : "complete",
							c = a && a[l],
							u = {};
						u[l] = function() {
							var t = n.e || n.elements,
								e = t.nodeType ? [t] : t;
							c && c.call(e, e), r(i)
						}, n.o ? n.o = s.extend({}, a, u) : n.options = s.extend({}, a, u)
					}
				}), e.reverse()), r(e[0])
			}
		}(window.jQuery || window.Zepto || window, window, document)
	});