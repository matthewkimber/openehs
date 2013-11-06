/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

//This is for the popup box
$(function () {
    var ckConfig = {
        toolbar: [
            ["Bold", "Italic", "-"],
            ["UIColor"]
        ],
        extraPlugins: "autogrow",
        autoGrow_maxHeight: 800
    };
    $("#NotesTextBox").ckeditor(ckConfig);

    $("#prewrittenNoteSelect").live('change', function () {
        $.post("/Patient/TemplateDetail", {
            ID: $("#prewrittenNoteSelect").val()
        }, function (result) {
            $("textarea#NotesTextBox").val(result.body);
        }, "json");
    });


    $("#submitNoteButton").button().click(function () {
        $("#newNoteDialog").dialog("open");
    });

    $("#newNoteDialog").dialog({
        autoOpen: false,
        height: 600,
        width: 710,
        modal: true,
        buttons: {
            "Save": function () {
                $.post("/Patient/AddNote", {
                    patientID: $("#patientId").val(),
                    NoteBody: $("#NotesTextBox").val(),
                    TemplateTitle: $("#templateTitle").val(),
                    StaffId: $("#staffId").val()
                }, function (result) {
                    $("#newNoteDialog").dialog("close");
                    var output = result.NoteBody + '<br />';
                    $("#submittedNoteList").append(output);
                    if (typeof result.templateId != 'undefined')
                        $("#prewrittenNoteSelect").append('<option value="' + result.templateId + '" >' + result.templateTitle + '</option>');
                    $("#prewrittenNoteSelect").val("");
                    $("#NotesTextBox").val("");
                    $("#templateTitle").val("");
                    $("#templateNoteTitle").hide();
                }, "json");
            },
            Cancel: function () {
                $("#NotesTextBox").val("");
                $("#templateTitle").val("");
                $("#templateNoteTitle").hide();
                $("#prewrittenNoteSelect").val("");
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });


    $("#templateNoteCheckBox").live('click', function () {
        var htmlOutput = '<div id="templateNoteTitle"><br /><br /><fieldset id="templateTitleFS"><b>Template Title:</b><br /><input id="templateTitle"></fieldset></div>';
        $("#templateNoteTitle").show();
        $("#addTempTitleDIV").replaceWith(htmlOutput);
    });

    $("#templateNoteCheckBox").button().click(function () {

    });

});