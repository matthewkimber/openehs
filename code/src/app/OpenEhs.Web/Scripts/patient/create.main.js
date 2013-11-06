/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {
    $("input[type=button]").button();

    // Turns off autocomplete for all text inputs
    $("input[type=text]").attr("autocomplete", "off");

    $("#DateOfBirth").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true,
        yearRange: "1900:" + new Date().getFullYear(),
        

    }).focus(function () {
        $("#DateOfBirth").datepicker("show");
    });

    $("#InsuranceExpiration").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true,
        yearRange: "1900:" + new Date().getFullYear()

    }).focus(function () {
        $("#InsuranceExpiration").datepicker("show");
    });

    $("#newPatientContainer form").formwizard({
        formPluginEnabled: false,
        historyEnabled: true,
        validationEnabled: true,
        focusFirstInput: true,
        disableUIStyles: true
    });

    $("#Age").blur(function () {
        var date = new Date();
        var newYear = date.getFullYear() - $("#Age").val();
        date.setFullYear(newYear);
        $('#DateOfBirth').datepicker("setDate", date);
    });

    $("#copy").click(function () {
        $("#EcStreet1").val($("#Street1").val());
        $("#EcCity").val($("#City").val());
        $("#EcRegion").val($("#Region").val());
        $("#EcCountry").val($("#Country").val());
    });

    $("#Country").val("Ghana");
    $("#EcCountry").val("Ghana");
    $("#Region").val("Greater Accra");
});