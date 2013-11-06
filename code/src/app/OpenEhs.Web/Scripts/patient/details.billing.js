/// <reference path="../jquery-1.4.4.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.ui.js" />
/// <reference path="../jquery-jvert-tabs-1.1.4.js" />

$(function () {

    //----------------------------------------------//
    // Add new Line Item                            //
    //----------------------------------------------//

    $("#submitInvoiceItem").button().click(function () {
        $.ajax({
            type: "POST",
            url: "/Patient/AddInvoiceItem",
            data: {
                patientID: $("#patientId").val(),
                product: $("#modal_GetProduct").val(),
                service: "",
                quantity: $("#modal_GetQuantity").val()
            },
            success: function (response) {
                var table = document.getElementById("detailsTable");
                var tr = document.createElement("tr");

                var elements = new Array();

                for (var i = 0; i < 2; i++)
                    elements[i] = document.createElement("td");

                elements[0].appendChild(document.createTextNode(response.Name));
                elements[1].appendChild(document.createTextNode(response.Quantity));

                for (var i = 0; i < 2; i++)
                    tr.appendChild(elements[i]);

                table.appendChild(tr);

                product: $("#modal_GetProduct").val("");
                //service: $("#modal_GetService").val("");
                quantity: $("#modal_GetQuantity").val("");
            },
            dataType: "json"
        });
    });

    $("#submitInvoiceItemService").button().click(function () {
        $.ajax({
            type: "POST",
            url: "/Patient/AddInvoiceItem",
            data: {
                patientID: $("#patientId").val(),
                product: "",
                service: $("#modal_GetService").val(),
                quantity: "1"
            },
            success: function (response) {
                var table = document.getElementById("detailsTable");
                var tr = document.createElement("tr");

                var elements = new Array();

                for (var i = 0; i < 2; i++)
                    elements[i] = document.createElement("td");

                elements[0].appendChild(document.createTextNode(response.Name));
                elements[1].appendChild(document.createTextNode(response.Quantity));

                for (var i = 0; i < 2; i++)
                    tr.appendChild(elements[i]);

                table.appendChild(tr);

                //product: $("#modal_GetProduct").val("");
                service: $("#modal_GetService").val("");
                //quantity: $("#modal_GetQuantity").val("");
            },
            dataType: "json"
        });
    });

});

