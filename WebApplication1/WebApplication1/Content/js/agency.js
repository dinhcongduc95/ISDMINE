// Agency Theme JavaScript

(function($) {
    "use strict"; // Start of use strict

    // jQuery for page scrolling feature - requires jQuery Easing plugin
    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top - 50)
        }, 1250, 'easeInOutExpo');
        event.preventDefault();
    });

    // Highlight the top nav as scrolling occurs
    $('body').scrollspy({
        target: '.navbar-fixed-top',
        offset: 51
    });
	
	$('body').scrollspy({
        target: '.runningBanner-top',
        offset: 51
    });

    // Closes the Responsive Menu on Menu Item Click
    $('.navbar-collapse ul li a:not(.dropdown-toggle)').click(function() {
        $('.navbar-toggle:visible').click();
    });

    // Offset for Main Navigation
    $('#mainNav').affix({
        offset: {
            top: 100
        }
    });
	function testScroll(ev){
    if(window.pageYOffset>200)alert('User has scrolled at least 400 px!');
	}
	window.onscroll=testScroll
	
})(jQuery); // End of use strict

// Running banner
$( document ).ready(function() {
	$('.thumbnail-label').each(function(){
		$(this).html($(this).text().substring(0, 18) + ' ...');			
	});
	$('#minusBtn').click(function(){
		$('.slider-horizontal').addClass('slider-horizontal-closed');
		$('#bannerContent').css("display", "none");
		$('.floatingText').css('display', 'inline');
		$('#minusBtn').toggleClass('hideBtn');
		$('#plusBtn').toggleClass('hideBtn');
		$('.collapseBtn').addClass('collapseBtn-collapsed');
		$('.runningBanner').addClass('shrink');
	});
	$('#plusBtn').click(function(){
		$('#bannerContent').css("display", "inline");
		$('.floatingText').css('display', 'none');
		$('#minusBtn').toggleClass('hideBtn');
		$('#plusBtn').toggleClass('hideBtn');
		$('.collapseBtn').removeClass('collapseBtn-collapsed');
		$('.runningBanner').removeClass('shrink');
	});
	
	// moving thumbnail
	$('#carousel-example-generic').on('slid.bs.carousel', function () {
		var node = $('.item.active').attr('node');
		$( "[id*='node']" ).each(function(){
			$(this).find('img').removeClass('activeNode');
			$(this).find('i').css('opacity', '0');
			$(this).find('div').removeClass('thumbnail-mover-1');
			$(this).find('.thumbnail-label-slideshow').removeClass('blur');
		});
		$('#node'+node).find('img').addClass('activeNode');			
		$('#node'+node).find('i').css('opacity', '1');
		$('#node'+node).find('.thumbnail-label-div').addClass('thumbnail-mover-1');
		$('#node'+node).find('.thumbnail-label-slideshow').addClass('blur');
	});
	
	$('.img-circle').each(function(){
		var calId = $(this).attr('id').split('-')[2];
		$(this).click(function(){
			$(this).toggleClass('shadow-cast');
			$('#callendar-' + calId).toggleClass('display-scale-up');
			
			$('#callendar-icon-' + calId).toggleClass('display-scale-down');
			
		});
	});
});

// Popover toggle
$(document).ready(function(){
    $('[data-toggle="popover"]').popover();
	$('[data-toggle="popover"]').popover();
});

// scroll spy 
function testScroll(ev){
	if(window.pageYOffset>101){
		$('#randomBanner').removeClass('hidden');
	}else{
		$('#randomBanner').addClass('hidden');
	}
}
window.onscroll=testScroll;

// Change item in running banner
var curInd = 0;
var curIndHorizontal = curInd;
var max = 2;
setInterval(function(){
	$('.slider').eq(curInd).removeClass('closed');
	$(".slider:not(:eq("+ curInd +"))").addClass('closed');
	$('.slider-horizontal').eq(curInd).removeClass('closed-horizontal');
	$(".slider-horizontal:not(:eq("+ curInd +"))").addClass('closed-horizontal');		
	if(curInd == max){
		curInd = 0;
	}else{
		curInd++;
	}
}, 3000);
setInterval(function(){
	$('.slider-horizontal').eq(curInd).toggleClass('closed-horizontal');
	$(".slider-horizontal:not(:eq("+ curInd +"))").toggleClass('closed-horizontal');	
	if(curIndHorizontal == max){
		curIndHorizontal = 0;
	}else{
		curIndHorizontal++;
	}
}, 7000);