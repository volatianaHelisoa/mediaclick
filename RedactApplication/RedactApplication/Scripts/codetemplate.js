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
var showThumbPhotos = (function (e) {
    if (typeof FileReader == "undefined") return true;
    var elem = $(this);
    var files = e.target.files;
    for (var i = 0, f; f = files[i]; i++) {
        if (f.type.match('image.*')) {
            var reader = new FileReader();
            var previewDiv = $('.thumbnail-photo', elem.parent());
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
$('input[type=file]#logoUrl').bind("change", showThumb);
$('input[type=file]#menu1_paragraphe1_photoUrl').bind("change", showThumbPhotos);
$('input[type=file]#menu1_paragraphe2_photoUrl').bind("change", showThumbPhotos);
$('input[type=file]#photoALaUneUrl').bind("change", showThumbPhotos);
$('input[type=file]#menu2_paragraphe1_photoUrl').bind("change", showThumbPhotos);
$('input[type=file]#menu2_paragraphe2_photoUrl').bind("change", showThumbPhotos);
$('input[type=file]#menu3_paragraphe1_photoUrl').bind("change", showThumbPhotos);
$('input[type=file]#menu3_paragraphe2_photoUrl').bind("change", showThumbPhotos);
$('input[type=file]#menu4_paragraphe1_photoUrl').bind("change", showThumbPhotos);
$('input[type=file]#menu4_paragraphe2_photoUrl').bind("change", showThumbPhotos);

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


    tinymce.init({
        selector: 'textarea',
        height: 300,
        width: '100%',
        menubar: false,

        plugins: [
            'advlist autolink lists link image charmap anchor textcolor',
            'searchreplace visualblocks code fullscreen',
            'insertdatetime media table contextmenu paste code  wordcount'
        ],
        toolbar: 'insert | undo redo |  formatselect | bold italic backcolor  | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | removeformat | help',
        content_css: [
            '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
            '//www.tinymce.com/css/codepen.min.css'],
        setup: function (ed) {
            ed.on('keyup', function (e) {
                tinyMceChange(ed);
                var contenu = $.trim($(this).text());

                var contenu_res = ed.getContent().replace(/(style="([^"]*)")/g, "");
                var regex = /(&nbsp;|<([^>]+)>)/ig;
                contenu_res = contenu_res.replace(regex, "");
                var words = ed.plugins.wordcount.getCount();

                var charLength = words.length;
                var charLimit = $("#limit").val();
                // Displays count
                $(".left_char").html(charLength + " sur " + charLimit + " caractère(s) utilisé(s)");
                // Alert when max is reached
                /* if (charLength > charLimit) {
                    $(".left_char").html("<strong>Le nombre de caractères maximum de " +
                        charLimit +
                        " atteint.</strong>");
                } */
            });
            ed.on('change', function (e) {
                tinyMceChange(ed);
            });
        }
    });

});









