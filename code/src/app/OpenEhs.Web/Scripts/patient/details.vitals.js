/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {
    // ------------------------------------------------- //
    //  Setup Vitals Tab                                 //
    // ------------------------------------------------- //

    function processVitalsReturn(result) {
        if (result.error == "false") {
            var table = document.getElementById("vitalsTable");
            var tr = document.createElement("tr");

            var elements = new Array();

            for (var i = 0; i < 8; i++)
                elements[i] = document.createElement("td");

            elements[0].appendChild(document.createTextNode(result.date));
            elements[1].appendChild(document.createTextNode(result.type));

            if (result.height != "0") elements[2].appendChild(document.createTextNode(result.height));
            else
                elements[2].appendChild(document.createTextNode("N/A"));

            if (result.weight != "0") elements[3].appendChild(document.createTextNode(result.weight));
            else
                elements[3].appendChild(document.createTextNode("N/A"));

            if (result.temperater != "0") elements[4].appendChild(document.createTextNode(result.temperature));
            else
                elements[4].appendChild(document.createTextNode("N/A"));

            if (result.heartRate != "0") elements[5].appendChild(document.createTextNode(result.heartRate));
            else
                elements[5].appendChild(document.createTextNode("N/A"));

            if (result.bpSystolic != "0") elements[6].appendChild(document.createTextNode(result.bpSystolic + "/" + result.bpDiastolic));
            else
                elements[6].appendChild(document.createTextNode("N/A"));

            if (result.respiratoryRate != "0") elements[7].appendChild(document.createTextNode(result.respiratoryRate));
            else
                elements[7].appendChild(document.createTextNode("N/A"));

            for (var i = 0; i < 8; i++)
                tr.appendChild(elements[i]);

            table.appendChild(tr);

            //Reset form
            $("#modal_vitalHeight").val("");
            $("#modal_vitalWeight").val("");
            $("#modal_vitalTemperature").val("");
            $("#modal_vitalHeartRate").val("");
            $("#modal_vitalBpSystolic").val("");
            $("#modal_vitalBpDiastolic").val("");
            $("#modal_vitalRespiratoryRate").val("");

        } else
            alert("Error");
    }

    $("#newVitalDialog").dialog({
        autoOpen: false,
        height: 425,
        width: 450,
        modal: true,
        buttons: {
            "Save Vital": function () {
                if ($("#addVitals").valid()) {
                    $.ajax({
                        type: "POST",
                        url: "/Patient/AddVital",
                        data: {
                            date: $("#modal_vitalDate").val(),
                            time: $("#modal_vitalTime").val(),
                            patientID: $("#patientId").val(),
                            height: $("#modal_vitalHeight").val(),
                            weight: $("#modal_vitalWeight").val(),
                            temperature: $("#modal_vitalTemperature").val(),
                            heartRate: $("#modal_vitalHeartRate").val(),
                            BpSystolic: $("#modal_vitalBpSystolic").val(),
                            BpDiastolic: $("#modal_vitalBpDiastolic").val(),
                            respiratoryRate: $("#modal_vitalRespiratoryRate").val(),
                            type: $('input:radio[name=modal_vitalsType]:checked').val()
                        },
                        success: function (response) {
                            $("#newVitalDialog").dialog("close");
                            processVitalsReturn(response);
                        },
                        dataType: "json"
                    });
                }
            },

            Cancel: function () {
                $(this).dialog("close");
            }
        },

        close: function () { }
    });
    $(".vitalAddButton").button().click(function () {
        $("#newVitalDialog").dialog("open");
    });


    $("#addVitals").validate({
        rules: {
            modal_vitalHeight: {
                number: true
            },
            modal_vitalWeight: {
                number: true
            },
            modal_vitalTemperature: {
                number: true
            },
            modal_vitalHeartRate: {
                number: true
            },
            modal_vitalBpSystolic: {
                number: true
            },
            modal_vitalBpDiastolic: {
                number: true
            },
            modal_vitalRespiratoryRate: {
                number: true
            },
            modal_vitalsType: {
                required: true
            }
        }
    });

    $("#vitalsRadio").buttonset();
    $("#addVitals .datepicker").datepicker({
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        changeYear: true,
        defaultDate: (new Date()).getTime()
    }).focus(function () {
        $(this).datepicker("show");
    });

    $("#modal_vitalTime").timepicker({});

});