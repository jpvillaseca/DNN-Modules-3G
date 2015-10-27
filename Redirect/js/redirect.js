$(window).load(function () { 	
	$('.rest_switch').click(function() {
		$('.rest_wrapper').slideToggle('fast', function() {
		if($('.rest_switch > span').hasClass('fa-angle-double-down')) {
			$('.rest_switch > span').removeClass('fa-angle-double-down').addClass('fa-angle-double-up');
		} else {
			$('.rest_switch > span').removeClass('fa-angle-double-up').addClass('fa-angle-double-down');
		}
		});
	});
});

function init(config)
{
	$.get("http://www.telize.com/geoip", function (data) {
		console.log(data);
		if(config[data.country_code])
		{
			$('#redirect_country').html(data.city + ', ' + data.country);
			$('#redirect_url').attr('href', config[data.country_code]);
			$('.redirect_wrapper').fadeIn(350, function() {
				setTimeout(function() {
					window.location.href = config[data.country_code];
				}, 4000);
			});
		}
	}, "jsonp");
}
