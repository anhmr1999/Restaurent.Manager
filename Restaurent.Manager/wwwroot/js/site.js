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

$(document).on('click', '.payment-button', function (e) {
    e.preventDefault();
    var url = $(this).attr('href');
    $.ajax({
        url: url
    }).done((res) => {
        $('#modal').find('.modal-dialog').addClass('modal-lg');
        $('#modal-container').html(res);
        $('#modal').modal('show');
    });
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
    if (!confirm('Are you sure?'))
        e.preventDefault();
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
    if (!$('.emp-password').val()) {
        valid = false;
        $('.emp-password-error').html('Password is not empty');
    } else {
        $('.emp-password-error').html('');
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

$('#modal').on('hidden.bs.modal', (e) => {
    $('#modal').find('.modal-dialog').removeClass('modal-lg').removeClass('modal-xl').removeClass('.modal-md');
    $('#modal-container').html('');
})