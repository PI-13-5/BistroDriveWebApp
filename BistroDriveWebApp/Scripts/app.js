(function (michaels, $) {
    michaels.initialize = function () {
        michaels.carousel.init();
        michaels.tabbing.init();
    }
})(window.michaels = window.michaels || {}, jQuery);

// carousel setting is here
(function (michaels, $) {
    michaels.carousel = {
        setting: {
            'wrap': 'circular'
        },
        init: function () {
            var jcarousel = $('.jcarousel'),
                // setting of slide number in single time		
            moveSlide = "5";
            screenWidth = $(document).width();
            // add movement of slide here for different resolution		
            if (screenWidth >= 768 && screenWidth <= 1024) {
                moveSlide = "4";
            } else if (screenWidth >= 320 && screenWidth <= 480) {
                moveSlide = "2";
            }
            jcarousel.on('jcarousel:reload jcarousel:create', function () {
                var width = jcarousel.innerWidth();
                jcarousel.jcarousel('items').css('width', width + 'px');
            }).jcarousel(michaels.carousel.setting);
            //alert(moveSlide);
            $('.jcarousel-control-prev')
                .jcarouselControl({
                    target: '-=' + moveSlide
                });

            $('.jcarousel-control-next')
                .jcarouselControl({
                    target: '+=' + moveSlide
                });
            // make touch enable slider	
            $(".horizontalSlider").swipe({
                swipeLeft: function () {
                    $(this).find(".jcarousel-control-next").click()
                },
                swipeRight: function () {
                    $(this).find(".jcarousel-control-prev").click()
                }
            })

            // for creating pagination dots		
            $('.jcarousel-pagination').on('jcarouselpagination:active', 'a', function () {
                $(this).addClass('active');
            }).on('jcarouselpagination:inactive', 'a', function () {
                $(this).removeClass('active');
            }).on('click', function (e) {
                e.preventDefault();
            }).jcarouselPagination({
                perPage: moveSlide,
                item: function (page) {
                    return '<a href="#' + page + '">' + page + '</a>';
                }
            });
        }
    }
})(window.michaels = window.michaels || {}, jQuery);

// tabbing js
(function (michaels, $) {
    michaels.tabbing = {
        init: function () {
            $(".tab li, select.mobile option").each(function () {
                var _this = $(this),
                    target = _this.attr('data-target');
                _this.on('click', function () {
                    _this.addClass("active").siblings().removeClass('active');
                    $("#" + target).addClass("active").siblings().removeClass("active");
                })
            })
        }
    }
})(window.michaels = window.michaels || {}, jQuery);

$(document).ready(function () {
    michaels.initialize();
})