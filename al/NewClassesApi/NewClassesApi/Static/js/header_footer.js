if ($(window).width() > 750) {
	$(".nav-href").click(function () {
		if ($(this).siblings('.li-show').hasClass('show')) {
			$(this).siblings('.li-show').stop().removeClass('show');
			$(this).siblings('.li-show').animate({
				'overflow': 'none',
				'height': '0px',
			}, 200);
		} else {
			$(".nav>ul>li>div").stop().animate({
				'overflow': 'none',
				'height': '0px'
			}, 200).removeClass('show');
			$(this).siblings('.li-show').addClass('show');
			$(this).siblings('.li-show').stop().animate({
				'height': $(this).siblings('.li-show').children().length * 40 + 'px',
			}, 200);
		}
	})
}
var flag = true;
$(".munes").click(function () {
	$(".nav-bar").slideToggle();
	if (flag) {
		$(this).addClass('x');
	} else {
		$(this).removeClass('x');
	}
	flag = !flag;
	$(".nav-href").attr("href", "javascript:;")
});
