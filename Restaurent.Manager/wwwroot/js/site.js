$(document).on('click', '.dish-option', function (e) {
    e.preventDefault();
    $.ajax({
        url: '/Home/AddModal/1'
    }).done((res) => {
        $('#modal-container').html(res);
        $('#modal').modal('show');
    })
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

$(document).on('click', '.role-button', function (e) {
    e.preventDefault();
    $('.role-button').removeClass('active');
    $(this).addClass('active');
});

$('#modal').on('hidden.bs.modal', (e) => {
    $('#modal-container').html('');
})