//////
var openBookPath = '../../../DynamicData/Content/Images/ui/x_book_open.png';
var closedBookPath = '../../../DynamicData/Content/Images/ui/x_book_closed.png';
//////

// toggles a panel on / off based on a trigger's state
function SetToggleOn(controlToToggle, triggerControl, cookie, image) {
    ///////
    var openCloseBookImage = $(image);
    var currentToggleControl = $(controlToToggle);
    ///////

    $(controlToToggle).hide();

    if (navigator.cookieEnabled) { // check if cookies are enabled

        if ($.cookie(cookie) == 'true') {
            $(controlToToggle).show();
        } else {
            $(controlToToggle).hide();
        }
    }

    else {
        $(controlToToggle).show();
    }

    //////
    ChangeImageUrlOnStatus(currentToggleControl, openCloseBookImage);
    //////

    $(triggerControl).click(function () {

        $(controlToToggle).slideToggle(0, function () {

            try {
                if (navigator.cookieEnabled) {
                    $.cookie(cookie, $(controlToToggle).is(':visible').toString());
                }

                //////
                ChangeImageUrlOnStatus(currentToggleControl, openCloseBookImage);
                //////
            }

            catch (e) {
                alert("error: " + e.Message.toString());
            }
        });

        return false;
    });
    // -->
}

function SetImageUrl(image, imageUrl) {
    var currentImage = $(image);
    currentImage.attr('src', imageUrl);
}

function ChangeImageUrlOnStatus(currentControl, currentImage) {
    if ($(currentControl).is(':hidden')) {
        SetImageUrl(currentImage, closedBookPath);
    }

    else if ($(currentControl).is(':visible')) {
        SetImageUrl(currentImage, openBookPath);
    }
}