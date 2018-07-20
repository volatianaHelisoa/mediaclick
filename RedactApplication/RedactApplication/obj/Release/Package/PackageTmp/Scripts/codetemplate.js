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











