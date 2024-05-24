$(document).on('click', '.dish-option', function (e) {
    e.preventDefault();
    var id = $(this).data('id');
    $.ajax({
        url: '/Home/AddModal/' + id
    }).done((res) => {
        $('#modal').find('.modal-dialog').addClass('modal-lg');
        $('#modal-container').html(res);
        $('#modal').modal('show');
    })
});

$(document).on('click', '.options', function (e) {
    e.preventDefault();
    var classFor = $(this).data('for');
    $('.options').removeClass('active');
    $(this).addClass('active');

    $('.table-view').addClass('d-none');
    if (classFor != 'all')
        $('.table-view.' + classFor).removeClass('d-none');
    else
        $('.table-view').removeClass('d-none');
});

$(document).on('click', '.btn-add-modal, .btn-edit-modal', function (e) {
    e.preventDefault();
    var url = $(this).attr('href');
    $.ajax({
        url: url
    }).done((res) => {
        $('#modal-container').html(res);
        $('#modal').modal('show');
    });
});

$(document).on('submit', '#menu-form', function (e) {
    $.each($('#menu-form').find('.order-item'), (i, item) => {
        $(item).find('input.food').attr('name', `records[${i}].foodId`);
        $(item).find('input.quantity').attr('name', `records[${i}].quantity`);
        $(item).find('input.note').attr('name', `records[${i}].note`);
    });
}).on('click', '.payment-button', function (e) {
    e.preventDefault();
    var anyNewItem = $('.add-order').find('.order-item').length;
    if (anyNewItem) {
        alert('Chưa save kìa!');
    } else {
        var url = $(this).attr('href');
        $.ajax({
            url: url
        }).done((res) => {
            $('#modal').find('.modal-dialog').addClass('modal-lg');
            $('#modal-container').html(res);
            $('#modal').modal('show');
        });
    }
});

$(document).on('click', '.quantity-btn', function (e) {
    e.preventDefault();
    var focus = $(this).data('focus');
    if (focus == '' || focus == undefined)
        return;
    var value = parseInt($(focus).html());
    if ($(this).hasClass('bonus'))
        $(focus).html(value + 1);

    if ($(this).hasClass('minus')) {
        if (value - 1 <= 0)
            alert("Quantity must be greater than 0");
        else
            $(focus).html(value - 1);
    }
});

$(document).on('change', '.search-val', function () {
    $(this).parents('form').submit();
});

$(document).on('click', '.role-button', function (e) {
    e.preventDefault();
    $('.role-button').removeClass('active');
    $(this).addClass('active');
    var role = $(this).data('role');
    $('#role-ip').val(role);
});

$(document).on('click', '#img-d', function () {
    $('#img-input').trigger('click');
});

$(document).on('change', '#img-input', function (e) {
    var file = e.target.files[0];
    if (!file)
        $('#img-d').attr('src', '/images/no-image.png');
    else
        $('#img-d').attr('src', (window.URL ? URL : webkitURL).createObjectURL(file));
});

$(document).on('click', '.btn-remove', function (e) {
    e.preventDefault();
    var msg = $(this).data('message');
    var href = $(this).attr('href');
    $('#confirm-message').html(msg);
    $('#confirm-accept-btn').attr('href', href);
    $('#confirmModal').modal('show');
});

$(document).on('submit', '#employee-form', function (e) {
    var valid = true;
    if (!$('.emp-name').val()) {
        valid = false;
        $('.emp-name-error').html('Name is not empty');
    } else {
        $('.emp-name-error').html('');
    }
    if (!$('.emp-email').val()) {
        valid = false;
        $('.emp-email-error').html('Email is not empty');
    } else {
        $('.emp-email-error').html('');
    }
    if (!$('.emp-dob').val()) {
        valid = false;
        $('.emp-dob-error').html('Birthday is not empty');
    } else {
        $('.emp-dob-error').html('');
    }
    if (!$('.emp-phone').val()) {
        valid = false;
        $('.emp-phone-error').html('Phone is not empty');
    } else {
        $('.emp-phone-error').html('');
    }
    if (!valid)
        e.preventDefault();
});

$(document).on('submit', '#dish-form', function (e) {
    var valid = true;
    if (!$('.dish-name').val()) {
        valid = false;
        $('.dish-name-error').html('Name is not empty');
    } else {
        $('.dish-name-error').html('');
    }

    if ($('.dish-price').val() <= 0) {
        valid = false;
        $('.dish-price-error').html('Price is not empty');
    } else {
        $('.dish-price-error').html('');
    }

    if ($('.id').val() == 0 && $('#img-input')[0].files.length == 0) {
        valid = false;
        $('.dish-img-error').html('Image is not choosed');
    } else {
        $('.dish-img-error').html('');
    }
    if (!valid)
        e.preventDefault();
});

$(document).on('submit', '#table-form', function (e) {
    var valid = true;
    if (!$('.table-name').val()) {
        valid = false;
        $('.table-name-error').html('Name is not empty');
    } else {
        $('.table-name-error').html('');
    }
    if (!valid)
        e.preventDefault();
})

$(document).on('click', '.btn-add-new-food-to-bill', function (e) {
    e.preventDefault();
    var id = $(this).data('id');
    var name = $(this).data('name');
    var price = $(this).data('price');
    var priceStr = $(this).data('pricestr');
    var image = $(this).data('img');
    var note = $('.add-food-to-billnote').val();
    var quantity = $('.modal-add-quantity').html();
    $('#modal').modal('hide');

    var current = $('.add-order').find(`.order-item[data-id=${id}]`)[0];
    if (current == undefined) {
        $('.add-order').append(`<div class="cart my-1 order-item" data-id="${id}" data-price="${price}" data-qty="${quantity}">
                            <div class="d-none">
                                <input class="form-control food" value="${id}" />
                                <input class="form-control quantity" value="${quantity}"/>
                                <input class="form-control note" value="${note}"/>
                            </div>
                            <div class="cart-body p-0 bg-white" style="border-radius: 5px; overflow: hidden">
                                <div class="d-flex" style="width: 100%;">
                                    <img src="${image}" style="height: 120px; width: 120px; object-fit:cover" />
                                    <div class="pl-3" style="flex: 1">
                                        <div class="d-flex h-100">
                                            <div class="m-auto w-100">
                                                <div class="w-100">
                                                    <h6 class="mb-0">${name}</h6>
                                                </div>
                                                <span class="d-block">${priceStr}</span>
                                                <div style="color: #e49557; font-size: 0.8rem;">${note}</div>
                                                <div class="d-flex mt-2">
                                                    <div class="quantity-ip">
                                                        <span class="d-inline-block cursor-pointer option-minus">-</span>
                                                        <span class="d-inline-block qty-display">${quantity}</span>
                                                        <span class="d-inline-block cursor-pointer option-plus">+</span>
                                                    </div>
                                                    <div class="text-right pr-3" style="flex: 1">
                                                        <span class="cursor-pointer">
                                                            <img src="/images/icons/trash.png" class="trash-item" width="20" height="20" />
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>`);
    } else {
        var currentQty = $(current).data('qty');
        var qty = parseInt(quantity) + parseInt(currentQty);
        $(current).data('qty', qty);
        $(current).find('input.quantity').val(qty);
        $(current).find('.quantity-ip .qty-display').html(qty);
        $(current).data('note', note)
        $(current).find('input.note').val(note);
    }
    loadPrice();
}).on('click', '.trash-item', function (e) {
    e.preventDefault();
    var parent = $(this).parents('.order-item');
    var status = $(parent).data('status');
    if (status == undefined || status == 1)
        $(parent).remove();
    loadPrice();
}).on('click', '.btn-payment', function (e) {
    e.preventDefault();
    if ($('#modal').find('.order-dish[data-status=1]').length > 0 || $('#modal').find('.order-dish[data-status=2]').length > 0) {
        var focus = $('#modal').find('.order-dish[data-status=1]')[0];
        if (!focus)
            focus = $('#modal').find('.order-dish[data-status=2]')[0]
        var foodName = $(focus).data('name');
        $('#payment-danger-modal .message strong').html(foodName);
        $('#modal .spinner').removeClass('d-none');
        $('#payment-danger-modal').modal('show');
    } else {
        var msg = $(this).data('message');
        var href = $(this).attr('href');
        $('#confirm-message').html(msg);
        $('#confirm-accept-btn').attr('href', href);
        $('#modal .spinner').removeClass('d-none');
        $('#confirmModal').modal('show');
    }
}).on('click', '.option-minus', function (e) {
    e.preventDefault();
    var qty = $(this).parents('.order-item').find('input.quantity').val();
    var newQty = qty - 1;
    if (newQty <= 0) return;
    $(this).parents('.order-item').find('input.quantity').val(newQty);
    $(this).parents('.order-item').find('span.qty-display').html(newQty);
    $(this).parents('.order-item').data('qty', newQty);
    loadPrice();
}).on('click', '.option-plus', function (e) {
    e.preventDefault();
    var qty = $(this).parents('.order-item').find('input.quantity').val();
    var newQty = parseInt(qty) + 1;
    $(this).parents('.order-item').find('input.quantity').val(newQty);
    $(this).parents('.order-item').find('span.qty-display').html(newQty);
    $(this).parents('.order-item').data('qty', newQty);
    loadPrice();
}).on('click', '.btn-eye', function (e) {
    e.preventDefault();
    $('.pwd-i').attr('type', 'text');
    $(this).addClass('btn-close-eye').removeClass('btn-eye');
    $(this).attr('src', '/images/icons/u-eye.png')
}).on('click', '.btn-close-eye', function (e) {
    e.preventDefault();
    $('.pwd-i').attr('type', 'password');
    $(this).addClass('btn-eye').removeClass('btn-close-eye');
    $(this).attr('src', '/images/icons/eye.png');
});

$('#modal').on('hidden.bs.modal', (e) => {
    $('#modal').find('.modal-dialog').removeClass('modal-lg').removeClass('modal-xl').removeClass('.modal-md');
    $('#modal-container').html('');
});

$('#payment-danger-modal, #confirmModal').on('hidden.bs.modal', (e) => {
    $('#modal .spinner').addClass('d-none');
});

$(document).on('click', '.view-bill', function (e) {
    e.preventDefault();
    var id = $(this).data('id');
    $.ajax({
        url: '/Home/PaymentModal/' + id + '?confirm=True'
    }).done((res) => {
        $('#modal').find('.modal-dialog').addClass('modal-lg');
        $('#modal-container').html(res);
        $('#modal').modal('show');
    });
});

function loadPrice() {
    var total = 0;
    $.each($('#order-container').find('.order-item'), (i, item) => {
        var qty = $(item).data('qty');
        var price = $(item).data('price');
        total += qty * price;
    });
    $('.bill-total-price').html(total.toLocaleString('en-US'));
    if ($('.add-order').find('.order-item').length > 0)
        $('.line-new-item').removeClass('d-none');
    else
        $('.line-new-item').addClass('d-none');
}