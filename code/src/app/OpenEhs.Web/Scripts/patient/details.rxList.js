/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {

    // ------------------------------------------------- //
    //  Setup Rx List Tab                                //
    // ------------------------------------------------- //
    $(".medicationAddButton").button().click(function () {
        $("#newMedicationDialog").dialog("open");
    });

    $(".medicationPrintButton").button().click(function () { });

    $("#rxInfoLink").click(function () {
        $("#rxMoreInfo").slideToggle("slow", function () { });
    });

    $("#addRxForm").validate({
        errorLabelContainer: $("#newMedicationDialog .modalErrorContainer"),
        wrapper: "li",
        messages: {
            name: "A medication name is required",
            //instructions: "Instructions are required",
            expdate: "An expiration date is required"
        }
    });

    $("#newMedicationDialog").dialog({
        autoOpen: false,
        height: 375,
        width: 450,
        modal: true,
        buttons: {
            "Create New Medication": function () {
                $("#createMedicationDialog").dialog("open");
            },
            "Save Medication": function () {
                if ($("#addRxForm").valid()) {
                    $.post("/Patient/AddMedicationToPatient", {
                        patientID: $("#patientId").val(),
                        name: $("#medicationDropDownList").val(),
                        dose: $("#modal_medicationDosage").val(),
                        frequency: $("#modal_medicationFrequency").val(),
                        expDate: $("#RxExpDatePicker").val(),
                        route: $("#medicationROA").val(),
                        instructions: $("#modal_medicationInstructions").val()
                    }, function (response) {
                        $("#addRxForm").dialog("close");

                        var newMedication = '<li><div><b>Name: </b>' + response.name + '</div>' +
                                            '<div><b>Dosage: </b>' + response.dose + '</div>' +
                                            '<div><b>Frequency: </b>' + response.frequency + '</div>' +
                                            '<div><b>Route: </b>' + response.route + '</div>' +
                                            '<div><b>Instructions: </b>' + response.instructions + '</div>' +
                                            '<div><b>Start Date: </b>' + response.startDate + '</div>' + 
                                            '<div><b>Exp Date: </b>' + response.expDate + '</div></li>';
                        $("#MedicationListTwo").append(newMedication);

                        $("#medicationDropDownList").val("");
                        $("#modal_medicationDosage").val("");
                        $("#modal_medicationFrequency").val("");
                        $("#medicationROA").val("");
                        $("#modal_medicationInstructions").val("");
                        $("#RxExpDatePicker").val("");
                    });
                    $(this).dialog("close");
                }
            },
            Cancel: function () {
                $("#patientId").val("");
                $("#medicationDropDownList").val("");
                $("#modal_medicationInstructions").val("");
                $("#RxExpDatePicker").val("");

                $(this).dialog("close");
            }
        },
        close: function () {
            $("#newMedicationDialog .modalErrorContainer").hide();
            $('#addRxForm').each(function () {
                this.reset();
            });
        }
    });

    $("#createMedicationDialog").dialog({
        autoOpen: false,
        height: 200,
        width: 420,
        modal: true,
        buttons: {
            "Add Medication": function () {
                $.post("/Patient/CreateNewMedication", {
                    MedicationName: $("#modal_medicationName").val(),
                    MedicationDescription: $("#modal_medicationDescription").val()
                }, function (result) {

                    $("#medicationDropDownList").append('<option value="' + result.Id + '">' + result.Name + '</option>');

                    $("#modal_medicationName").val("");
                    $("#modal_medicationDescription").val("");

                    $("#createMedicationDialog").dialog("close");

                }, "json");
            },
            Cancel: function () {
                $("#modal_medicationName").val("");
                $("#modal_medicationDescription").val("");


                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

    $("#addRxForm .datepicker").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true
    }).focus(function () {
        $(this).datepicker("show");
    });

});