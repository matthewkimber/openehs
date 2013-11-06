/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {
    $("#CreatePatientButton").button();

    $("#showOpenCheckins").button().click(function () {
        $.ajax({
            type: "POST",
            url: "/Dashboard/ActiveCheckIns",
            data: {
                loc: $("select[name='loactionBox'] option:selected").val()
            },
            success: function (response) {
                var table = document.getElementById("searchCheckinResult");
                var rows = table.getElementsByTagName("tr");
                for (var i = rows.length - 1; i > 0; i--) {
                    table.deleteRow(i);
                }
                //$("#searchCheckinResult > tr"

                $.each(response.bob, function (index, Data) {
                    var html = "<tr><td><a href=\"/Patient/Details/" + Data.ID + "\">" + Data.ID + "</a></td><td>" + Data.Name +
                    "</td></tr>";
                    $("#searchCheckinResult").append(html);
                    //                    var tr = document.createElement("tr");

                    //                    var elements = new Array();

                    //                    for (var i = 0; i < 2; i++)
                    //                        elements[i] = document.createElement("td");


                    //                    elements[0].appendChild(document.createTextNode(Data.ID));
                    //                    elements[1].appendChild(document.createTextNode(Data.Name));


                    //                    for (var i = 0; i < 2; i++)
                    //                        tr.appendChild(elements[i]);

                    //                    table.appendChild(tr);
                    var html = "<tr><td><a href=\"/Patient/Details/" + Data.ID + "\">" + Data.ID + "</a></td><td>" + Data.Name +
                    "</td></tr>";
                    table.appendChild(html);
                });

            },
            dataType: "json"
        });

    });

});

$(document).ready(function () {
    $("#searchCheckinResult tbody tr").live('click', function () {
        var href = $(this).find("a").attr("href");

        if (href) {
            window.location = href;
        }
    });
});

//patientID: $("#patientId").val(),
//loc: $("select[name='loactionBox'] option:selected").val()