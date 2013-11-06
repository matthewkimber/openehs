/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.form.wizard-3.0.4.js" />
/// <reference path="../jquery-ui.js" />
/// <reference path="../jquery.form.js" />


$(function () {
    $("#registrationForm").formwizard({
        formPluginEnabled: false,
        validationEnabled: true,
        focusFirstInput: true,
        formOptions: {
            success: onSuccess,
            beforeSubmit: function (data) { },
            dataType: 'json',
            resetForm: true
        }
    });

    function onSuccess(data) {
        $("#registrationForm").hide("fast");
        window.location = "/Dashboard";
    }

    $("#lastNameTextbox").blur(function () {
        if ($("#firstNameTextbox").val() != "") {
            CheckForUsernameAvailability($("#firstNameTextbox").val().substring(0, 1) + $("#lastNameTextbox").val());
        }
    });

    function CheckForUsernameAvailability(username) {
        var paramData = { "username": username };

        $.post("/Account/CheckForUsernameAvailability",
                paramData,
                function (data) {
                    $("#Username").val(data.requestedUsername);
                    $("#generatedUsername").html(data.requestedUsername);
                },
                "json"
        );
    }

    $("#Country").val("Ghana");
    $("#Region").val("Greater Accra");
});
