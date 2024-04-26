$(document).on('click', '.dish-option', function (e) {
    e.preventDefault();
    $.ajax({
        url: '/Home/AddModal/1'
    }).done((res) => {
        $('#modal-container').html(res);
        $('#modal').modal('show');
    })
})