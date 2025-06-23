var customerModal = new bootstrap.Modal(document.getElementById('customerModal'));

$(document).ready(function () {
    $('#btnAddCustomer').click(function () {
        clearForm();
        $('#customerModalLabel').text('Add Customer');
        customerModal.show();
    });

    $('#btnViewCustomers').click(function () {
        loadCustomers();
        $('#customersTableDiv').show();
    });

    $('#customerForm').submit(function (e) {
        e.preventDefault();
        var customer = {
            CustomerId: $('#CustomerId').val() || 0,
            Name: $('#Name').val(),
            Mobile: $('#Mobile').val(),
            Address: $('#Address').val()
        };

        if (customer.CustomerId == 0 || customer.CustomerId === "") {
            // Add new customer
            $.ajax({
                url: '/AjaxCustomer/Add',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(customer),
                success: function (res) {
                    alert(res.message);
                    customerModal.hide();
                    loadCustomers();
                    $('#customersTableDiv').show();
                },
                error: function () {
                    alert('Failed to add customer.');
                }
            });
        } else {
            // Update existing customer
            $.ajax({
                url: '/AjaxCustomer/Update',
                method: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(customer),
                success: function (res) {
                    alert(res.message);
                    customerModal.hide();
                    loadCustomers();
                    $('#customersTableDiv').show();
                },
                error: function () {
                    alert('Failed to update customer.');
                }
            });
        }
    });
});

function loadCustomers() {
    $.ajax({
        url: '/AjaxCustomer/GetAll',
        method: 'GET',
        success: function (data) {
            var tbody = '';
            $.each(data, function (i, cust) {
                tbody += `<tr>
                    <td>${i + 1}</td>
                    <td>${cust.name}</td>
                    <td>${cust.mobile}</td>
                    <td>${cust.address}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="editCustomer(${cust.customerId})">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteCustomer(${cust.customerId})">Delete</button>
                    </td>
                </tr>`;
            });
            $('#customerTable tbody').html(tbody);
        },
        error: function () {
            alert('Failed to load customers.');
        }
    });
}

function clearForm() {
    $('#CustomerId').val('');
    $('#Name').val('');
    $('#Mobile').val('');
    $('#Address').val('');
}

function editCustomer(id) {
    $.ajax({
        url: `/AjaxCustomer/Edit/${id}`,
        method: 'GET',
        success: function (cust) {
            $('#CustomerId').val(cust.customerId);
            $('#Name').val(cust.name);
            $('#Mobile').val(cust.mobile);
            $('#Address').val(cust.address);
            $('#customerModalLabel').text('Edit Customer');
            customerModal.show();
        },
        error: function () {
            alert('Failed to fetch customer details.');
        }
    });
}

function deleteCustomer(id) {
    if (confirm("Are you sure to delete this customer?")) {
        $.ajax({
            url: `/AjaxCustomer/Delete/${id}`,
            method: 'DELETE',
            success: function () {
                loadCustomers();
            },
            error: function () {
                alert('Failed to delete customer.');
            }
        });
    }
}
