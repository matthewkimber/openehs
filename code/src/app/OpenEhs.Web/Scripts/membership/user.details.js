/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery-ui.js" />

$(document).ready(function () {
    $("#tabs").tabs();

    $("#AddRoleDialog").dialog({
        autoOpen: false,
        height: 150,
        width: 300,
        modal: true,
        buttons: {
            "Save": function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/User/AddRole",
                    data: {
                        id: $("#SelectedRole").val(),
                        userId: $("#UserId").val()
                    },
                    success: function (response) {
                        if (response.success) {
                            $("#AddRoleDialog").dialog("close");
                            $("#RoleList ul").append("<li class=\"group\"><div class=\"RoleName group\">" + response.RoleName + "</div><div class=\"RemoveRoleAction group\"><img src=\"/images/common/icons/16x16/Delete.png\" id=\"" + response.RoleId + "\" alt=\"Remove Role\" /></div></li>");
                        }
                        else {
                            alert(response.error);
                        }
                    },
                    dataType: "json"
                });
            },
            Cancel: function () {
                $("#AddRoleDialog").dialog("close");
            }
        },
        close: function () { }
    });

    $("#AddRoleButton").click(function () {
        $("#AddRoleDialog").dialog("open");
    });

    $(".RemoveRoleAction").live("click", function () {
        var role = $(this);

        $.ajax({
            type: "POST",
            url: "/Admin/User/RemoveRole",
            data: {
                id: role.find("img").attr("id"),
                userId: $("#UserId").val()
            },
            success: function (response) {
                if (response.success) {
                    role.parent().remove();
                }
                else {
                    alert(response.error);
                }
            },
            error: function (response) {
                alert("An error has occurred while removing a role. " + response.error);
            },
            dataType: "json"
        });
    });
});