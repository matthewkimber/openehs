/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {


    $(".surgeryLink").click(function () {
        $(".surgeryInfo").slideToggle("slow", function () { });
    });

    $(".noteLink").click(function () {
        $(".noteInfo").slideToggle("slow", function () { });
    });

    $(".submitSelectButton").click(function () {
        //$(".staffInfo").slideToggle("slow", function () { });
        $.ajax({
            type: "POST",
            url: "/Patient/SearchVisitList",
            data: {
                patientID: $("#patientId").val(),
                checkInID: $("select[name='visitPick'] option:selected").val()
            },
            success: function (response) {

                var checkin = response[0];
                var htmlOutput = "<div id=\"replaceDIV\"><ul class=\"visitSummaryList\"><li><p class=\"visitSummaryHeader\"><u>Encounter</u></p><b>Visit Date: </b>" + checkin.date +
                    "<br /><b>Diagnosis: </b>" + checkin.Diagnosis +
                    "<br /><b>Attending Staff: </b>" + checkin.lastName + ", " + checkin.firstName +
                    "<br /><b>Recorded Vitals: </b><br />" +
                    "<table class=\"detailsTable\"><thead><tr><th>Time</th><th>Type</th><th>Height(cm)</th><th>Weight(kg)</th><th>Temp(&deg;C)</th><th>HR(bpm)</th><th>BP(mmHg)</th><th>RR(Hz)</th></tr></thead><tbody>";

                for (var x = 0; x < checkin.Vitals.length; x++) {

                    var vital = checkin.Vitals[x];

                    htmlOutput += "<tr><td>" + vital.Time + "</td><td>" + vital.type + "</td><td>" + vital.Height + "</td><td>" + vital.Weight + "</td><td>" + vital.Temperature + "</td><td>" + vital.HeartRate + "</td><td>" + vital.BpDiastolic + "/" + vital.BpSystolic + "</td><td>" + vital.RespiratoryRate + "</td></tr>";
                }

                htmlOutput += "</tbody></table>";

                htmlOutput += "<div id=\"pmhNoteLink\"><b>Notes...</b></div><div id=\"pmhNoteInfo\">";

                htmlOutput += "<b>Encounter Note:</b><br />";


                for (var r = 0; r < checkin.Note.length; r++) {

                    var note = checkin.Note[r];

                    htmlOutput += "<b>Note Author: </b>" + note.FirstName + ", " + note.LastName + "<br />";

                    htmlOutput += "" + note.Body + "<br />";
                }


                htmlOutput += "</div>";

                htmlOutput += "<div id=\"pmhChartLink\"><b>Charting...</b></div><div id=\"pmhChartInfo\">";

                htmlOutput += "Feed Chart:";

                htmlOutput += "<table class=\"detailsTable\"><thead><tr><th>Date and Time</th><th>Feed Type</th>" +
                            "<th>Amount Offered</th><th>Amount Taken</th><th>Vomit</th><th>Urine</th><th>Stool</th><th>Comments</th></tr></thead>";

                for (var w = 0; w < checkin.FeedChart.length; w++) {

                    var feed = checkin.FeedChart[w];

                    htmlOutput += "<tr><td>" + feed.Time + "</td><td>" + feed.Type + "</td><td>" + feed.AmountOffered + "</td><td>" + feed.AmountTaken + "</td><td>" + feed.Vomit + "</td><td>" + feed.Urine + "</td><td>" + feed.Stool + "</td><td>" + feed.Comments + "</td></tr>";

                }

                htmlOutput += "</table>Output Chart:";

                htmlOutput += "<table class=\"detailsTable\" id=\"centerForOutputChart\" border=\"1\"><thead><tr><th></th><th colspan=\"2\">N.G. Suction</th>" +
                              "<th>Urine</th><th colspan=\"2\">Stool</th></tr><tr><th>Date and Time</th><th>Amount</th><th>Colour</th><th>Amount</th><th>Amount</th><th>Colour</th></tr></thead><tbody>";

                for (var z = 0; z < checkin.OutputChart.length; z++) {

                    var out = checkin.OutputChart[z];

                    htmlOutput += "<tr><td>" + out.Time + "</td><td>" + out.NGSuctionAmount + "</td><td>" + out.NGSuctionColor + "</td><td>" + out.UrineAmount + "</td><td>" + out.StoolAmount + "</td><td>" + out.StoolColor + "</td></tr>";

                }

                htmlOutput += "</tbody></table>Intake Chart:";

                htmlOutput += "<table class=\"detailsTable\"><thead><tr><th>Date and Time</th><th>Kind Of Fluid</th><th>Amount</th></tr></thead><tbody>";

                for (var b = 0; b < checkin.IntakeChart.length; b++) {

                    var intake = checkin.IntakeChart[b];

                    htmlOutput += "<tr><td>" + intake.Time + "</td><td>" + intake.KindOfFluid + "</td><td>" + intake.Amount + "</td></tr>";

                }

                htmlOutput += "</tbody></table>";

                htmlOutput += "</div>";

                htmlOutput += "<div id=\"pmhSurgeryLink\"><b>Surgery...</b></div><div id=\"pmhSurgeryInfo\">";

                for (var t = 0; t < checkin.Surgery.length; t++) {

                    var surgery = checkin.Surgery[t];

                    htmlOutput += "<b>Start Time:</b>" + " " + surgery.StartTime + "<br />";

                    htmlOutput += "<b>End Time:</b>" + " " + surgery.EndTime + "<br />";

                    htmlOutput += "<b>Case Type:</b>" + " " + surgery.CaseType + "<br />";
                }

                htmlOutput += "<b>Surgery Staff:</b><br />";

                htmlOutput += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Surgeon:</b><br />";

                htmlOutput += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Surgeon Assistant:</b><br />";

                htmlOutput += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Anaesthetist:</b><br />";

                htmlOutput += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Anaesthetist Assistant:</b><br />";

                htmlOutput += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Nurse:</b><br />";

                htmlOutput += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Consultant:</b><br />";

                htmlOutput += "<b>Notes:</b><br />";

                for (var p = 0; p < checkin.SurgeryNote.length; p++) {

                    var surgeryNote = checkin.SurgeryNote[p];

                    htmlOutput += "" + surgeryNote.Body + "<br />";
                }


                htmlOutput += "</div>";

                htmlOutput += "</li></ul></div>";

                $("#replaceDIV").replaceWith(htmlOutput);
                //Need to add surgery right under encounter stuff... Also ask matt if I can call another POST right here!
            },
            dataType: "json"
        });

    });


    $("#pmhNoteLink").live('click', function () {
        $("#pmhNoteInfo").slideToggle("slow", function () { });
    });

    $("#pmhChartLink").live('click', function () {
        $("#pmhChartInfo").slideToggle("slow", function () { });
    });

    $("#pmhSurgeryLink").live('click', function () {
        $("#pmhSurgeryInfo").slideToggle("slow", function () { });
    });


});