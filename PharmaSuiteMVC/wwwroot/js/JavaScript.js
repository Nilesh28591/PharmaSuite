$(document).ready(function () { });
$("#Demo").click(function () { $("#Modal").modal('show'); });

$("#save").click(function () {
    var obj = $("#EmpForm").serialize();

    $.ajax({
        url: "/Medicine/Add_Cat",
        type: "POST",
        dataType: "json",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: obj,
        success: function () {
            alert("Employee added");
        },

        error: function () {
            alert("Failed");
        }
    })
    $("#Modal").modal('hide');
})