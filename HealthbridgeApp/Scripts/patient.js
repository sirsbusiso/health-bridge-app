

function PopulateTable() {
    $.ajax({
        url: '/api/Patient',
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (res) {

            $("#patientInfo").empty();

            $.each(res, function (index, value) {

                $("#patientInfo").append("<tr>" +
                    "<td class='pId' hidden='hidden'>" + value.PatientId + "</td>" +
                    "<td>" +
                    "<button name='Details' title='Patient details' class='btn btn-success patientDetails'><span class='glyphicon glyphicon-info-sign' aria-hidden='true'></span></button> " +
                    "<button name='Edit' title='Update patient details' class='btn btn-info editPatient'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span></button> " +
                    "<button name='Remove' title='Remove a patient' class='btn btn-danger removePatient'><span class='glyphicon glyphicon-trash' aria-hidden='true'></span></button>" +
                    "</td>" +
                    "<td>" + value.FirstName + "</td>" +
                    "<td>" + value.LastName + "</td>" +
                    "<td>" + value.IdNumber + "</td>" +
                    
                    "</tr>");
            })

        },
        error: function (err) {
            console.log(err);
        }
    });
}

$(document).ready(function () {
    PopulateTable();
});

$(".addpatientbtn").click(function () {
    $("#modalLabel").html("Add New Patient");
    $(".save").show();
    $(".edit").hide();
    $("#patientmodal").modal({ backdrop: false });
});

$('#patient-firstname').click(function () {
    $("#firstnameerror").html("");
});

$('#patient-lastname').click(function () {
    $("#lastnameerror").html("");
});

$('#patient-id').click(function () {
    $("#iderror").html("");
});

$(".save").click(function () {
    var reg = new RegExp('^[0-9]*$');
    var FirstName = $('#patient-firstname').val();
    var LastName = $('#patient-lastname').val();
    var IdNumber = $('#patient-id').val();

    if (FirstName == "") {
        $("#firstnameerror").html("Please enter First Name");
        return false;
    }
    else if (LastName == "") {
        $("#lastnameerror").html("Please enter Last Name");
        return false;
    }
    else if (IdNumber == "") {
        $("#iderror").html("Please enter Id Number");
        return false;
    }
    else if (IdNumber.length < 13 || IdNumber.length > 13) {
        $("#iderror").html("Id Number must be 13 digits");
        return false;
    }
    if (reg.test(IdNumber) == false) {
        $("#iderror").html("Only Numeric Id is allowed");
        return false;
    }
    else {
        var Patient = new Object();
        Patient.FirstName = $('#patient-firstname').val();
        Patient.LastName = $('#patient-lastname').val();
        Patient.IdNumber = $('#patient-id').val();

        var url = '/api/Patient';
        $.ajax({
            type: "POST",
            url: url,
            dataType: 'json',
            data: Patient,
            success: function (data) {
                PopulateTable();
                // Ajax call completed successfully
                alert(data);
                $("#patientmodal").modal('toggle');
            },
            error: function (data) {

                // Some error in ajax call
                alert("Opps Error");
            }
        });
    }
});

$("#closeModal").click(function () {
    debugger;
    $('#patient-lastname').val(" ");
    $('#patient-id').val(" ");
    $('#patient-firstname').val(" ");
    $('#pId').val(" ");

    $('#patient-lastname').removeAttr('readonly',false);

    $('#patient-id').removeAttr('readonly', false);

    $('#patient-firstname').removeAttr('readonly', false);

    $("#patientmodal").modal('toggle');
});

$("#patientInfo").on("click", "tr>td>button.removePatient", function () {

    var c = confirm('Are you sure you want to remove this patient?');

    if (c == true) {
        var PatientId = $(this).closest("tr").find(".pId").text().trim();

        var url = '/api/Patient/' + PatientId;
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

$("#patientInfo").on("click", "tr>td>button.patientDetails", function () {
    var PatientId = $(this).closest("tr").find(".pId").text().trim();

    $("#modalLabel").html("Patient Details");

    $(".edit").hide();
    $(".save").hide();

    var url = '/api/Patient/' + PatientId;
    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#patient-lastname').val(result.LastName);
            $('#patient-lastname').attr('readonly', 'true');

            $('#patient-id').val(result.IdNumber);
            $('#patient-id').attr('readonly', 'true');

            $('#patient-firstname').val(result.FirstName);
            $('#patient-firstname').attr('readonly', 'true');

            $('#pId').val(PatientId);
            $("#patientmodal").modal({ backdrop: false });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
});

$("#patientInfo").on("click", "tr>td>button.editPatient", function () {
    var PatientId = $(this).closest("tr").find(".pId").text().trim();
    $("#modalLabel").html("Edit Patient");
    $(".edit").show();
    $(".save").hide();
    var url = '/api/Patient/' + PatientId;
    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
          
            $('#patient-lastname').val(result.LastName);
            $('#patient-lastname').removeAttr('readonly');

            $('#patient-id').val(result.IdNumber);
            $('#patient-id').removeAttr('readonly');

            $('#patient-firstname').val(result.FirstName);
            $('#patient-firstname').removeAttr('readonly');

            $('#pId').val(PatientId);
            $("#patientmodal").modal({ backdrop: false });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
});

$(".edit").click(function () {
    var reg = new RegExp('^[0-9]*$');
    var FirstName = $('#patient-firstname').val();
    var LastName = $('#patient-lastname').val();
    var IdNumber = $('#patient-id').val();
    

    if (FirstName == "") {
        $("#firstnameerror").html("Please enter First Name");
        return false;
    }
    else if (LastName == "") {
        $("#lastnameerror").html("Please enter Last Name");
        return false;
    }
    else if (IdNumber == "") {
        $("#iderror").html("Please enter Id Number");
        return false;
    }
    else if (IdNumber.length < 13 || IdNumber.length > 13) {
        $("#iderror").html("Id Number must be 13 digits");
        return false;
    }
    if (reg.test(IdNumber) == false) {
        $("#iderror").html("Only Numeric Id is allowed");
        return false;
    }
    else {
        var Patient = new Object();
        Patient.FirstName = $('#patient-firstname').val();
        Patient.LastName = $('#patient-lastname').val();
        Patient.IdNumber = $('#patient-id').val();
        Patient.PatientId = $('#pId').val();

        var url = '/api/Patient';
        $.ajax({
            type: "PUT",
            url: url,
            dataType: 'json',
            data: Patient,
            success: function (data) {
                PopulateTable();
                // Ajax call completed successfully
                alert(data);
                $("#patientmodal").modal('toggle');
            },
            error: function (data) {

                // Some error in ajax call
                alert("Opps Error");
            }
        });
    }
})
