/*************************** Variables ******************************/

var memeSize = 400; // Canvas and image size in pixels. Must have square aspect ratio (1:1)

var canvas = document.getElementById('condorimeme'); // Canvas node
ctx = canvas.getContext('2d'); // Canvas initialization
canvas.width = memeSize; // Canvas width set 
canvas.height = memeSize; // Canvas height set

var img = document.getElementById("default"); // Image instance
var text = document.getElementById('txt'); // Text input node
var json; // JSON initialization

var txtX = 0; // Text X coordinate
var txtY = 0; // Text Y coordinate
var txtWidth = 0; // Text globe width
var txtLength = 0; // Text max length
var txtLineHeight = 24; // Text line height constant

var minLengthShare = 5; // Minimum characters required in order to enable sharing and download buttons

var popup = "height=300,width=600,status=yes,toolbar=no,menubar=no,location=no"; // Share popup parameters

/**************************** Events *******************************/

$(window).load(function () {
    $.ajax({ // Calling Memes JSON
        cache: false,
        url: "/DesktopModules/Digevo/Digevo.Condorimeme/img/src.json",
        dataType: "json",
        success: function(data) {
            json = data;
            $('content > .overlay').fadeOut(300);
        }
    });

    $('.canvas_container').waitForImages(function () { // Draw default image on canvas
        refreshImage();
    });

    $('.gallery .img').each(function (i, obj) { // Cast loading effect on every not loaded image in gallery
        $(this).waitForImages(function () {
            $(this).children('.overlay').fadeOut(200);
        });
    });

    var meta = $('.og_container .meta > span');
    if(meta.length > 0)
    {
        var date = $('.og_container .og_frame > img').attr('id');
        var year = date.substring(2, 4);
        var month = date.substring(4, 6);
        var day = date.substring(6, 8);
        var hour = date.substring(8, 10);
        var minutes = date.substring(10, 12);

        meta.html('CREADO EL ' + day + '/' + month + '/' + year + ' A LAS ' + hour + ':' + minutes + 'HRS.');
    }

    $('.history_container').scrollbox({
        direction: 'h',
        distance: 163
    });
});

$(document).ready(function () {
    setTimeout(function () {
        $('.rel_container > .overlay').removeAttr('style');
    }, 400);
	
	var querystring = "&showFullscreen=";
	if(window.location.href.indexOf("showFullscreen=") > -1) {
       	$('.condorimemes a').each(function()
		{
			var href = $(this).attr('href');
				href = href + querystring;
				$(this).attr('href', href);
		});
    }
	
	$('.url > input').attr('value', window.location.href).attr('placeholder', window.location.href);
	
});

$('.gallery').on('click', 'img', function () { // Delegated draw image
    img = $(this).get(0);
    changeImage();

    var overlay = $('.meme_container > .overlay');
    if (overlay.is(':visible')) {
        overlay.fadeOut(200);
    }

    var name = $(this).attr('id');
    if (name in json)
    {
        txtX = (json[name].txtX != null) ? json[name].txtX : json.default.txtX;
        txtY = (json[name].txtY != null) ? json[name].txtY : json.default.txtY;
        txtWidth = (json[name].txtWidth != null) ? json[name].txtWidth : json.default.txtWidth;
        txtLength = (json[name].txtLength != null) ? json[name].txtLength : json.default.txtLength;
    }
    else
    {
        txtX = json.default.txtX;
        txtY = json.default.txtY;
        txtWidth = json.default.txtWidth;
        txtLength = json.default.txtLength;
    }
});

$('.make').click(function () {
    upload();
});

$('.download_link').click(function () {
    var overlay = $('content > .overlay')
    setTimeout(function () {
        location.reload(true);
    }, 4000);
});

$('.factory').click(function () {
    $('#meme').slideUp(300, function () {
        $('#factory').slideDown(300).removeClass('hidden');
        $('.goback').fadeIn(300);
    });
});

$('.preview').click(function () {
    $('.goback').fadeOut(300);
    $('#factory').slideUp(300, function () {
        $('#meme').slideDown(300);
    });
});

$('.facebook').click(function () {
    window.open("http://www.facebook.com/sharer.php?u=" + window.location.href, null, popup);
});

$('.twitter').click(function () {
    window.open("http://twitter.com/share?text=%23Condorimeme&url=" + window.location.href, null, popup)
});

$('.whatsapp').click(function () {
    window.open("whatsapp://send?text=" + encodeURIComponent("¡Mira el #Condorimeme que he creado! " + window.location.href), null, popup)
});

/*************************** Listeners *****************************/

text.addEventListener('keydown', drawMeme);
text.addEventListener('keyup', drawMeme);
text.addEventListener('change', drawMeme);

/*************************** Functions *****************************/

function changeImage() {
    var time = 150; // Transition fade-in-out time (ms)

    $('.canvas_frame > .overlay').fadeIn(time, function () {
        drawMeme();
        $(this).fadeOut(time);
    });
}

function refreshImage() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.drawImage(img, 0, 0, memeSize, memeSize);

    var input = $('#txt').val();
    if (input.length > txtLength) { // Cut string if needed
        $('#txt').val(input.substring(0, txtLength));
    }

    if ($('#txt').attr('maxLength') != txtLength) { // Change input maxlength if needed
        $('#txt').attr('maxLength', txtLength);
        $('.counter > #total').html(txtLength); // Also, refresh total counter.
    }

    $('.counter > #current').html($('#txt').val().length); // Refresh current characters
    var rel = $('.rel_container > .overlay');

    if (input.length < minLengthShare) {
        if (!rel.is(':visible')) {
            rel.fadeIn(200);
        }
    } else {
        if (rel.is(':visible')) {
            rel.fadeOut(200);
        }
    }
}

function drawMeme() {
    refreshImage();

    ctx.lineWidth = 0;
    ctx.font = '18pt "Walter Turncoat"';
    ctx.textAlign = 'center';
    ctx.textBaseline = 'top';

    var _text = text.value;
    _text = _text.toUpperCase();

    wrapText(ctx, _text, txtX, txtY, txtWidth, txtLineHeight);
}

function wrapText(context, text, x, y, maxWidth, lineHeight) {
    var pushMethod = 'push';
    var lines = [];
    var y = y;
    var line = '';
    var words = text.split(' ');

    for (var n = 0; n < words.length; n++) {
        var testLine = line + ' ' + words[n];
        var metrics = context.measureText(testLine);
        var testWidth = metrics.width;

        if (testWidth > maxWidth) {
            lines[pushMethod](line);
            line = words[n] + ' ';
        } else {
            line = testLine;
        }
    }
    lines[pushMethod](line);

    for (var k in lines) {
        context.strokeText(lines[k], x, y + lineHeight * k);
        context.fillText(lines[k], x, y + lineHeight * k);
    }
}

function upload(action) {
    var time = 400;
    var rel = $('.rel_container > .overlay');
    var span = $('.rel_container > .overlay span');
    var overlayGallery = $('.gallery_container > .overlay');
    var overlayMeme = $('.meme_container > .overlay');

    if (!rel.hasClass('solid')) {
        rel.addClass('solid');
        rel.addClass('loading2');
        span.html('CONTACT&Aacute;NDONOS CON PELOTILLEHUE');
    }

    rel.fadeIn(time);
    overlayGallery.fadeIn(time);
    overlayMeme.fadeIn(time);

    var img64 = canvas.toDataURL("image/jpeg", 0.8);
    img64 = img64.replace(/^data:image\/jpeg;base64,/, "");

    var dataToSend = { "img64": img64 };

     setTimeout(function () {
        $.ajax({
            method: 'POST',
            url: window.location.href,
            dataType: "json",
            data: dataToSend,
            success: function (data) {
                console.log(data);
                uploadCompleted(data.name);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(errorThrown);
                $('.rel_container > .overlay').removeClass('loading2');
                $('.rel_container > .overlay .table .cell').html('<span>OCURRI&Oacute; UN ERROR. INT&Eacute;NTALO DENUEVO O M&Aacute;S TARDE.</span>');
                setTimeout(function () {
                    overlayGallery.fadeOut(time);
                    overlayMeme.fadeOut(time);
                    $('.rel_container > .overlay').fadeOut(time, function () {
                        overlayReset();
                    });
                }, 3000);
            }
        });
    }, 1000);
}

function uploadCompleted(name) {
    var rel = $('.rel_container > .overlay');
    var content = $('.rel_container > .overlay .table .cell');
	var aurl = "http://" + window.location.host + "/Condorimemes/tabid/5416/Default.aspx?id=" + name;
	if(window.location.href.indexOf("showFullscreen=") > -1) { aurl += "&showFullscreen="; }
	
    rel.removeClass('loading2');
    content.html('<span>&iexcl;LISTO! TE LLEVAREMOS A TU NUEVO CONDORIMEME EN UNOS SEGUNDOS O</span> <a class="make_link" href="' + aurl + '">HAZ CLIC AQU&Iacute</a><span>.</span>');

    setTimeout(function () {
        window.location.href = aurl;
    }, 3000);
}

function overlayReset() {
    $('.rel_container > .overlay').addClass('loading2');
    $('.rel_container > .overlay .table .cell').html('<br /><span>CONTACT&Aacute;NDONOS CON PELOTILLEHUE</span>');
}