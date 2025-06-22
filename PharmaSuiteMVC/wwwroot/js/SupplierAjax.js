
$(document).ready(function () {
    FetchSup().then(function () {
        //    $('#myTable').DataTable(); // Initialize only after data is loaded
    });
})



//function FetchSup() {
//    $.ajax({
//        url: "/Supplier/GetSuppliers",
//        //url: "https://localhost:7259/api/Supplier/GetSup/",
//        type: "Get",
//        dataType: "json",
//        contentType: "application/json; charset=UTF-8",

//        success: function (result) {
//            console.log("Fetched data:", result);
//            var obj = ""
//            $.each(result, function (index, item) {
//                obj += "<tr>",
//                    obj += "<td>" + item.supplierId + "</td>",
//                    obj += "<td>" + item.name + "</td>",
//                    obj += "<td>" + item.contactPerson + "</td>",
//                    obj += "<td>" + item.phone + "</td>",
//                    obj += "<td>" + item.email + "</td>",
//                    obj += "<td>" + item.address + "</td>",
//                    obj += "<td>" + item.createdBy + "</td>",
//                    obj += "<td>" + item.createdAt + "</td>",
//                    obj += "<td>" + item.modifiedBy + "</td>",
//                    obj += "<td>" + item.modifiedAt + "</td>",
//                    obj += '<td><a class="btn btn-danger" onclick="DeleteEmp(' + item.supplierId + ')">Delete</a> <a class="btn btn-primary" onclick="EditEmp(' + item.supplierId + ')">Edit</a></td>',
//                    obj += "</tr>"
//            })
//            $("#empTable").html(obj);
//            $('#myTable').DataTable();

//        },
//        error: function () {
//            alert("Failed to fetch employees");
//        }
//    });
//}






function FetchSup() {
    $.ajax({
        url: "/Supplier/GetSuppliers",
        type: "GET",
        dataType: "json",
        success: function (result) {
            // 1. Destroy existing DataTable if exists
            if ($.fn.DataTable.isDataTable('#myTable')) {
                $('#myTable').DataTable().clear().destroy();
            }

            // 2. Build table body
            var obj = "";
            $.each(result, function (index, item) {
                obj += `<tr>
                    <td>${item.supplierId}</td>
                    <td>${item.name}</td>
                    <td>${item.contactPerson}</td>
                    <td>${item.email}</td>
                    <td>${item.phone}</td>
                    <td>${item.address}</td>
                    <td>${item.createdBy}</td>
                    <td>${item.createdAt}</td>
                    <td>${item.modifiedBy}</td>
                    <td>${item.modifiedAt}</td>
                    <td>
                        <a onclick="EditEmp(${item.supplierId})" class="btn btn-sm btn-primary">Edit</a>
                        <a onclick="DeleteEmp(${item.supplierId})" class="btn btn-sm btn-danger">Delete</a>
                    </td>
                </tr>`;
            });
            $("#myTable tbody").html(obj);

            // 3. Reinitialize DataTable
            $('#myTable').DataTable();
        },
        error: function () {
            alert("Failed to load suppliers");
        }
    });
}










$("#addbtn").click(function () {
    $("#exampleModal").modal('show');
    $("#updateBtn").hide();
    $("#Idsup").hide();

})

$("#close1").click(function () {
    $("#exampleModal").modal('hide');
})

$("#close2").click(function () {
    $("#exampleModal").modal('hide');
})



$("#closeUpdate").click(function () {
    $("#exampleModal2").modal('hide');
})


//$("#newsup").click(function () {
//    var obj = $("#addsupp").serialize();
//    $.ajax({
//        url: "/Supplier/AddSup",
//        type: "POST",
//        dataType: "json",
//        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
//        data: obj,

//        success: function () {
//            alert("Employee added");
//            FetchSup();
//        },

//        error: function () {
//            alert("Failed");
//        }
//    })
//    $("#exampleModal").modal("hide");


//});








$("#newsup").click(function (e) {
    e.preventDefault(); // prevent form from submitting traditionally

    //if (!$("#addsupp").valid()) {
    //    return; // stop if validation fails
    //}

    var obj = {
        Name: $("#Name").val(),
        ContactPerson: $("#ContactPerson").val(),
        Phone: $("#Phone").val(),
        Email: $("#Email").val(),
        Address: $("#Address").val()
    };

    $.ajax({
        url: "/Supplier/AddSup",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (response) {
            if (response.success) {
                alert("Supplier added successfully");
                FetchSup(); // Make sure this function exists
                $("#exampleModal").modal("hide");
            } else {
                alert("Add failed: " + (response.message || "Unknown error"));
            }
        },
        error: function (xhr) {
            alert("Error: " + xhr.status + " - " + xhr.responseText);
        }
    });
});




function DeleteEmp(id) {
    if (confirm("Are you sure you want to delete")) {
        $.ajax({
            url: "/Supplier/DeleteSup/?id=" + id,

            success: function () {
                FetchSup();
            }
        })
    }
    else {
        alert("Not Deleted");
    }

}


function EditEmp(id) {

    $("#newsup").hide();
    $("#updateBtn").show();


    $.ajax({
        url: "/Supplier/EditSupView?id=" + id,
        type: "Get",
        dataType: "json",
        contentType: "application/json; charset=UTF-8",

        success: function (result) {
            $("#SupplierId").val(result.supplierId);
            $("#Name").val(result.name);
            $("#ContactPerson").val(result.contactPerson);
            $("#Phone").val(result.phone);
            $("#Email").val(result.email);
            $("#Address").val(result.address);
            $("#exampleModal").modal('show');
            $("#Idsup").show();
        },
        error: function () {
            alert("Failed to fetch");
        }
    });

}




$("#updateBtn").click(function () {
    var obj = $("#addsupp").serialize();

    $.ajax({
        url: "/Supplier/EditSup",
        type: "POST",
        dataType: "json",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: obj,

        success: function () {
            alert("Successfully Updated");
            $("#exampleModal").modal("hide");
            FetchSup();
        },

        error: function () {
            alert("Failed to Update");
        }

    });
})












