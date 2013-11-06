/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {
    // ------------------------------------------------- //
    //  Setup Chronic Diseases Tab                       //
    // ------------------------------------------------- //
    $("#addProblemDialog").dialog({
        autoOpen: false,
        height: 170,
        width: 375,
        modal: true,
        buttons: {
            "Create New Disease": function () {
                $("#createDiseaseDialog").dialog("open");
            },
            "Add Disease": function () {
                $.post("/Patient/AddDiseaseToPatient", {
                    patientId: $("#patientId").val(),
                    problemId: $("#addDiseaseName").val()
                }, function (returnData) {
                    if (returnData.error == "false") {
                        $("#addProblemDialog").dialog("close");

                        var newAllergy = '<li><div style="float: left;">' + returnData.Name + '</div></li>';
                        $("#ChronicDiseaseList").append(newAllergy);

                        $("#addDiseaseName").val("");
                        $("#addProblemDialog").dialog("close");

                    } else {
                        $("#addProblemDialog .error").html(returnData.status);
                    }
                }, "json");
            },
            Cancel: function () {
                $("#addDiseaseName").val("");
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

    $("#createDiseaseDialog").dialog({
        autoOpen: false,
        height: 170,
        width: 420,
        modal: true,
        buttons: {
            "Add Disease": function () {
                $.post("/Patient/CreateNewDisease", {
                    DiseaseName: $("#modal_DiseaseName").val()
                }, function (result) {

                    //Need this code for later!!!
                    $("#addDiseaseName").append('<option value="' + result.Id + '">' + result.Name + '</option>');

                    $("#modal_DiseaseName").val("");
                    $("#createDiseaseDialog").dialog("close");

                }, "json");
            },
            Cancel: function () {
                $("#modal_DiseaseName").val("");

                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

    $("#chronicDiseasesAddButton").button().click(function () {
        $("#addProblemDialog").dialog("open")
    });



});