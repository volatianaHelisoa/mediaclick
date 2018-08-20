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

    tinymce.PluginManager.add('stylebuttons', function (editor, url) {
        ['pre', 'p', 'code', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6'].forEach(function (name) {
            editor.addButton("style-" + name, {
                tooltip: "Toggle " + name,
                text: name.toUpperCase(),
                onClick: function () { editor.execCommand('mceToggleFormat', false, name); },
                onPostRender: function () {
                    var self = this, setup = function () {
                        editor.formatter.formatChanged(name, function (state) {
                            self.active(state);
                        });
                    };
                    editor.formatter ? setup() : editor.on('init', setup);
                }
            })
        });
    });

    tinymce.init({
        selector: "textarea",
        width: '100%',
        height: 270,
        plugins: "stylebuttons",
        statusbar: false,
        menubar: false,
        toolbar: "bold italic | link | undo redo | style-p style-h1 style-h2 style-h3 style-pre style-code",
        content_css: [
            '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
            '//www.tinymce.com/css/codepen.min.css']
     
    });



});









