/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {
    // ------------------------------------------------- //
    //  Setup Allergy Tab                                //
    // ------------------------------------------------- //

    $("#addAllergyDialog").dialog({
        autoOpen: false,
        height: 170,
        width: 375,
        modal: true,
        buttons: {
            "Create New Allergy": function () {
                $("#createAllergyDialog").dialog("open");
            },
            "Add Allergy": function () {
                $.post("/Patient/AddAllergyToPatient", {
                    patientId: $("#patientId").val(),
                    allergyId: $("#addAllergyName").val()
                }, function (returnData) {
                    if (returnData.error == "false") {
                        $("#addAllergyDialog").dialog("close");

                        var newAllergy = '<li><div style="float: left;">' + returnData.Name + '</div><div style="float: right;"></div></li>';
                        $("#allergyList").append(newAllergy);
                    } else {
                        $("#addAllergyDialog .error").html(returnData.status);
                    }
                }, "json");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

    $("#createAllergyDialog").dialog({
        autoOpen: false,
        height: 170,
        width: 420,
        modal: true,
        buttons: {
            "Add Allergy": function () {
                $.post("/Patient/CreateNewAllergy", {
                    AllergyName: $("#modal_allergyName").val()
                }, function (result) {

                    //Need this code for later!!!
                    $("#addAllergyName").append('<option value="' + result.Id + '">' + result.Name + '</option>');

                    $("#modal_allergyName").val("");
                    $("#createAllergyDialog").dialog("close");

                }, "json");
            },
            Cancel: function () {
                $("#modal_allergyName").val("");


                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

    $("#addAllergyForm").validate();

    $("#addAllergyForm").submit(function () {
        return false;
    });

    $("#allergyAddButton").button().click(function () {
        $("#addAllergyDialog").dialog("open")
    });

    $("#allergyRemove").button().click(function () {
        alert("remove this");
        $.post("/Patient/RemoveAllergy", {
            patientID: $("#patientId").val(),
            allergyID: $("#allergyId").val()
        }, function (returnData) {
            if (returnData.error == "false") {
               

            } else {
                $("#addAllergyDialog .error").html(returnData.status);
            }
        }, "json");
    });

});