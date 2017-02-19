function getParameterByName(e) {
	e = e.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
	var i = new RegExp("[\\?&]" + e + "=([^&#]*)"),
		t = i.exec(location.search);
	return null === t ? "" : decodeURIComponent(t[1].replace(/\+/g, " "))
}

function squareLink() {
	$(".square-wrap").attr("href", $(".client-name.active").attr("data-href"))
}

function homeClientName(e, i) {
	$clientActiveOffset = -5, $clientOffset = 5, "up" == e && (
		$clientActiveOffset = 5, $clientOffset = -5), ("" == i || "undefined" ==
		typeof i) && (i = 1), 0 != $(".ninety-wrap").css("opacity") && $(
		".ninety-wrap").velocity({
		opacity: 0,
		position: "relative",
		translateY: -5,
		translateZ: 0
	}, {
		duration: 150
	}), $clientName.filter(".active").velocity({
		opacity: 0,
		translateY: $clientActiveOffset,
		translateZ: 0
	}, {
		duration: 150
	}).removeClass("active"), $clientName.eq(i - 1).velocity({
		translateY: $clientOffset,
		translateZ: 0
	}, {
		duration: 0
	}).velocity({
		opacity: 1,
		translateY: 0,
		translateZ: 0
	}, {
		duration: 300
	}).addClass("active"), $(".client-name-wrap").velocity({
		height: $clientName.eq(i - 1).height()
	}, {
		duration: 300
	}), squareLink()
}! function(e, i) {
	var t = function(e, i, t) {
		var n;
		return function() {
			function a() {
				t || e.apply(o, r), n = null
			}
			var o = this,
				r = arguments;
			n ? clearTimeout(n) : t && e.apply(o, r), n = setTimeout(a, i || 100)
		}
	};
	jQuery.fn[i] = function(e) {
		return e ? this.bind("resize", t(e)) : this.trigger(i)
	}
}(jQuery, "smartresize");
var animEndEventNames = {
		WebkitAnimation: "webkitAnimationEnd",
		MozTAnimation: "animationend",
		animation: "animationend"
	},
	animEndEventName = animEndEventNames[Modernizr.prefixed("animation")],
	transEndEventNames = {
		WebkitTransition: "webkitTransitionEnd",
		MozTransition: "transitionend",
		transition: "transitionend"
	},
	transEndEventName = transEndEventNames[Modernizr.prefixed("transition")],
	$clientName = $(".client-name");
svg4everybody(), $(document).ready(function() {
		function e(e) {
			$(".square-edges div").css({
				"z-index": 11
			}).filter(e.attr("data-square")).css({
				"z-index": 3
			}), window.clearTimeout(g)
		}

		function i() {
			$(".carousel").on("init", function(e, i) {
					$(".slick-active").find(".video-slide").attr("data-bg-mp4") && t($(
						".slick-active"))
				}), $carousel = $(".carousel").slick({
					cssEase: "cubic-bezier(0.165, 0.84, 0.44, 1)",
					draggable: !1,
					speed: 600,
					infinite: !1,
					prevArrow: $(".arw-wrap.left"),
					nextArrow: $(".arw-wrap.right"),
					swipe: !1,
					initialSlide: $initialSlide,
					lazyLoad: "progressive",
					touchMove: !1,
					draggable: !1,
					waitForAnimate: !0
				}).on("beforeChange", function(e, i, t, n) {
					$(".btn-play-mobile").addClass("hide"), $oldVideo = $(
							'.slick-slide[data-slick-index="' + t + '"] .bg-case-study-video'),
						$oldVideo.length && (l.dispose(), $oldVideo.remove()), $nextSlide = $(
							'.slick-slide[data-slick-index="' + n + '"]'), $nextSlide.find(
							".video-slide").length ? targetImage = $nextSlide.find(
							".video-slide .video-poster") : targetImage = $nextSlide.find(
							".inside"), "true" == $nextSlide.attr("data-lazy") && $("<img/>").attr(
							"src", targetImage.attr("data-lrg")).load(function() {
							$(this).remove(), $nextSlide.attr("data-lazy", "false"), targetImage
								.css("background-image", "url(" + targetImage.attr("data-lrg") +
									")").addClass("loaded")
						})
				}).on("afterChange", function(e, i) {
					$nextSlide = $('.slick-slide[data-slick-index="' + i.currentSlide +
							'"]'), $nextSlideVideo = $nextSlide.find(".video-slide"), p.hasClass(
							"active") || p.addClass("active"), $nextSlide.find(".video-slide").attr(
							"data-bg-mp4") && t($nextSlide), $currentSlide = $carousel.slick(
							"slickCurrentSlide") + 1, ga("send", "event", "Carousel",
							"Slide Progress", $(".work-copy-headings h1").text() + " - Slide " +
							$currentSlide), d.removeClass("white-ui gray-ui").addClass($(
							".slick-slide").eq($currentSlide - 1).attr("data-ui-color") + "-ui"),
						Modernizr.history && history.replaceState("slide", "slide " +
							$currentSlide, "?slide=" + $currentSlide), $progCurrent.text(
							$currentSlide), $(".slick-slide").eq($currentSlide - 1).find(
							".video-slide").length && $(".btn-play-main").removeClass("hide")
				}), $initialSlide > 0 && $(".arw-wrap.left").removeClass("slick-disabled"),
				parseInt($initialSlide + 1) == parseInt($(".work-grid").attr(
					"data-total-slides")) && $(".arw-wrap.right").addClass("slick-disabled"),
				d.removeClass("white-ui gray-ui").addClass($(".slick-active").attr(
					"data-ui-color") + "-ui")
		}

		function t(e) {
			e.find(".video-slide").append(
				'<video id="bg-video" loop class="bg-case-study-video" muted preload="auto"><source src="' +
				e.find(".video-slide").attr("data-bg-mp4") +
				'" type="video/mp4"></source></video>'), $(".btn-play-main").append(
				'<span class="vjs-loading-spinner"></span>'), videojs("bg-video", {
				controlBar: !1,
				textTrackDisplay: !1,
				bigPlayButton: !1,
				errorDisplay: !1,
				posterImage: !1,
				loadingSpinner: !1
			}).ready(function() {
				l = this, l.one("canplay", function() {
					l.play(), $(".bg-case-study-video").velocity({
						opacity: 1
					}, {
						duration: 350
					})
				}), l.one("play", function() {
					$(".btn-play-main .vjs-loading-spinner").remove()
				})
			})
		}

		function n(e) {
			$(window).width() < 1025 && Modernizr.touch ? ($(".large-video source").attr(
					"src", e), e = $(".large-video")[0], e.load(), e.currentTime = 0, e.webkitEnterFullscreen(),
				e.play(), $(".large-video").on("ended", function() {
					e.webkitExitFullscreen()
				}).on("webkitfullscreenchange", function(i) {
					var t = document.webkitIsFullScreen,
						n = t ? "FullscreenOn" : "FullscreenOff";
					"FullscreenOff" == n && (e.pause(), $(".large-video source").attr(
						"src", ""))
				})) : (d.addClass("video-playing").append(
					'<div id="video-wrap"><video id="video-main" controls class="video-js vjs-sublime-skin" height="100%" width="100%" preload="metadata"><source src="/Content/front-page/video/Why-Go-To-School.mp4" type="video/mp4"></video></div>'), $("#video-wrap").velocity(
					"fadeIn", {
						duration: 300,
						easing: [.165, .84, .44, 1]
					}), $(".slick-active .bg-case-study-video").length && l.pause(),
				$player = videojs(document.getElementById("video-main"), {}, function() {
					this.play(), $("#video-wrap .video-js").append(
						'<a href="#" class="btn-close"><span class="line-one"></span><span class="line-two"></span><span class="line-three"></span><span class="visuallyhidden">Close video</span></a>'
					), this.on("ended", function() {
						$("#video-wrap .btn-close").trigger("click"), $videoUrl = this.I.currentSrc,
							ga("send", "event", "Stories - Video", "Story - Ended",
								"Stories Page - " + $videoUrl), $(".carousel").length && (
								$currentSlide = $carousel.slick("slickCurrentSlide") + 1, ga(
									"send", "event", "Carousel - Video", "Video - Ended", $(
										".work-copy-headings h1").text() + " - " + $currentSlide +
									" -  - " + e))
					})
				}), $(document).on("keydown.hideVideo", function(e) {
					$keyCode = e.keyCode, 27 == $keyCode && $("#video-wrap .btn-close").trigger(
						"click")
				}))
		}

		function a() {
			$(this).scrollTop() > $(".footer").outerHeight() ? d.addClass(
				"footer-visible") : d.removeClass("footer-visible")
		}

		function o() {
			$(window).width() > 800 && (window_scroll = $(this).scrollTop(), $(
				".featured-content,.carousel-info").css({
				opacity: 1 - window_scroll / 400
			}))
		}

		function r(e, i) {
			$(".over-wrap .active").velocity("finish").velocity("transition.fadeOut", {
					duration: 300
				}).removeClass("active"), $nextEl.velocity("finish").velocity(i, {
					duration: 300
				}).addClass("active"), "transition.slideRightIn" == i ? $direction =
				"Click Right" : $direction = "Click Left", ga("send", "event",
					"One Sixty Over - Buttons ", $direction, e.text())
		}
		var l, s, d = $("body"),
			c = $(".btn-menu"),
			u = $(".content-wrapper"),
			p = $(".hero"),
			h = $(".work-copy-headings");
		if (console.log(
			"Hi! Looking for an interactive job? We're hiring. http://www.160over90.com/careers"
		), d.removeClass("loading"), $.Velocity.RegisterEffect(
			"transition.fadeInHalf", {
				defaultDuration: 500,
				calls: [
					[{
						opacity: [.5, 0]
					}]
				]
			}), c.on("click", function(e) {
			if (e.preventDefault(), d.toggleClass("menu-open"), d.hasClass(
					"open-work-grid") && $(".work-grid-wrap .btn-grid").trigger("click"),
				$menuEasing = [.17, .67, .83, .67], d.hasClass("menu-open")) {
				$(".menu-wrap").velocity("finish").velocity("fadeIn", {
					duration: 450,
					easing: [.165, .84, .44, 1],
					complete: function() {
						$(".menu-work").addClass("active")
					}
				}), $(".menu-work").scrollTop(0), $(
					".square-wrap h1, .sq-top,.sq-right, .sq-bottom, .sq-left, .onepage-pagination, .arw-right, .arw-left, .btn-play-main, .carousel-info, .onepage-pagination-mobile"
				).velocity("finish").velocity({
					opacity: 0
				}, {
					mobileHA: !1
				}), $(document).on("keydown.hideMenu", function(e) {
					$keyCode = e.keyCode, 27 == $keyCode && c.trigger("click")
				}), $(".homepage").length && $(".logo").velocity("finish").velocity(
					"fadeIn", {
						duration: 450,
						easing: [.165, .84, .44, 1]
					}), $menuWork = $(".menu-work li"), $menuWork.css({
					opacity: "0"
				}), $(window).width() > 767 ? $animationOrder = [
					[0, 1, 2, 3, 4, 5, 6],
					[7, 8, 9, 10, 11, 12, 13],
					[14, 15, 16, 17, 18, 19, 20],
					[21, 22, 23, 24, 25, 26, 27],
					[28, 29, 30, 31, 32, 33, 34]
				] : $animationOrder = [
					[0, 1, 2],
					[3, 4, 5],
					[6, 7, 8],
					[9, 10, 11],
					[12, 13, 14],
					[15, 16, 17],
					[18, 19, 20],
					[21, 22, 23],
					[24, 25, 26],
					[27, 28, 29],
					[30, 31, 32],
					[33, 34, 35]
				];
				for (var i = 0; i < $animationOrder.length; i++)
					for (var t = 0; t < $animationOrder[i].length; t++) $delay = 70 + 70 *
						i, $($menuWork[$animationOrder[i][t]]).velocity("finish").velocity(
							"transition.fadeIn", {
								duration: 500,
								delay: $delay,
								easing: [.53, .19, .44, .9],
								display: "inline-block"
							});
				$url = window.location.href, ga("send", "event", "Global - UI",
					"Menu Icon - Open", $url)
			} else $(".menu-wrap").velocity("finish").velocity("fadeOut", {
				duration: 450,
				easing: [.165, .84, .44, 1],
				complete: function() {
					$(".menu-work").removeClass("active")
				}
			}), $(document).off("keydown.hideMenu"), $(".homepage").length && $(
				".logo").velocity("finish").velocity("reverse", {
				duration: 450,
				easing: [.165, .84, .44, 1]
			}), $(
				".square-wrap h1, .sq-top,.sq-right, .sq-bottom, .sq-left, .onepage-pagination, .arw-right, .arw-left, .btn-play-main, .carousel-info, .onepage-pagination-mobile"
			).velocity("reverse", {
				mobileHA: !1
			}), $url = window.location.href, ga("send", "event", "Global - UI",
				"Menu Icon - Close", $url)
		}), $(".header").headroom(), $(".homepage").length) {
			var g;
			$(".fg").each(function(e) {
				$(this).css({
					top: 100 * e + "%"
				})
			}), $(".homepage").onepage_scroll({
				sectionContainer: ".slide",
				animationTime: 1e3,
				pagination: !0,
				updateURL: !1,
				loop: !1,
				keyboard: !0,
				beforeMove: function(i, t) {
					g = window.setTimeout(function() {
						e(t)
					}, 400), $(".onepage-pagination-mobile li").removeClass("active").eq(
						i - 1).addClass("active")
				},
				afterMove: function(e, i, t) {
					homeClientName(t, e), ga("send", "event", "Brand Art", "Scroll Slide",
						$(".client-name.active span").text() + " - Slide " + e, e)
				}
			}), d.on("click", ".onepage-pagination a", function(e) {
				e.preventDefault()
			}), squareLink(), e($(".homepage .ops-section.active"))
		}
		if ($(".carousel").length) {
			if ($progCurrent = $(".progress-current"), getParameterByName("slide") ?
				$initialSlide = getParameterByName("slide") - 1 : $initialSlide = 0,
				$progCurrent.text($initialSlide + 1), $(window).smartresize(function() {
					$(window).width() > 1024 && !$(".carousel").hasClass("slick-initialized") &&
						i(), $(window).width() < 1025 && $(".carousel").hasClass(
							"slick-initialized") && $(".carousel").slick("unslick"), $(window).width() <
						1025 && Modernizr.touch ? ($(".carousel").css("padding-bottom", h.outerHeight() +
							"px"), d.removeClass("black-ui undefined-ui white-ui gray-ui")) : ($(
							".carousel").css("padding-bottom", 0), d.removeClass("open-content")),
						$(window).width() < 1025 && "undefined" == typeof s && (s = new Layzr({
							bgAttr: "data-layzr-bg",
							threshold: 70,
							callback: function() {
								this.classList.add("loaded")
							}
						}))
				}), $(window).width() > 992 && 0 == Modernizr.touch ? (i(), $(document).keydown(
					function(e) {
						$keyCode = e.keyCode, 37 == $keyCode && $carousel.slick("slickPrev"),
							39 == $keyCode && $carousel.slick("slickNext")
					}), $(".slick-slide.slick-active .video-slide").length && $(
					".btn-play-main").removeClass("hide")) : $(".carousel").css(
					"padding-bottom", h.outerHeight() + "px"), d.removeClass(
					"white-ui gray-ui").addClass($(".slick-active").attr("data-ui-color") +
					"-ui"), $(".work-grid").length) {
				$totalSlides = $(".progress-total").text();
				var v, f = $(".work-grid"),
					m = f.children();
				switch ($totalSlides) {
					case "12":
					case "16":
						v = 4;
						break;
					case "15":
					case "25":
						v = 5;
						break;
					case "18":
					case "24":
					case "30":
					case "36":
						v = 6;
						break;
					case "28":
					case "35":
					case "42":
					case "49":
						v = 7;
						break;
					case "32":
					case "40":
					case "48":
						v = 8;
						break;
					case "45":
						v = 9;
						break;
					case "50":
						v = 10;
						break;
					case "33":
					case "44":
						v = 11;
						break;
					default:
						v = 5
				}
				m.removeClass("edge").filter(":nth-child(" + v + "n)").addClass("edge"),
					$gridArray = [], $(".edge").each(function(e) {
						$newOrder = [], $newOrder.push($(this).index()), $yo = $(this).prevUntil(
							":nth-child(" + v + "n)").add($(this)), $gridArray.push($yo)
					})
			}
			$(".btn-grid").on("click", function() {
					if ($currentSlide = $carousel.slick("slickCurrentSlide") + 1, d.toggleClass(
						"open-work-grid"), $this = $(this), d.hasClass("open-work-grid")) {
						$(".work-grid-wrap").velocity({
							opacity: 1
						}, {
							visibility: "visible",
							duration: 250
						}), $(".work-grid li").css({
							opacity: "0"
						});
						for (var e = 0; e < $gridArray.length; e++) $delay = 125 + 70 * e, $(
							$gridArray[e]).velocity("transition.fadeIn", {
							duration: 500,
							delay: $delay,
							easing: [.53, .19, .44, .9]
						});
						$(document).one("keydown.hideGrid", function(e) {
							$keyCode = e.keyCode, 27 == $keyCode && $this.trigger("click")
						}), ga("send", "event", "Work - UI", "Grid Icon - Open", $(
							".work-copy-headings h1").text() + " - Slide " + $currentSlide)
					} else $(".work-grid-wrap").velocity({
						opacity: 0
					}, {
						visibility: "hidden",
						duration: 250
					}), $(document).off("keydown.hideGrid"), ga("send", "event",
						"Work - UI", "Grid Icon - Close", $(".work-copy-headings h1").text() +
						" - Slide " + $currentSlide)
				}), $(".work-grid li").hover(function() {
					$("a", this).velocity("finish").velocity({
						opacity: 1,
						scale: 1.04
					}, {
						duration: 500,
						easing: [.165, .84, .44, 1]
					})
				}, function() {
					$("a", this).velocity("reverse")
				}), $(".work-grid li").on("click", function() {
					$(".work-grid-wrap").velocity({
							opacity: 0
						}, {
							visibility: "hidden",
							duration: 250
						}), d.removeClass("open-work-grid"), $(".carousel").slick("slickGoTo",
							$(this).index(), !0), $(document).off("keydown.hideGrid"), $gridNumber =
						$(this).index() + 1, ga("send", "event", "Work - UI", "Grid Item", $(
							".work-copy-headings h1").text() + " - Slide " + $gridNumber)
				}), $(".work-grid .btn-play,.btn-play-mobile").on("click", function() {
					n($(this).attr("data-mp4"))
				}), $(window).width() < 1025 && !$(".page-template-page-careers").length &&
				($height = h.outerHeight() + $(".header").outerHeight(), u.css({
					height: "calc(100% - " + $height + "px)"
				}), u.velocity({
					translateY: "100%"
				}, {
					duration: 0
				})), h.on("click", function() {
					$(window).width() < 1025 && (d.toggleClass("open-content"), $btn = $(
							".btn-read-more-mobile"), $workHeadlinesHeight = h.outerHeight() + $(
							".header").outerHeight(), $height = $(window).height() -
						$workHeadlinesHeight, d.hasClass("open-content") ? (u.find(
								".container").scrollTop(0), $(".header").addClass("headroom--pinned")
							.removeClass("headroom--unpinned"), $btn.text("- Less"), u.velocity({
								translateY: "0%"
							}, {
								mobileHA: !0,
								easing: [.165, .84, .44, 1],
								duration: 350
							})) : ($btn.text("+ More"), u.velocity({
							translateY: "100%"
						}, {
							mobileHA: !0,
							easing: [.165, .84, .44, 1],
							duration: 350
						})))
				})
		}
		$(".case-study-video").each(function(e) {
			var i = $(this).attr("id");
			videojs(i, {
				controls: !0,
				height: "auto",
				width: "auto"
			}).ready(function() {
				this.one("play", function() {
					$pageTitle = $(".work-copy-headings h1").text(), $videoUrl = this.I
						.currentSrc, ga("send", "event", "Stories - Video",
							"Play Main Videos", $pageTitle + " - " + $videoUrl)
				}), this.on("play", function(i) {
					$(".case-study-video").each(function(i) {
						e !== i && this.player.pause()
					}), l.pause()
				}), this.on("pause", function(e) {
					$paused = 0, $(".case-study-video").each(function(e) {
						this.player.paused() && $paused++
					}), $paused == $(".case-study-video").length && l.play()
				})
			})
		}), $("a[href*=#]:not([href=#])").click(function() {
			if (location.pathname.replace(/^\//, "") == this.pathname.replace(/^\//,
				"") && location.hostname == this.hostname) {
				var e = $(this.hash);
				if (e = e.length ? e : $("[name=" + this.hash.slice(1) + "]"), e.length)
					return $("html,body").animate({
						scrollTop: e.offset().top
					}, 1e3), $currentSlide = $carousel.slick("slickCurrentSlide") + 1, ga(
						"send", "event", "Work - UI", "Click Down Arrow", $(
							".work-copy-headings h1").text() + " - Slide " + $currentSlide), !1
			}
		}), $(".hover").hover(function() {
			$hoverCount = $(this).attr("data-hover");
			for (var e = 0; e < $hoverCount; e++) $(this).append(
				'<div class="hover-piece item' + e + '"></div>')
		}, function() {
			$(this).find(".hover-piece").remove()
		}), $(".btn-play-main").on("click", function(e) {
			e.preventDefault(), $(this).addClass("hide"), $videoMp4 = $(
					".slick-active").find(".video-slide").attr("data-mp4"), n($videoMp4),
				$currentSlide = $carousel.slick("slickCurrentSlide") + 1, $videoUrl = $(
					this).attr("data-mp4"), ga("send", "event", "Carousel - Video",
					"Play Button", $(".work-copy-headings h1").text() + " - " +
					$currentSlide + " - " + $videoMp4)
		}), $(document).on("click", ".story-video", function(e) {
			e.preventDefault(), n($(this).attr("data-mp4"))
		}), $(document).on("click", "#video-wrap .btn-close", function(e) {
			e.preventDefault(), $("#video-wrap").velocity("fadeOut", {
				duration: 300,
				easing: [.165, .84, .44, 1],
				complete: function() {
					var e = document.getElementById("video-main");
					videojs(e).dispose(), d.removeClass("video-playing"), $(this).remove(),
						$(".btn-play-main").removeClass("hide"), $(
							".slick-slide.slick-active .bg-case-study-video").length && l.play(),
						$(document).off("keydown.hideVideo")
				}
			})
		}), $(window).on("scroll", function(e) {
			$(window).width() > 992 && 0 == Modernizr.touch && !$(
				".page-template-obituary").length && (a(), $(document).scrollTop() + 80 >
				u.position().top ? d.addClass("black-ui") : d.removeClass("black-ui"))
		}), $(window).width() > 992 && 0 == Modernizr.touch && a();
		var k = $("#latest-wrap .container");
		k.masonry({
			itemSelector: ".post-item",
			gutter: 20
		}), $(window).scroll(function() {
			o()
		}), $(".btn-load-more").on("click", function(e) {
			e.preventDefault(), $(".load-more").addClass("loading-latest").append(
				'<span class="loading-latest-span">Loading</span>');
			var i = parseInt($(".btn-load-more").attr("data-paged")) + 1;
			$totalPosts = $(".ajax-content").length, $(".load-more").attr("data-cat") ?
				$category = $(".load-more").attr("data-cat") : $category = !1, $.ajax({
					url: $(this).attr("data-url"),
					data: "paged=" + i + "&ajax=true&totalPosts=" + $totalPosts + "&cat=" +
						$category,
					type: "GET"
				}).done(function(e) {
					$(".load-more").removeClass("loading-latest").find(
						".loading-latest-span").remove()
				}).success(function(e) {
					$items = $(e).filter(".ajax-content"), $(".latest-content").append(
						$items), k.masonry("appended", $items, !0), $(".btn-load-more").attr(
						"data-paged", i), $(e).filter(".total-posts").length || $(
						".load-more").remove(), ga("send", "event", "Load More - Latest",
						"Link Click", i)
				})
		}), $('input[type="checkbox"]').on("click", function() {
			$(this).closest("label").toggleClass("checked"), $lblLocation = "",
				$locations = $(".locations input:checked"), $locations.length ?
				$locations.each(function(e) {
					e > 0 && ($lblLocation += ", "), $lblLocation += $(this).closest(
						"label").text()
				}) : $lblLocation = "Desired Location", $(".lbl-location").text(
					$lblLocation)
		}), $(".location-selector").on("click", function() {
			$lWrap = $(".location-wrap"), $lWrap.toggleClass("active"), $icon =
				$lWrap.find(".icon-svg use"), $iconAttr = $icon.attr("xlink:href"),
				$iconAttr = $iconAttr.split("#"), $lWrap.hasClass("active") ? ($(
					".locations label").velocity("finish").velocity(
					"transition.slideDownIn", {
						stagger: 100,
						duration: 300
					}), $icon.attr("xlink:href", $iconAttr[0] + "#icon-close")) : $icon.attr(
					"xlink:href", $iconAttr[0] + "#caret-down")
		}), $(".over-wrap").length && $(document).off("keydown").keydown(function(e) {
			$keyCode = e.keyCode, 37 == $keyCode && $(
				".one-sixty-over .arw-wrap.left").trigger("click"), 39 == $keyCode && $(
				".one-sixty-over .arw-wrap.right").trigger("click")
		}), $(".one-sixty-over .arw-wrap.right").on("click", function() {
			$active = $(".over-wrap .active"), $active.next("li").length ? $nextEl =
				$active.next("li") : $nextEl = $(".over-wrap li").first(), r($nextEl,
					"transition.slideRightIn")
		}), $(".one-sixty-over .arw-wrap.left").on("click", function() {
			$active = $(".over-wrap .active"), $active.prev("li").length ? $nextEl =
				$active.prev("li") : $nextEl = $(".over-wrap li").last(), r($nextEl,
					"transition.slideLeftIn")
		}), $(window).width() < 1025 && (s = new Layzr({
			bgAttr: "data-layzr-bg",
			threshold: 70,
			callback: function() {
				this.classList.add("loaded")
			}
		})), $(".form-careers").length && ($.validator.addMethod("phoneUS",
			function(e, i) {
				return e = e.replace(/\s+/g, ""), this.optional(i) || e.length > 9 && e.match(
					/^(\+?1-?)?(\([2-9]([02-9]\d|1[02-9])\)|[2-9]([02-9]\d|1[02-9]))-?[2-9]([02-9]\d|1[02-9])-?\d{4}$/
				)
			}, "Please specify a valid phone number"), $.validator.addMethod("accept",
			function(e, i, t) {
				var n, a, o = "string" == typeof t ? t.replace(/\s/g, "").replace(/,/g,
						"|") : "image/*",
					r = this.optional(i);
				if (r) return r;
				if ("file" === $(i).attr("type") && (o = o.replace(/\*/g, ".*"), i.files &&
					i.files.length))
					for (n = 0; n < i.files.length; n++)
						if (a = i.files[n], !a.type.match(new RegExp("\\.?(" + o + ")$", "i")))
							return !1;
				return !0
			}, $.validator.format("Please upload a PDF or ZIP.")), $validator = $(
			".form-careers").validate({
			rules: {
				number: {
					number: !0
				},
				work_samples: {
					required: !0,
					accept: "application/pdf,application/zip"
				},
				resume: {
					required: !0,
					accept: "application/pdf,application/zip"
				},
				email: {
					required: !0,
					email: !0
				},
				number: {
					phoneUS: !0
				},
				website: {
					url: !0
				}
			}
		})), $(".square-wrap").on("click", function() {
			$link = $(".square-wrap").attr("href"), $index = $(".client-name.active")
				.index(), ga("send", "event", "Brand Art", "Link Click", $link, $index)
		}), $(".related-wrap a").on("click", function() {
			$linkText = $(this).text(), ga("send", "event", "Related Work",
				"Link Click", "From: " + $(".work-copy-headings h1").text() + " To: " +
				$linkText)
		}), $(".menu-work a").on("click", function() {
			ga("send", "event", "Main Menu - Client", "Logo Click", $(this).find(
				"strong").text())
		}).on("mouseenter", function() {
			ga("send", "event", "Main Menu - Client", "Logo Hover", $(this).find(
				"strong").text())
		}), $(".carousel").length && ($(".arw-wrap.left").on("click", function() {
			$currentSlide = $carousel.slick("slickCurrentSlide") + 1, ga("send",
				"event", "Carousel - Buttons ", "Click Left", $(
					".work-copy-headings h1").text() + " - Slide " + $currentSlide)
		}), $(".arw-wrap.right").on("click", function() {
			$currentSlide = $carousel.slick("slickCurrentSlide") + 1, ga("send",
				"event", "Carousel - Buttons ", "Click Right", $(
					".work-copy-headings h1").text() + " - Slide " + $currentSlide)
		})), $(".work-grid .btn-play").on("click", function() {
			$videoUrl = $(this).attr("data-mp4"), $currentSlide = $carousel.slick(
				"slickCurrentSlide") + 1, ga("send", "event", "Work Thumbnails - Video",
				"Play Button", $videoUrl)
		}), $(".btn-play-mobile").on("click", function() {
			$videoUrl = $(this).attr("data-mp4"), $currentSlide = $carousel.slick(
				"slickCurrentSlide") + 1, ga("send", "event", "Mobile Work - Video",
				"Play Button", $(".work-copy-headings h1").text() + " - " +
				$currentSlide + " - " + $videoUrl)
		}), $(document).on("click", ".featured-content .story-video", function() {
			$videoUrl = $(this).attr("data-mp4"), ga("send", "event",
				"Stories - Video", "Featured Story - Watch Button", "Stories Page - " +
				$videoUrl)
		}).on("click", "#stories-wrap .story-img.story-video", function() {
			$videoUrl = $(this).attr("data-mp4"), ga("send", "event",
				"Stories - Video", "Story - Play Button", "Stories Page - " + $videoUrl
			)
		}).on("click", "#stories-wrap .btn.story-video", function() {
			$videoUrl = $(this).attr("data-mp4"), ga("send", "event",
				"Stories - Video", "Story - Watch Button", "Stories Page - " +
				$videoUrl)
		}), $(".logo").on("click", function() {
			$url = window.location.href, ga("send", "event", "Global - UI",
				"160over90 Logo", $url)
		}), $(".social a").on("click", function(e) {
			$url = window.location.href, $networkName = $(this).find(
				".visuallyhidden").text(), ga("send", "event", "Global - UI",
				$networkName + " Link", $url)
		}), $(".office-contact-wrap a").on("click", function() {
			$url = window.location.href, $link = $(this).attr("href"), ga("send",
				"event", "Contact Link", $link, $url)
		}), $(".other-location-wrap a").on("click", function() {
			$linkText = $(this).text(), ga("send", "event", "Other Locations",
				"Link Click", "From: " + $(".work-copy-headings h1").text() + " To: " +
				$linkText)
		}), $(".normal-layout .img-featured").on("click", function() {
			$link = $(this).attr("href"), ga("send", "event", "Latest Links",
				"Image Click", $link)
		}), $(".normal-layout .post-excerpt h2 a").on("click", function() {
			$link = $(this).attr("href"), ga("send", "event", "Latest Links",
				"Heading Click", $link)
		}), $(".normal-layout .post-excerpt .cta").on("click", function() {
			$link = $(this).attr("href"), ga("send", "event", "Latest Links",
				"CTA Click", $link)
		}), $(".normal-layout .meta a").on("click", function() {
			$link = $(this).attr("href"), ga("send", "event", "Latest Links",
				"Archive Click", $link)
		}), $("a.back").on("click", function() {
			$postTitle = $(".blog-content-wrap h1").text(), $nextUrl = $(this).attr(
				"href"), ga("send", "event", "Related Posts", "Link Click", "From: " +
				$postTitle + " To: " + $nextUrl)
		}), $(".menu-item a").on("click", function() {
			$linkText = $(this).text(), ga("send", "event", "Global - UI",
				"Link Click", $linkText)
		})
	}), $(window).load(function() {
		$(".homepage").length && $(
			".fg-wrap, .homepage, .onepage-pagination, .onepage-pagination-mobile").velocity({
			opacity: 1
		}, {
			duration: 600,
			mobileHA: !1,
			complete: function() {
				window.setTimeout(homeClientName, 1e3), $(".home-loading").removeClass(
					"home-loading")
			}
		})
	}),
	function(e) {
		e(document).on("ready", function() {
			var i = e("#icims-jobs-index"),
				t = e("#icims-apply");
			i.length && e.ajax(i.data("url"), {
				data: {
					action: "icims_jobs_index"
				}
			}).then(function(e) {
				i.html(e)
			}), t.length && t.on("click", function() {
				ga("send", "event", "Global - UI", "Apply Button Click", e(
					"#icims-job-title").text())
			})
		})
	}(jQuery);