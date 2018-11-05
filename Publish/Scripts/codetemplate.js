var showThumb = (function (e) {
    if (typeof FileReader == "undefined") return true;
    var elem = $(this);
    var files = e.target.files;
    for (var i = 0, f; f = files[i]; i++) {
        if (f.type.match('image.*')) {
            var reader = new FileReader();
            var previewDiv = $('.thumbnail', elem.parent());
            reader.onload = (function (theFile) {
                return function (e) {
                    var image = e.target.result;
                    if ($(previewDiv).children().length) {
                        console.log('efa misy image ato');
                        previewDiv.children().remove();
                        previewDiv.append('<img src="' + image + '" />');
                    }

                    else {
                        previewDiv.append('<img src="' + image + '"/>');
                    }
                };
            })(f);
            reader.readAsDataURL(f);
        }
    }
});
/*
Miniatures photos
--------------------------------------------------------------------------------------------------
var showThumbPhotos = (function (e) {
    var fileSize = (this.files[0].size / 1024 / 1024),
        elem = $(this),  
        files = e.target.files,
        previewDiv = $('.thumbnail-photo', elem.parent()),
        errorMessage = '<p class="errorMaximage">Fichier trop volumineux, veuillez reessayer</p>';
    if (typeof FileReader == "undefined" || fileSize > 1 ) {     
        previewDiv.children().remove();
        previewDiv.append(errorMessage);
        return true;
    }
    for (var i = 0, f; f = files[i]; i++) {
        if (f.type.match('image.*')) {
            var reader = new FileReader();
            reader.onload = (function (theFile) {
                return function (e) {
                    var image = e.target.result;
                    if ($(previewDiv).children().length) {
                        previewDiv.children().remove();
                        previewDiv.append('<img src="' + image + '" />');
                    }
                    else {
                        previewDiv.append('<img src="' + image + '"/>');
                    }
                };
            })(f);
            reader.readAsDataURL(f);
        }
    }
});
*/
$('.content-to-show').each(function () {
    var current = null;
    current = $(this).find('.item:first');
    current.addClass('active');

    var currentTab = null;
    currentTab = $(this).find('#tab-navigation li:first a').addClass('tab-nav-active')

    $('#tab-navigation a').click(function (e) {
        e.preventDefault();
        var tab = $(this).data('content');
        $('.item').removeClass('active');
        $('#' + tab).addClass('active');
        $('#tab-navigation a').removeClass('tab-nav-active');
        $(this).addClass('tab-nav-active');
    });
})

$(document).ready(function () {
    $("#fire-menu2").hide();
    $("#fire-menu3").hide();
    $("#fire-menu4").hide();

    $('#nb-menu').change(function () {

        $("#fire-menu2").hide();
        $("#fire-menu3").hide();
        $("#fire-menu4").hide();

        if ($('#nb-menu option:selected').val() == 0) {
            return;
        }
        else {
            var nbmenu = $('#nb-menu option:selected').val();

            console.log(nbmenu);
            var i = 1;
            while (i <= nbmenu) {
                $("#fire-menu" + i).show();
                i++;
            }
        }
    });

	$('input[type="file"]').change(function(e){
		var fileName = e.target.files[0].name;
		var fileSize = (this.files[0].size / 1024 / 1024 ),
			files = e.target.files,
			elem = $(e.target),
			errorMessage = 'Fichier trop volumineux ou non pris en charge';

		if (typeof FileReader == "undefined" || fileSize > 0.5){
			elem.parents().children('p').text(errorMessage).addClass('error');
		}
		else {
			$(this).next('label').children('span').text(fileName);
			$(this).parents().children('i').css("display","inline-block");
			elem.parents().children('p').text("Image au format JPG, PNG ou GIF").removeClass('error');
		}
	});


    $(".remove-file").click(function (e) {
        $(this).parents().children('input[type="file"]').val("").clone(true);
        $(this).parents().children('label').children('.file-name').text("Choisir un fichier");
        $(this).hide();
    });

    tinymce.init({
        selector: 'textarea',
        height: 300,
        width: '100%',
        menubar: false,
        plugins: [
            'advlist autolink lists link image charmap anchor textcolor paste',
            'searchreplace visualblocks code fullscreen'
        ],
        paste_as_text: true,
        toolbar: ' formatselect | bold italic link | code ',
        content_css: [
            '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
            '//www.tinymce.com/css/codepen.min.css']
    
    });

});









