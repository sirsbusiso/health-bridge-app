function PopulateTable() {
    $.ajax({
        url: '/api/Invoice/Get',
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (res) {

            $("#invoiceInfo").empty();

            $.each(res, function (index, value) {
                var invDate = value.InvoiceDateTime;

                if (invDate != null) { invDate = invDate.substring(0, 10); }


                $("#invoiceInfo").append("<tr>" +
                    "<td class='iId' hidden='hidden'>" + value.InvoiceId + "</td>" +
                    "<td>" +
                    "<button name='Details' class='btn btn-info viewDetails'><span class='glyphicon glyphicon-info-sign' aria-hidden='true'></span></button> " +
                    "<button name='Remove' class='btn btn-danger removeInvoice'><span class='glyphicon glyphicon-trash' aria-hidden='true'></span></button>" +
                    "</td>" +
                    "<td>" + invDate + "</td>" +
                    "<td>" + value.Patient + "</td>" +
                    "<td>" + value.InvoiceTotal + "</td>" +
                    "</tr>");
            })

        },
        error: function (err) {
            console.log(err);
        }
    });
}

function PopulateDetailsTable(id) {
    $.ajax({
        url: '/api/InvoiceLine/' + id,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (res) {

            $("#invoiceDetails").empty();

            $.each(res, function (index, value) {


                var invDate = value.InvoiceDate;
                if (invDate != null) { invDate = invDate.substring(0, 10); }


                $("#iDate").text(invDate);
                $("#pName").text(value.Patient);
                $('#invTotal').text(value.InvoiceTotal);




                $("#invoiceDetails").append("<tr>" +
                    "<td class='ilId' hidden='hidden'>" + value.InvoiceLineId + "</td>" +
                    "<td class='iId' hidden='hidden'>" + value.InvoiceId + "</td>" +
                    "<td>" +
                    "<button name='Edit' class='btn btn-success updateInvoiceLine'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span></button> " +
                    "<button name='Remove' class='btn btn-danger removeInvoiceLine'><span class='glyphicon glyphicon-trash' aria-hidden='true'></span></button>" +
                    "</td>" +
                    "<td>" + value.Qty + "</td>" +
                    "<td>" + value.Code + "</td>" +
                    "<td>" + value.Description + "</td>" +
                    "<td>" + value.LineTotal + "</td>" +
                    "</tr>");
            })

        },
        error: function (err) {
            console.log(err);
        }
    });
}
$("#invoiceInfo").on("click", "tr>td>button.removeInvoice", function () {

    var c = confirm('Are you sure you want to remove this invoice?');

    if (c == true) {
        var invoiceId = $(this).closest("tr").find(".iId").text().trim();

        var url = '/api/Invoice/' + invoiceId;
        $.ajax({
            url: url,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {

                alert(result);
                PopulateTable();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        return false;
    }
});

$("#invoiceDetails").on("click", "tr>td>button.updateInvoiceLine", function () {
    debugger;
    var invoiceLineId = $(this).closest("tr").find(".ilId").text().trim();
    var invoiceId = $(this).closest("tr").find(".iId").text().trim();
    var url = '/api/Invoice/Get/' + invoiceLineId;
    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#line-qty').val(result.Qty);
            $('#line-code').val(result.Code);

            $('#line-total').val(result.LineTotal);
            $('#line-description').val(result.Description);

            $('#lineId').val(invoiceLineId);
            $('#invoiceId').val(invoiceId);

            $("#lineeditmodal").modal({ backdrop: false });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

});

$(".createinvoicelinebtn").click(function () {
    $("#invoiceModal").modal({ backdrop: false });
})



$("#invoiceInfo").on("click", "tr>td>button.viewDetails", function () {
    debugger;
    var invoiceId = $(this).closest("tr").find(".iId").text().trim();
    var invoiceLineId = $(this).closest("tr").find(".ilId").text().trim();
    $("#invoiceId").val(invoiceId);
    $("#invoiceLineId").val(invoiceLineId);
    PopulateDetailsTable(invoiceId);
    $("#invoice-line-details").show();
    $("#invoice-line-info").hide();

});

$("#invoiceDetails").on("click", "tr>td>button.removeInvoiceLine", function () {
    var c = confirm('Are you sure you want to remove this invoice line?');

    if (c == true) {
        debugger;
        var invoiceLineId = $(this).closest("tr").find(".ilId").text().trim();
        var invoiceId = $(this).closest("tr").find(".iId").text().trim();
        var url = '/api/InvoiceLine/' + invoiceLineId;
        $.ajax({
            url: url,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {

                alert(result);
                $("#iDate").text(" ");
                $("#pName").text(" ");
                $('#invTotal').text("0.00");
                PopulateDetailsTable(invoiceId);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        return false;
    }
});

$(document).ready(function () {
    PopulateTable();

    $('#invoice-patientname').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/api/Patient/GetById/' + request.term,
                dataType: 'json',
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.FirstName + " " + item.LastName,
                            value: item.PatientId
                        }
                    }));
                },
                error: function (error) {
                    console.log(error);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            event.preventDefault();
            console.log(ui);
            $('#patientId').val(ui.item.value);
            $('#invoice-patientname').val(ui.item.label);
        }
    });
});

$(".backtolist").click(function () {
    PopulateTable();
    $("#invoice-line-details").hide();
    $("#invoice-line-info").show();

});

$(".createinvoicebtn").click(function () {
    $("#modalLabel").html("Create New Invoice");
    $(".save").show();
    $(".edit").show();
    $("#invoiceModal").modal({ backdrop: false });
});

$('#addItem').click(function () {
    var lastItem = $('#lineItems').children('.row').not('#header-row').last();
    var lastItemId = lastItem.attr('id');
    var lastItemIdNum = parseInt(lastItemId.replace('row', ''));

    lastItem.parent().append(
        '<div class="row" id="row' + (lastItemIdNum + 1) + '">' +
        '    <div class= "col-lg-2" >' +
        '        <input type="number" class="form-control" id="qty' + (lastItemIdNum + 1) + '" value="1" />' +
        '    </div >' +
        '   <div class="col-lg-3">' +
        '        <input class="form-control" id="code' + (lastItemIdNum + 1) + '" />' +
        '    </div>' +
        '    <div class="col-lg-4">' +
        '        <textarea class="form-control" id="desc' + (lastItemIdNum + 1) + '"></textarea>' +
        '    </div>' +
        '    <div class="col-lg-2">' +
        '        <input class="form-control itemTotal" id="total' + (lastItemIdNum + 1) + '" value="0"/>' +
        '    </div>' +
        '    <div class="col-lg-1">' +
        '        <span id="remove' + (lastItemIdNum + 1) + '" class="glyphicon glyphicon-remove alert-danger removeItem"></span>' +
        '    </div>' +
        '</div >'
    );

    if (lastItemIdNum === 1) {
        $('#remove1').show();
    } else {
        var firstRow = $('#lineItems').children('.row').not('#header-row').first();
        var firstRowId = firstRow.attr('id');
        var firstRowIdNum = parseInt(firstRowId.replace('row', ''));

        $('#remove' + firstRowIdNum).show();
    }

});



$('#lineItems').on('click', '.removeItem', function () {
    if ($('#lineItems').children('.row').not('#header-row').length > 1) {
        var rowToRemove = $(this).parent().parent();
        rowToRemove.remove();
    }

    if ($('#lineItems').children('.row').not('#header-row').length === 1) {
        var lastItem = $('#lineItems').not('#header-row').children('.row').last();
        var lastItemId = lastItem.attr('id');
        var lastItemIdNum = parseInt(lastItemId.replace('row', ''));

        $('#remove' + lastItemIdNum).hide();
    }
});

$(".save").click(function () {
    var invoiceDate = $('#invoice-invoicedate').val();
    var patientId = $('#patientId').val();
    var patient = $("#invoice-patientname").val();
    var isValid = false;
    var invoice = {
        invoiceDateTime: invoiceDate,
        patientId: patientId,
        createInvoiceLineViewModels: []
    };

    var lineItems = $('#lineItems .row').not('#header-row');

    $.each(lineItems, function (i, e) {
        console.log(e);
        var values = e.getElementsByClassName('form-control');

        console.log(values);

        var lineItem = {
            qty: values[0].value,
            code: values[1].value,
            description: values[2].value,
            lineTotal: values[3].value
        };
        debugger;
        if (invoice.invoiceDateTime === '') {
            alert("Please select date");
            isValid = false;
        }
        else if (patient === '') {
            alert("Please select patient");
            isValid = false;
        }
        else if (lineItem.qty <= 0) {
            alert("Qty can not be 0 or less");
            isValid = false;
        }
        else if (lineItem.qty === '') {
            alert("Please enter qty");
            isValid = false;
        }
        else if (lineItem.code.length > 10) {
            alert("Code can not exceed 10 charactors");
            isValid = false;
        }
        else if (lineItem.code === '') {
            alert("Please enter code");
            isValid = false;
        }
        else if (lineItem.description.length > 250) {
            alert("Description can not exceed 250 charactors");
            isValid = false;
        }
        else if (lineItem.description === '') {
            alert("Please enter description");
            isValid = false;
        }
        else if (lineItem.lineTotal <= 0) {
            alert("Line total can not be 0 or less");
            return false;
        }
        else if (lineItem.lineTotal === '') {
            alert("Please enter lineTotal");
            isValid = false;
        }
        else {
            invoice.createInvoiceLineViewModels.push(lineItem);
            isValid = true;
        }

    });

    if (isValid) {
        var url = '/api/Invoice';
        $.ajax({
            type: "POST",
            url: url,
            dataType: 'json',
            data: invoice,
            success: function (data) {
                // Ajax call completed successfully
                alert(data);
                $("#invoiceModal").modal('toggle');
                var url = '/Invoice/Index';
                window.location.href = url;
            },
            error: function (data) {

                // Some error in ajax call
                alert("Error, please make sure you fill in all the information");
            }
        });
    }

});

$(".edit").click(function () {
    debugger;
    var isValid = false;
    var line = {
        Qty: $('#line-qty').val(),
        Code: $('#line-code').val(),
        Description: $('#line-description').val(),
        Total: $('#line-total').val(),
        InvoiceLineId: $('#lineId').val(),
        LineTotal: $('#line-total').val(),
        invoiceId: $('#invoiceId').val()

    };
    if (line.Qty <= 0) {
        alert("Qty can not be 0 or less");
        return false;
    }
    else if (line.Qty === '') {
        alert("Please enter qty");
        return false;
    }
    else if (line.Code.length > 10) {
        alert("Code can not exceed 10 charactors");
        return false;
    }
    else if (line.Code === '') {
        alert("Please enter code");
        return false;
    }
    else if (line.Description.length > 250) {
        alert("Description can not exceed 250 charactors");
        return false;
    }
    else if (line.Description === '') {
        alert("Please enter description");
        return false;
    }
    else if (line.LineTotal <= 0) {
        alert("Line total can not be 0 or less");
        return false;
    }
    else if (line.LineTotal === '') {
        alert("Please enter line total");
        return false;
    }
    else {
        isValid = true;
    }

    var url = '/api/InvoiceLine';
    $.ajax({
        type: "PUT",
        url: url,
        dataType: 'json',
        data: line,
        success: function (data) {

            PopulateDetailsTable(line.invoiceId);
            $("#iDate").text(" ");
            $("#pName").text(" ");
            $('#invTotal').text("0.00");
            // Ajax call completed successfully
            alert(data);
            $("#lineeditmodal").modal('toggle');
        },
        error: function (data) {

            // Some error in ajax call
            alert("Error, please make sure you fill in all the information");
        }
    });
});
$("#closeModal").click(function () {
    $('#line-qty').val(" "),
        $('#line-code').val(" "),
        $('#line-description').val(" "),
        $('#line-total').val(" "),
        $('#lineId').val(" "),
        $('#line-total').val(" "),
        $('#invoiceId').val(" "),

        $("#invoiceModal").modal('toggle');
});

$("#close").click(function () {
    $('#line-qty').val(" "),
        $('#line-code').val(" "),
        $('#line-description').val(" "),
        $('#line-total').val(" "),
        $('#lineId').val(" "),
        $('#line-total').val(" "),
        $('#invoiceId').val(" "),

        $("#lineeditmodal").modal('toggle');
});