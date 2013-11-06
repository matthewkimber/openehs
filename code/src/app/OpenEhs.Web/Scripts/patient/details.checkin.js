/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {
    // ------------------------------------------------- //
    //  Setup Check In Modal                             //
    // ------------------------------------------------- //

    //Set up correct tabs.
    $.ajax({
        type: "POST",
        url: "/Patient/GetCurrentCheckin",
        data: {
            patientID: $("#patientId").val()
        },
        success: function (response) {
            if (response.checkin != "null") {
                $("#newCheckInButton").hide();
                $("#checkOutButton").show();
            }
            else {
                $("#vitalAddButton").button("disable");
                $("#submitSurgery").button("disable");
                $("#feedAddButton").button("disable");
                $("#intakeAddButton").button("disable");
                $("#suctionAddButton").button("disable");
                $("#urineAddButton").button("disable");
                $("#stoolAddButton").button("disable");
                $("#submitNoteButton").button("disable");
                $('#modal_GetStaffSurgeon').attr('disabled', true);
                $('#modal_GetStaffSurgeonAssistant').attr('disabled', true);
                $('#modal_GetStaffAnaesthetist').attr('disabled', true);
                $('#modal_GetStaffAnaesthetistAssistant').attr('disabled', true);
                $('#modal_GetStaffNurse').attr('disabled', true);
                $('#modal_GetStaffConsultant').attr('disabled', true);
                $('#surgeryStartTime').attr('disabled', true);
                $('#surgeryEndTime').attr('disabled', true);
                $('#modal_GetSurgeryLocation').attr('disabled', true);
                $('#caseType1').attr('disabled', true);
                $('#caseType2').attr('disabled', true);
                $("#submitInvoiceItemService").button("disable");
                $("#submitInvoiceItem").button("disable");
            }
        },
        dataType: "json"
    });

    $("#newCheckinModal").dialog({
        autoOpen: false,
        height: 225,
        width: 375,
        modal: true,
        buttons: {
            "Check In": function () {
                $.ajax({
                    type: "POST",
                    url: "/Patient/AddCheckIn",
                    data: {
                        patientID: $("#patientId").val(),
                        PatientType: $('input:radio[name="modal_checkinType"]:checked').val(),
                        staffId: $("select[name='staff'] option:selected").val(),
                        locationId: $("select[name='location'] option:selected").val()
                    },
                    success: function (response) {
                        $("#newCheckInButton").hide();
                        $("#newCheckinModal").dialog("close");
                        $("#checkOutButton").show();
                        $("#vitalAddButton").button("enable");
                        $("#submitNoteButton").button("enable");
                        $("#submitSurgery").button("enable");
                        $("#feedAddButton").button("enable");
                        $("#intakeAddButton").button("enable");
                        $("#suctionAddButton").button("enable");
                        $("#urineAddButton").button("enable");
                        $("#stoolAddButton").button("enable");
                        $('#modal_GetStaffSurgeon').attr('disabled', false);
                        $('#modal_GetStaffSurgeonAssistant').attr('disabled', false);
                        $('#modal_GetStaffAnaesthetist').attr('disabled', false);
                        $('#modal_GetStaffAnaesthetistAssistant').attr('disabled', false);
                        $('#modal_GetStaffNurse').attr('disabled', false);
                        $('#modal_GetStaffConsultant').attr('disabled', false);
                        $('#surgeryStartTime').attr('disabled', false);
                        $('#surgeryEndTime').attr('disabled', false);
                        $('#modal_GetSurgeryLocation').attr('disabled', false);
                        $('#caseType1').attr('disabled', false);
                        $('#caseType2').attr('disabled', false);
                        $("#submitInvoiceItemService").button("enable");
                        $("#submitInvoiceItem").button("enable");
                    },
                    dataType: "json"
                });
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        close: function () { }
    });

    // ------------------------------------------------- //
    //  Setup Check Out Modal                             //
    // ------------------------------------------------- //
    $("#checkOutModal").dialog({
        autoOpen: false,
        height: 170,
        width: 375,
        modal: true,
        buttons: {
            "Confirm": function () {
                $.ajax({
                    type: "POST",
                    url: "/Patient/CheckOut",
                    data: {
                        patientID: $("#patientId").val()
                    },
                    success: function (response) {
                        $("#checkOutButton").hide();
                        $("#checkOutModal").dialog("close");
                        $("#newCheckInButton").show();
                        $("#vitalAddButton").button("disable");
                        $("#submitInvoiceItemService").button("disable");
                        $("#submitInvoiceItem").button("disable");
                    },
                    dataType: "json"
                });
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        close: function () { }
    });

    $("#checkinRadio").buttonset();
})