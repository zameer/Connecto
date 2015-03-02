$(function () {});
$.fn.syncMenu = function (settings, breadcrump) {
    $(this).attr('class', bindCss('ao'));
    if (breadcrump != undefined) setBreadcrump(breadcrump);
    $.each(settings, function() {
        $('#menu-' + this.name).attr('class', bindCss(this.option));
    });
    function bindCss(opt) { return opt == 'a' ? 'active' : (opt == 'o' ? 'open' : (opt == 'ao' ? 'active open' : '')); };
    function setBreadcrump(bc) {
        $("#breadcrumbs ul").append('<li><a href="' + bc.link + '">' + bc.title + '</a></li>');
    }
};
function showMessage(data) {
    var message = null;
    var isSuccess = false;
    if (data.Status == "Success") {
        showNotification({ message: data.Message, type: 'success', autoClose: false, duration: 5 });
        isSuccess = true;
    }
    else if (data.Status == "Fail") {
        showNotification({ message: data.Message, type: 'error', autoClose: false, duration: 5 });
        isSuccess = false;
    }
    else if (data.Status == "Failure") {
            $('input,select,textarea').removeClass('input-error');
            message = "<ul>";
            $.each(data.Exceptions, function (index) {
                $("#" + data.Exceptions[index].Message).addClass('input-validation-error');

                message += "<li> " + data.Exceptions[index].Message + " </li>";
            });
            message += "</ul>";
            showNotification({ message: message, type: 'error', autoClose: false, duration: 5 });
    }
    return isSuccess;
}