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