$(document).ready(function () {
    $('.navbar li span').click(function () {
        if ($('.navbar li span i').hasClass("color")) {

            $('.navbar li span i').removeClass("color")
        }
        $(this).find("i").addClass("color");
        $(this).next().slideToggle();
        if ($(this).find("i").hasClass("fa-plus")) {
            $(this).find("i").removeClass("fa-plus");
            $(this).find("i").addClass("fa-minus");
        } else {
            $(this).find("i").removeClass("fa-minus");
            $(this).find("i").addClass("fa-plus");
        }

    });
    $(".navbar-toggler").click(function () {
        if ($(this).find(".navbar-toggler-icon").hasClass("active")) {
            $(this).find(".navbar-toggler-icon").removeClass("active");
            $(this).find(".navbar-toggler-times").addClass("active");
        } else {
            $(this).find(".navbar-toggler-times").removeClass("active");
            $(this).find(".navbar-toggler-icon").addClass("active");
        }
    });



   $(window).resize(function(){
    if ($(window).width() >= 991) {

        $(".navbar-main").addClass("active");
        $(".navbar-expand-lg").addClass("deactive");
    } else {
        $(".navbar-main").removeClass("active");
        $(".navbar-main").addClass("deactive");

        $(".navbar-expand-lg").removeClass("deactive");
    }
   })


    $('.slider-active').owlCarousel({
        loop: true,
        nav: true,
        margin: 10,
        item: 1,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });

    $('.product-slider-active').owlCarousel({
        loop: true,
        nav: false,
        autoplay: false,
        autoplayTimeout: 5000,
        item: 4,
        margin: 30,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 2
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1170: {
                items: 3
            },
            1300: {
                items: 4
            }
        }
    })

    
    $(".active .slider-content h1").addClass("bounceInDown")
    $(".active h2").addClass("bounceInRight")
    $(".active p").addClass("fadeIn")
  
    $(".owl-next, .owl-prev").click(function(){
        $(".slider-content h1").removeClass("bounceInDown").addClass("animated")
        $(".active .slider-content h1").addClass("bounceInDown")
        $("h2").removeClass("bounceInRight").addClass("animated")
        $(".active h2").addClass("bounceInRight")
        $("p").removeClass("fadeIn").addClass("animated")
        $(".active p").addClass("fadeIn")
        
    })

    new WOW().init();

    $(".product-tab-list a:first").addClass("active");
    $(".tab-pane").hide();
    $(".tab-pane:first").show();
    $(".product-tab-list a").click(function(e){
        e.preventDefault()
        $(".product-tab-list a").removeClass("active");
        $(this).addClass("active");
        $(".tab-pane").hide();
        var activeTab=$(this).attr("href");
        $(activeTab).show();
    })


    $('.brand-logo-active').owlCarousel({
        loop: true,
        nav: true,
        autoplay: false,
        autoplayTimeout: 5000,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        item: 5,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1000: {
                items: 5
            }
        }
    })
    

    $.scrollUp({
        scrollText: '<i class="fa fa-angle-double-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });

    //var CartPlusMinus = $('.cart-plus-minus');
    //CartPlusMinus.prepend('<div class="dec qtybutton">-</div>');
    //CartPlusMinus.append('<div class="inc qtybutton">+</div>');
    //$(".qtybutton").on("click", function() {
    //    var $button = $(this);
    //    var oldValue = $button.parent().find("input").val();
    //    if ($button.text() === "+") {
    //        var newVal = parseFloat(oldValue) + 1;
    //    } else {
    //        // Don't allow decrementing below zero
    //        if (oldValue > 0) {
    //            var newVal = parseFloat(oldValue) - 1;
    //        } else {
    //            newVal = 1;
    //        }
    //    }
    //    $button.parent().find("input").val(newVal);
    //});

    $('.quickview-active').owlCarousel({
        loop: true,
        autoplay: false,
        autoplayTimeout: 5000,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        nav: true,
        item: 3,
        margin: 12,
    })

    var ProductDetailsSmall = $('.product-details-small a');
    $('.product-details-large .tab-pane:first-child').addClass('active');
    ProductDetailsSmall.click( function(e) {
        e.preventDefault();
        var $href = $(this).attr('href');
        ProductDetailsSmall.removeClass('active');
        $(this).addClass('active');
        $('.product-details-large .tab-pane').removeClass('active');
        $('.product-details-large ' + $href).addClass('active');
    })

    $('.count').each(function () {
        $(this).prop('Counter',0).animate({
            Counter: $(this).text()
        }, {
            duration: 1000,
            easing: 'swing',
            step: function (now) {
                $(this).text(Math.ceil(now));
            }
        });
    });

    $('.testimonial-active').owlCarousel({
        loop: true,
        nav: false,
        autoplay: false,
        autoplayTimeout: 5000,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        item: 1,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    })

    $(".shop-area .shop-topbar .grid-options ul li").click(function(){
        var $proStyle = $(this).data('view');
        $(".shop-topbar .grid-options li").removeClass("active-color");
        $(this).addClass("active-color");
        $('.product-view').removeClass('product-grid product-list').addClass($proStyle);
    
    })

    var sliderrange = $('#slider-range');
    var amountprice = $('#amount');
    var min = sliderrange.data("min");
    var max = sliderrange.data("max");
    var sltmax = sliderrange.data("sltmax");
    var sltmin = sliderrange.data("sltmin");
    
    $(function() {
        sliderrange.slider({
            range: true,
            min: min,
            max: max,
            values: [sltmin, sltmax],
            slide: function(event, ui) {
                amountprice.val(ui.values[0] + "-" + ui.values[1]);
            }
        });
        amountprice.val(sliderrange.slider("values", 0) +
            "-" + sliderrange.slider("values", 1));
    });

  
    $('.product-dec-slider').owlCarousel({
        loop: true,
        autoplay: false,
        autoplayTimeout: 5000,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        nav: true,
        item: 4,
        margin: 12,
        responsive: {
            0: {
                items: 2
            },
            768: {
                items: 3
            },
            1000: {
                items: 4
            }
        }
    })


    $(".description-review-topbar a:first").addClass("active");
    $(".description-review-bottom .tab-pane").removeClass("active")
    $(".description-review-bottom .tab-pane:first").addClass("active")
    $(".description-review-topbar a").click(function(e){
        e.preventDefault()
        $(".description-review-topbar a").removeClass("active");
        $(this).addClass("active");
        $( ".description-review-bottom .tab-pane").removeClass("active")
        var activeTab=$(this).attr("href");
        $(activeTab).addClass("active");
    })

    $('.related-product-active').owlCarousel({
        loop: true,
        nav: false,
        autoplay: false,
        autoplayTimeout: 5000,
        item: 4,
        margin: 30,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 2
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1170: {
                items: 3
            },
            1300: {
                items: 4
            }
        }
    })

    $(".login-tab-list a:first").addClass("active");
    $(".login-register .tab-pane").removeClass("active")
    $(".login-register .tab-pane:first").addClass("active")
    $(".login-tab-list a").click(function(e){
        e.preventDefault()
        $(".login-tab-list a").removeClass("active");
        $(this).addClass("active");
        $(".tab-pane").removeClass("active")
        var activeTab=$(this).attr("href");
        $(activeTab).addClass("active");
    })
 

    // map
    
    function init() {
        var mapOptions = {
            zoom: 11,
            scrollwheel: false,
            center: new google.maps.LatLng(40.709896, -73.995481),
            styles: 
            [
                {
                    "featureType": "all",
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "saturation": 36
                        },
                        {
                            "color": "#878787"
                        },
                        {
                            "lightness": 40
                        }
                    ]
                },
                {
                    "featureType": "all",
                    "elementType": "labels.text.stroke",
                    "stylers": [
                        {
                            "visibility": "on"
                        },
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 16
                        }
                    ]
                },
                {
                    "featureType": "all",
                    "elementType": "labels.icon",
                    "stylers": [
                        {
                            "visibility": "off"
                        }
                    ]
                },
                {
                    "featureType": "administrative",
                    "elementType": "geometry.fill",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 20
                        }
                    ]
                },
                {
                    "featureType": "administrative",
                    "elementType": "geometry.stroke",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 17
                        },
                        {
                            "weight": 1.2
                        }
                    ]
                },
                {
                    "featureType": "landscape",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 20
                        }
                    ]
                },
                {
                    "featureType": "poi",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 21
                        }
                    ]
                },
                {
                    "featureType": "road.highway",
                    "elementType": "geometry.fill",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 17
                        }
                    ]
                },
                {
                    "featureType": "road.highway",
                    "elementType": "geometry.stroke",
                    "stylers": [
                        {
                            "color": "#444444"
                        },
                        {
                            "lightness": 29
                        },
                        {
                            "weight": 0.2
                        }
                    ]
                },
                {
                    "featureType": "road.arterial",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 18
                        }
                    ]
                },
                {
                    "featureType": "road.local",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 16
                        }
                    ]
                },
                {
                    "featureType": "transit",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#000000"
                        },
                        {
                            "lightness": 19
                        }
                    ]
                },
                {
                    "featureType": "water",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#2d333c"
                        },
                        {
                            "lightness": 17
                        }
                    ]
                }
            ]
        };
        var mapElement = document.getElementById('map');
        var map = new google.maps.Map(mapElement, mapOptions);
        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(40.709896, -73.995481),
            map: map,
            icon: 'img/maps.png',
            animation:google.maps.Animation.BOUNCE,
            title: 'Snazzy!'
        });
    }
    google.maps.event.addDomListener(window, 'load', init);
})
