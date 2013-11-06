/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {

    // ------------------------------------------------- //
    //  Setup Charting Tab                                //
    // ------------------------------------------------- //

    //$("#feedAddButton").button().click(function () {
    //alert("Blah");
    //});

    $("#intakeAddButton").button().click(function () {
        $("#addIntakeDialog").dialog("open");
    });

    $("#suctionAddButton").button().click(function () {
        $("#addOutputDialog").dialog("open");
    });

    $("#addIntakeDialog").dialog({
        autoOpen: false,
        height: 220,
        width: 450,
        modal: true,
        buttons: {
            "Add Intake": function () {
                $.post("/Patient/AddIntake", {
                    patientID: $("#patientId").val(),
                    kindoffluid: $("#model_kindoffeed").val(),
                    amount: $("#model_amount").val()
                }, function (result) {
                    if (result.error == "false") {
                        $("#addIntakeDialog").dialog("close");

                        var table = document.getElementById("intakeTable");
                        var tr = document.createElement("tr");

                        var elements = new Array();

                        for (var i = 0; i < 3; i++)
                            elements[i] = document.createElement("td");

                        elements[0].appendChild(document.createTextNode(result.Date));
                        elements[1].appendChild(document.createTextNode(result.KindOfFluid));
                        elements[2].appendChild(document.createTextNode(result.Amount));

                        for (var i = 0; i < 3; i++)
                            tr.appendChild(elements[i]);

                        table.appendChild(tr);

                        //Reset form
                        $("#model_kindoffeed").val("");
                        $("#model_amount").val("");

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

    $("#addFeedDialog").dialog({
        autoOpen: false,
        height: 370,
        width: 450,
        modal: true,
        buttons: {
            "Add Feed": function () {
                $.post("/Patient/AddFeed", {
                    patientID: $("#patientId").val(),
                    feedType: $("#model_feedtype").val(),
                    amountOffered: $("#model_amountoffered").val(),
                    amountTaken: $("#model_amounttaken").val(),
                    vomit: $("#model_vomit").val(),
                    urine: $("#model_urine").val(),
                    stool: $("#model_stool").val(),
                    comments: $("#model_comments").val()
                }, function (result) {
                    if (result.error == "false") {
                        $("#addFeedDialog").dialog("close");

                        var table = document.getElementById("detailsTables");
                        var tr = document.createElement("tr");

                        var elements = new Array();

                        for (var i = 0; i < 8; i++)
                            elements[i] = document.createElement("td");

                        elements[0].appendChild(document.createTextNode(result.Date));
                        elements[1].appendChild(document.createTextNode(result.FeedType));
                        elements[2].appendChild(document.createTextNode(result.AmountOffered));
                        elements[3].appendChild(document.createTextNode(result.AmountTaken));
                        elements[4].appendChild(document.createTextNode(result.Vomit));
                        elements[5].appendChild(document.createTextNode(result.Urine));
                        elements[6].appendChild(document.createTextNode(result.Stool));
                        elements[7].appendChild(document.createTextNode(result.Comments));

                        for (var i = 0; i < 8; i++)
                            tr.appendChild(elements[i]);

                        table.appendChild(tr);

                        //Reset form
                        $("#model_feedtype").val("");
                        $("#model_amountoffered").val("");
                        $("#model_amounttaken").val("");
                        $("#model_vomit").val("");
                        $("#model_urine").val("");
                        $("#model_stool").val("");
                        $("#model_comments").val("");
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


    $("#addOutputDialog").dialog({
        autoOpen: false,
        height: 300,
        width: 450,
        modal: true,
        buttons: {
            "Add Output": function () {
                $.post("/Patient/AddOutput", {
                    patientID: $("#patientId").val(),
                    ngAmount: $("#model_ngAmount").val(),
                    ngColor: $("#model_ngColour").val(),
                    urineAmount: $("#model_urineAmount").val(),
                    stoolAmount: $("#model_stoolAmount").val(),
                    stoolColor: $("#model_stoolColour").val()
                }, function (result) {
                    if (result.error == "false") {
                        $("#addOutputDialog").dialog("close");

                        var table = document.getElementById("outputSuctionTable");
                        var tr = document.createElement("tr");

                        var elements = new Array();

                        for (var i = 0; i < 6; i++)
                            elements[i] = document.createElement("td");

                        elements[0].appendChild(document.createTextNode(result.Date));
                        elements[1].appendChild(document.createTextNode(result.NGSuctionAmount));
                        elements[2].appendChild(document.createTextNode(result.NGSuctionColor));
                        elements[3].appendChild(document.createTextNode(result.UrineAmount));
                        elements[4].appendChild(document.createTextNode(result.StoolAmount));
                        elements[5].appendChild(document.createTextNode(result.StoolColor));

                        for (var i = 0; i < 6; i++)
                            tr.appendChild(elements[i]);

                        table.appendChild(tr);

                        //Reset form
                        $("#model_ngAmount").val("");
                        $("#model_ngColour").val("");
                        $("#model_urineAmount").val("");
                        $("#model_stoolAmount").val("");
                        $("#model_stoolColour").val("");
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


    $("#addFeedForm").submit(function () {
        return false;
    });

    $("#feedAddButton").button().click(function () {
        $("#addFeedDialog").dialog("open");
    });

    $("#addChartingForm .datepicker").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true
    }).focus(function () {
        $(this).datepicker("show");
    });

});