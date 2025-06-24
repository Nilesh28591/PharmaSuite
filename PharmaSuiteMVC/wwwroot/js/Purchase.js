// =========================
// PURCHASE.JS
// =========================
function loadPurchases() {
    $.ajax({
        url: '/Purchase/GetAllPurchases',
        method: "GET",
        success: function (data) {
            const tbody = $("#purchaseTable tbody");
            tbody.empty();

            $.each(data, function (index, purchase) {
                let itemsHtml = "<ul>";
                $.each(purchase.items, function (i, item) {
                    itemsHtml += `<li>${item.name} (Qty: ${item.quantity}) (Cost Price: ${item.costPrice})</li>`;
                });
                itemsHtml += "</ul>";

                tbody.append(` 
                    <tr>
                        <td>${purchase.purchaseId ?? ''}</td>
                        <td>${purchase.name}</td>
                        <td>${purchase.invoiceNumber}</td>
                        <td>${new Date(purchase.purchaseDate).toLocaleDateString()}</td>
                        <td>${itemsHtml}</td>
                        <td>
                            <button class="btn btn-sm btn-primary editBtn" data-id="${purchase.purchaseId ?? ''}" title="Edit"><i class="bi bi-pencil-square"></i></button>
                            <button class="btn btn-sm btn-danger deleteBtn" data-id="${purchase.purchaseId ?? ''}" title="Delete"><i class="bi bi-trash"></i></button>
                        </td>
                    </tr> 
                `);
            });
        },
        error: function (xhr) {
            alert("Error loading purchases: " + xhr.statusText);
        }
    });
}


// =========================
// STEP 1: Load Suppliers
// =========================
function loadSuppliers() {
   
    $.ajax({
        url: '/Purchase/GetAllSuppliers',
        method: 'GET',
        success: function (data) {
            let options = '<option value="">--Select--</option>';
            $.each(data, function (index, supplier) {
                options += `<option value="${supplier.supplierId}" 
                            data-email="${supplier.email}"
                            data-contact="${supplier.contactPerson}"
                            data-phone="${supplier.phone}"
                            data-address="${supplier.address}">${supplier.name}</option>`;
            });
            $("#supplierId").html(options);
        },
        error: function (xhr) {
            alert("Error loading suppliers: " + xhr.statusText);
           
        }
    });
}

// =========================
// STEP 2: Load Medicines
// =========================
function loadMedicines() {
    $.ajax({
        url: '/Purchase/GetAllMedicines',
        method: 'GET',
        success: function (data) {
            let options = '<option value="">--Select--</option>';
            $.each(data, function (index, medicine) {
                options += `<option value="${medicine.medicineId}"
                            data-category="${medicine.category}"
                            data-manufacturer="${medicine.manufacturer}"
                            data-price="${medicine.pricePerUnit}"
                            data-expiry="${medicine.expiryDate}"
                            data-batchno="${medicine.batchNo}"
                            data-cost="${medicine.costPrice}"
                                                            >${medicine.name}</option>`;
            });
            $("#medicineId").html(options);
        },
        error: function (xhr) {
            alert("Error loading medicines: " + xhr.statusText);
        }
    });
}



$(document).on('change', '#supplierId', function () {
    const selected = $(this).find(':selected');
    $("#supplierEmail").val(selected.data("email"));
    $("#supplierContactPerson").val(selected.data("contact"));
    $("#supplierPhone").val(selected.data("phone"));
    $("#supplierAddress").val(selected.data("address"));
});

$(document).on('change', '#medicineId', function () {
    const selected = $(this).find(':selected');
    $("#medicineCategory").val(selected.data("category"));
    $("#medicineManufacturer").val(selected.data("manufacturer"));
    $("#medicinePrice").val(selected.data("price"));
    $("#batchNo").val(selected.data("batchno"));
    $("#costPrice").val(selected.data("cost"));
    $("#expiryDate").val(new Date(selected.data("expiry")).toISOString().split('T')[0]);
});

function calculateCoste() {
    const priceperunit = parseFloat($("#medicinePrice").val());
    const quantity = parseInt($("#quantity").val());
    if (!isNaN(priceperunit) && !isNaN(quantity) && quantity > 0) {
        const total = priceperunit * quantity;
        $("#costPrice").val(total.toFixed(2));
    } else {
        $("#costPrice").val('');
    }
}

$(document).on('input', '#quantity', function () {
    calculateCoste();
});
// =========================
// STEP 3: Submitting the Purchase
// =========================

$(document).on("click", "#addPurchaseButton", function () {
    $("#purchaseForm")[0].reset();
    $("#purchaseForm").removeData("purchaseId"); // Important
});

$(function () {
    loadPurchases();
    loadSuppliers();
    loadMedicines();

    $("#purchaseForm").submit(function (e) {
        e.preventDefault();
        const purchaseId = $(this).data("purchaseId"); // Get Id if it's an Edit
        const dto = {
            supplierId: parseInt($("#supplierId").val()),
            name: $("#supplierId option:selected").text(),
            email: $("#supplierEmail").val(),
            invoiceNumber: $("#invoiceNumber").val(),
            createdBy: $("#createdBy").val(),
            items: [
                {
                    medicineId: parseInt($("#medicineId").val()),
                    name: $("#medicineId option:selected").text(),
                    batchNo: $("#batchNo").val(),
                    mfgDate: $("#mfgDate").val(),
                    expiryDate: $("#expiryDate").val(),
                    quantity: parseInt($("#quantity").val()),
                    costPrice: parseFloat($("#costPrice").val()),
                    createdBy: $("#createdBy").val()
                }
            ]
        };

        let url = '/Purchase/AddPurchaseAsync';
        let method = "POST";

        if (purchaseId) {
            // It's an Edit case
            url = `/Purchase/EditPurchase/${purchaseId}`;
            method = "PUT"; // You'd implement an Update method
        }

        $.ajax({
            url: url,
            method: method,
            contentType: "application/json",
            data: JSON.stringify(dto),
            success: function () {
                alert(purchaseId ? "Purchase updated successfully!" : "Purchase added successfully!");
                $("#addPurchaseModal").modal("hide");
                $("#purchaseForm")[0].reset();
                $("#purchaseForm").removeData("purchaseId");
                loadPurchases();
            },
            error: function (xhr) {
                alert("Error saving purchase: " + xhr.responseText);
            }
        });
    });
});

// =========================
// STEP 4: Edit Button Click
// =========================
$(document).on("click", ".editBtn", function () {
    const id = $(this).data("id");
    $.ajax({
        url: `/Purchase/GetPurchaseById?id=${id}`,
        method: "GET",
        success: function (data) {
            if (data) {
                $("#supplierId").val(data.supplierId).trigger("change");
                $("#invoiceNumber").val(data.invoiceNumber);
                $("#createdBy").val(data.createdBy);

                if (data.items.length > 0) {
                    const item = data.items[0];
                    $("#medicineId").val(item.medicineId).trigger("change");
                    $("#batchNo").val(item.batchNo);
                    $("#quantity").val(item.quantity);
                    $("#costPrice").val(item.costPrice);
                    $("#expiryDate").val(new Date(item.expiryDate).toISOString().split('T')[0]);
                }

                $("#addPurchaseModal").modal("show");

                $("#purchaseForm").data("purchaseId", id)
            } else {
                alert("Purchase not found.");
            }
        },
        error: function (xhr) {
            alert("Error retrieving the purchase: " + xhr.statusText);
        }
    });



});

// =========================
// STEP 6: Delete Button Click
// =========================

$(document).on("click", ".deleteBtn", function () {
    const id = $(this).data("id"); // Get the Purchase Id
    if (confirm("Are you sure you want to delete this purchase?")) {
        $.ajax({
            url: '/Purchase/DeletePurchase?id=' + id,
            method: 'DELETE',
            success: function (response) {
                alert("Purchase deleted successfully!");
                loadPurchases();
            },
            error: function (xhr) {
                alert("Error deleting purchase: " + xhr.responseText);
            }
        });
    }
});

