$(document).ready(function () {
	var fullscreen = "showFullscreen=";
	if (window.location.href.indexOf(fullscreen) > -1) {
       	$('a').each(function () {
			var href = $(this).attr('href');
			if (href && (href.indexOf('javascript:') == -1 && href.indexOf('mailto:') == -1 && href.indexOf('#') != 0)) {
				if(href.indexOf('?') > -1) {
					href += "&" + fullscreen;
				} else {
					href += "?" + fullscreen;
				}
				$(this).attr('href', href);
			}
		});
    }
});