$(function () {});
$.fn.syncMenu = function (settings) {
    $(this).attr('class', bindCss('ao'));
    $.each(settings, function () { $('#menu-' + this.name).attr('class', bindCss(this.option)); });
    function bindCss(opt) { return opt == 'a' ? 'active' : (opt == 'o' ? 'open' : (opt == 'ao' ? 'active open' : ''));};
};