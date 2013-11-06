/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {

    // ------------------------------------------------- //
    //  Setup Rx List Tab                                //
    // ------------------------------------------------- //
    $("#immunizationAddButton").button().click(function () {
        $("#newImmunizationDialog").dialog("open");
    });

    $("#immunizationInfoLink").click(function () {
        $("#immunizationMoreInfo").slideToggle("slow", function () { });
    });

    $("#addImmunizationForm").validate({
        errorLabelContainer: $("#newImmunizationDialog .modalErrorContainer"),
        wrapper: "li",
        messages: {
            name: "A name for the immunization is required",
            dateAdministered: "You must provide the date the immunization was administered"
        }
    });

    $("#newImmunizationDialog").dialog({
        autoOpen: false,
        height: 210,
        width: 400,
        modal: true,
        buttons: {
            "Create New Immunization": function () {
                $("#createImmunizationDialog").dialog("open");
            },
            "Save Immunization": function () {
                if ($("#addImmunizationForm").valid()) {
                    $.post("/Patient/AddImmunizationToPatient", {
                        patientID: $("#patientId").val(),
                        vaccineType: $("#immunizationSelect").val(),
                        dateAdministered: $("#modal_immAdministered").val()
                    }, function (response) {
                        if (response.error == "false") {

                            $("#newImmunizationDialog").dialog("close");
                            var newImmunization = '<li  style="display:none;" id="immun_' + response.id + '"><div><b>Vaccine Type: </b>' + response.immunization + '</div><div><b>Date Administered: </b>' + response.dateAdmin + '</div></li>';

                            $("#immunizationListOne").append(newImmunization);
                            $("#immun_" + response.id).fadeIn("slow", function () { });
                        } else {
                            alert("Error adding immunization");
                        }
                    });
                    $(this).dialog("close");
                }
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $("#newImmunizationDialog .modalErrorContainer").hide();
            $('#addImmunizationForm').each(function () {
                this.reset();
            });
        }
    });

    $("#createImmunizationDialog").dialog({
        autoOpen: false,
        height: 170,
        width: 420,
        modal: true,
        buttons: {
            "Add Immunization": function () {
                $.post("/Patient/AddNewImmunization", {
                    VaccieType: $("#modal_immName").val()
                }, function (result) {


                    //$("#prewrittenNoteSelect").append('<option value="' + result. + '" >' + result. + '</option>');
                    $("#immunizationSelect").append('<option value="' + result.Id + '">' + result.VaccineType + '</option>');

                    VaccieType: $("#modal_immName").val("");
                    $("#createImmunizationDialog").dialog("close");

                }, "json");
            },
            Cancel: function () {
                VaccieType: $("#modal_immName").val("");


                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

    $("#addImmunizationForm .datepicker").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true
    }).focus(function () {
        $(this).datepicker("show");
    });

});