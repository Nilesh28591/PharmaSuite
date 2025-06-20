$(document).on('change', 'select[name="MedicineId"]', function () {
    var medicineId = $(this).val();
    var $row = $(this).closest('.invoice-row');
    var $qtySpan = $row.find('.availableQty');
    var $priceInput = $row.find('.unitPrice');

    $.ajax({
        url: `/Sale/getQuantity/${medicineId}`,
        method: 'GET',
        success: function (data) {
            $qtySpan.text(data ?? '0');
        },
        error: function () {
            $qtySpan.text('Error');
        }
    });

    $.ajax({
        url: `/Sale/getUnitPrice/${medicineId}`,
        method: 'GET',
        success: function (data) {
            $priceInput.val(data);
        },
        error: function () {
            $priceInput.val('Error');
        }
    });
});

let itemIndex = 1;

function addItem() {
    const options = $('#medicineOptions').html();

    const itemHTML = `
      <div class="row g-3 mb-3 invoice-row border p-3 rounded bg-light" data-index="${itemIndex}">
        <div class="col-md-3">
          <select class="form-select" name="MedicineId">
            <option value="" disabled selected>-- Select Medicine --</option>
            ${options}
          </select>
          <small class="text-muted">Available Quantity: <span class="availableQty">-</span></small>
        </div>
        <div class="col-md-2">
          <input type="number" class="form-control quantity" name="Quantity" placeholder="Qty" min="1" required />
        </div>
        <div class="col-md-2">
          <input type="number" class="form-control unitPrice" name="UnitPrice" placeholder="Price/Unit" min="0" step="0.01" required readonly />
        </div>
        <div class="col-md-2">
          <input type="number" class="form-control discount" name="Discount" placeholder="Discount (%)" min="0" max="100" />
        </div>
        <div class="col-md-2">
          <input type="text" class="form-control lineTotal" placeholder="Total" readonly />
        </div>
        <div class="col-md-1 d-flex align-items-center">
          <button type="button" class="btn btn-danger btn-sm" onclick="removeItem(this)">X</button>
        </div>
      </div>
    `;

    $('#invoiceItems').append(itemHTML);
    itemIndex++;
}

function removeItem(btn) {
    $(btn).closest('.invoice-row').remove();
    calculateTotal();
}

function calculateTotal() {
    let total = 0;

    $('.invoice-row').each(function () {
        const $row = $(this);
        const qty = parseFloat($row.find('.quantity').val()) || 0;
        const price = parseFloat($row.find('.unitPrice').val()) || 0;
        const discount = parseFloat($row.find('.discount').val()) || 0;

        let lineTotal = qty * price;
        if (discount > 0) {
            lineTotal -= (lineTotal * discount / 100);
        }

        $row.find('.lineTotal').val(lineTotal.toFixed(2));
        total += lineTotal;
    });

    $('#totalAmountDisplay').text(total.toFixed(2));
    $('#totalAmount').val(total.toFixed(2));
}

function submitSale() {
    const sale = {
        customerName: $('[name="CustomerName"]').val(),
        saleDate: new Date().toISOString(),
        totalAmount: parseFloat($('#totalAmount').val()),
        saleItems: []
    };

    $('.invoice-row').each(function () {
        const $row = $(this);
        const item = {
            medicineId: parseInt($row.find('[name="MedicineId"]').val()),
            quantity: parseInt($row.find('[name="Quantity"]').val()),
            unitPrice: parseFloat($row.find('[name="UnitPrice"]').val()),
            discount: parseFloat($row.find('[name="Discount"]').val()) || 0
        };
        sale.saleItems.push(item);
    });

    $.ajax({
        url: "/Sale/Sale",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(sale),
        success: function (res) {
            if (res.success && res.redirectUrl) {
                window.location.href = res.redirectUrl;

            } else {
                alert("❌ Sale failed. Please try again.");
            }
        },
        error: function () {
            alert("❌ Error occurred during submission.");
        }
    });
}

$('#salesForm').on('input', calculateTotal);

$('#salesForm').on('submit', function (e) {
    e.preventDefault();
    submitSale();
});

// Expose to global scope if HTML uses onclick="addItem()" etc.
window.addItem = addItem;
window.removeItem = removeItem;
window.submitSale = submitSale;
