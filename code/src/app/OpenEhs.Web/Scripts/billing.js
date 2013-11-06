/// <reference path="jquery-1.4.4.js" />
/// <reference path="jquery.validate.js" />

$(document).ready(function () {

    $("#billing-tab").addClass("current");

    $("#InvoiceLineItems tbody tr:odd").addClass("striped");


    $("#CreateNewBillingButton").button();
    $("#SearchBillingButton").button();
    $("#PayInFullButton")
            .button()
            .click(function () {
                $("#addPaymentDialog").dialog("open");
            })
            .next()
                .button({
                    text: false,
                    icons: {
                        primary: "ui-icon-triangle-1-s"
                    }
                })
                .click(function () {
                    alert("Menu!");
                })
                .parent()
                    .buttonset();


    $("#addPaymentDialog").dialog({
        autoOpen: false,
        height: 200,
        width: 420,
        modal: true,
        buttons: {
            "Add Payment": function () {
                $.post("/Billing/AddPayment", {
                    InvoiceId: $("#PaymentInvoiceID").val(),
                    Amount: $("#model_paymentamount").val()
                }, function (result) {
                    $("#addPaymentDialog").dialog("close");

                    if (result.error) {
                        alert(result.message);
                        $("#model_paymentamount").val("");
                    } else {
                        var table = document.getElementById("PaymentsTable");
                        var tr = document.createElement("tr");

                        var elements = new Array();

                        for (var i = 0; i < 2; i++)
                            elements[i] = document.createElement("td");

                        elements[0].appendChild(document.createTextNode(result.Date));
                        elements[1].appendChild(document.createTextNode(result.Amount));
                        elements[1].setAttribute('style', 'text-align: right');

                        for (var i = 0; i < 2; i++)
                            tr.appendChild(elements[i]);

                        table.appendChild(tr);

                        htmlOutput = "<h1 id=\"InvoiceTotal\">";
                        htmlOutput += "Balance: ";
                        htmlOutput += result.Balance;
                        htmlOutput += "</h1>"
                        $("#InvoiceTotal").replaceWith(htmlOutput);

                        htmlOutput = "<h2 id=\"PaymentTotal\" style=\"text-align: right\">";
                        htmlOutput += "Total Paid: ";
                        htmlOutput += result.Payments;
                        htmlOutput += "</h2>";
                        $("#PaymentTotal").replaceWith(htmlOutput);

                        $("#model_paymentamount").val("");
                    }
                }, "json");
            },
            Cancel: function () {
                $("#model_paymentamount").val("");
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

    //                $("#PayInFullButton").button().click(function () {
    //                    $("#addPaymentDialog").dialog("open");
    //                });
});