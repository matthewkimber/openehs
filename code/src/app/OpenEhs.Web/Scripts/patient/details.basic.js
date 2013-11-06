/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {
    // ------------------------------------------------- //
    //  Setup Basic Tab                                  //
    // ------------------------------------------------- //
    $("#DateOfBirth").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true,
        yearRange: "1900:" + new Date().getFullYear()
    });

    $("#InsuranceExpiration").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true,
        yearRange: "1900:" + new Date().getFullYear()
    });

    /*
    $("#CreationDate").datepicker({
    showOn: "button",
    //buttonImage: "/Content/themes/base/images/calendar.png",
    buttonImageOnly: true,
    changeYear: true,
    //yearRange: "1900:" + new Date().getFullYear()
    });
    */

    $("#EmergencyContactMoreInfoLink").click(function () {
        $("#EmergencyContactMoreInfo").slideToggle("slow", function () { });
    });

    $("#BasicMoreInfoLink").click(function () {
        $("#BasicMoreInfo").slideToggle("slow", function () { });
    });

    $("#AddressLink").click(function () {
        $("#BasicAddress").slideToggle("slow", function () { });
    });

    $("#checkOutButton").button();
    $("#checkOutButton").hide();

    $("#GenderRadio").buttonset();

    $("#editPatientInfoButton").button().click(function () {
        $("#FirstName").removeAttr("disabled");
        $("#InsuranceNumber").removeAttr("disabled");
        $("#InsuranceExpiration").removeAttr("disabled");
        $("#MiddleName").removeAttr("disabled");
        $("#LastName").removeAttr("disabled");
        $("#Tribe").removeAttr("disabled");
        $("#Education").removeAttr("disabled");
        $("#Race").removeAttr("disabled");
        $("#Occupation").removeAttr("disabled");
        $("#MaleGenderRadio").removeAttr("disabled");
        $("#FemaleGenderRadio").removeAttr("disabled");
        $("#PlaceOfBirth").removeAttr("disabled");
        $("#DateOfBirth").removeAttr("disabled");
        $("#PhoneNumber").removeAttr("disabled");
        $("#Address_Street1").removeAttr("disabled");
        $("#Address_Street2").removeAttr("disabled");
        $("#Address_City").removeAttr("disabled");
        $("#MaritalStatus").removeAttr("disabled");
        $("#Gender").removeAttr("disabled");
        $("#Address_Region").removeAttr("disabled");
        $("#Address_Country").removeAttr("disabled");
        $("#EmergencyContact_FirstName").removeAttr("disabled");
        $("#EmergencyContact_LastName").removeAttr("disabled");
        $("#EmergencyContact_PhoneNumber").removeAttr("disabled");
        $("#EmergencyContact_Address_Street1").removeAttr("disabled");
        $("#EmergencyContact_Address_Street2").removeAttr("disabled");
        $("#EmergencyContact_Address_City").removeAttr("disabled");
        $("#EmergencyContact_Address_Region").removeAttr("disabled");
        $("#EmergencyContact_Address_Country").removeAttr("disabled");
        $("#IsActive").removeAttr("disabled");
        $("#editPatientInfoButton").hide();
        $("#savePatientInfoButton").show();
    });

    $("#savePatientInfoButton").button().click(function () {
        $.ajax({
            type: "POST",
            url: "/Patient/EditPatient",
            data: {
                PatientID: $("#patientId").val(),
                FirstName: $("#FirstName").val(),
                MiddleName: $("#MiddleName").val(),
                LastName: $("#LastName").val(),
                PlaceOfBirth: $("#PlaceOfBirth").val(),
                DateOfBirth: $("#DateOfBirth").val(),
                PhoneNumber: $("#PhoneNumber").val(),
                Address_Street1: $("#Address_Street1").val(),
                Address_Street2: $("#Address_Street2").val(),
                Address_City: $("#Address_City").val(),
                Address_Region: $("#Address_Region").val(),
                Address_Country: $("#Address_Country").val(),
                EC_FirstName: $("#EmergencyContact_FirstName").val(),
                EC_LastName: $("#EmergencyContact_LastName").val(),
                EC_PhoneNumber: $("#EmergencyContact_PhoneNumber").val(),
                EC_Address_Street1: $("#EmergencyContact_Address_Street1").val(),
                EC_Address_Street2: $("#EmergencyContact_Address_Street2").val(),
                EC_Address_City: $("#EmergencyContact_Address_City").val(),
                EC_Address_Region: $("#EmergencyContact_Address_Region").val(),
                EC_Address_Country: $("#EmergencyContact_Address_Country").val(),
                Note: $("#patientNoteTextBox").val(),
                IsActive: $("#IsActive").val(),
                Tribe: $("#Tribe").val(),
                MaritalStatus: $("#MaritalStatus").val(),
                Gender: $("#Gender").val(),
                Race: $("#Race").val(),
                Education: $("#Education").val(),
                Occupation: $("#Occupation").val(),
                InsuranceNumber: $("#InsuranceNumber").val(),
                InsuranceExpiration: $("#InsuranceExpiration").val()
            },
            success: function (response) {
                if (response.MiddleName == "") {
                    $("#patientInfo").html('<h2>' + response.LastName + ', ' + response.FirstName + '</h2><h2 style="margin-left: 20px; font-weight: normal;" id="patientAge">Age: ' + response.Age + '</h2>');
                }
                else {
                    $("#patientInfo").html("<h2>" + response.LastName + ", " + response.FirstName + " " + response.MiddleName + '</h2><h2 style="margin-left: 20px; font-weight: normal;" id="patientAge">Age: ' + response.Age + '</h2>');
                }
                $("#FirstName").attr("disabled", "disabled");
                $("#MiddleName").attr("disabled", "disabled");
                $("#LastName").attr("disabled", "disabled");
                $("#MaleGenderRadio").attr("disabled", "disabled");
                $("#FemaleGenderRadio").attr("disabled", "disabled");
                $("#PlaceOfBirth").attr("disabled", "disabled");
                $("#DateOfBirth").attr("disabled", "disabled");
                $("#PhoneNumber").attr("disabled", "disabled");
                $("#Gender").attr("disabled", "disabled");
                $("#Address_Street1").attr("disabled", "disabled");
                $("#Address_Street2").attr("disabled", "disabled");
                $("#Address_City").attr("disabled", "disabled");
                $("#Address_Region").attr("disabled", "disabled");
                $("#Address_Country").attr("disabled", "disabled");
                $("#EmergencyContact_FirstName").attr("disabled", "disabled");
                $("#EmergencyContact_LastName").attr("disabled", "disabled");
                $("#EmergencyContact_PhoneNumber").attr("disabled", "disabled");
                $("#EmergencyContact_Address_Street1").attr("disabled", "disabled");
                $("#EmergencyContact_Address_Street2").attr("disabled", "disabled");
                $("#EmergencyContact_Address_City").attr("disabled", "disabled");
                $("#EmergencyContact_Address_Region").attr("disabled", "disabled");
                $("#EmergencyContact_Address_Country").attr("disabled", "disabled");
                $("#IsActive").attr("disabled", "disabled");
                $("#Tribe").attr("disabled", "disabled");
                $("#Race").attr("disabled", "disabled");
                $("#MaritalStatus").attr("disabled", "disabled");
                $("#Education").attr("disabled", "disabled");
                $("#Occupation").attr("disabled", "disabled");
                $("#InsuranceNumber").attr("disabled", "disabled");
                $("#InsuranceExpiration").attr("disabled", "disabled");
                $("#savePatientInfoButton").hide();
                $("#editPatientInfoButton").show();
            },
            dataType: "json"
        });
    });
    $("#savePatientInfoButton").hide();

    $("#modal_TimeOfDeath").timepicker({});

    $("#newCheckInButton").button().click(function () {
        $("#newCheckinModal").dialog("open");
    });

    $("#checkOutButton").button().click(function () {
        $("#checkOutModal").dialog("open");
    });

    // ------------------------------------------------- //
    //  Configure CKEditor on Patient Note               //
    // ------------------------------------------------- //
    var ckConfig = {
        toolbar: [
            ["Bold", "Italic", "-"],
            ["UIColor"]
        ],
        extraPlugins: "autogrow",
        autoGrow_maxHeight: 800
    };

    $("#patientNoteTextBox").ckeditor(ckConfig);

});