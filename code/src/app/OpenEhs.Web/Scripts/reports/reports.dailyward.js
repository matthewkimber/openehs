$(document).ready(function () {
    $("#Date").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true,
        yearRange: "1900:" + new Date().getFullYear()

    }).focus(function () {
        $("#Date").datepicker("show");
    });
});