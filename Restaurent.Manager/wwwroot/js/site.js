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

$(document).on('click', '.btn-add-modal', function (e) {
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
});

$('#modal').on('hidden.bs.modal', (e) => {
    $('#modal').find('.modal-dialog').removeClass('modal-lg').removeClass('modal-xl').removeClass('.modal-md');
    $('#modal-container').html('');
})