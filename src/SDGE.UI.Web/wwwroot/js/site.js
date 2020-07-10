// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//modal

$(document).ready(function () {
    $('#dtMaterialDesignExample').DataTable();
});

/*$(document).ready(function () {

    setTimeout(function () { $("#dialog").fadeIn(); }, 10000)

});

$('#criar').click(function (event) {
    $("#dialog").fadeIn();
}*/

$(function () {
    var placeholderElement = $('#modalView');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
            //setTimeout(function () { placeholderElement.find('.modal').modal('hide'); }, 3000);
        });
       // setTimeout(function () { placeholderElement.find('.modal').modal('hide'); }, 30);
       
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            //var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            //if (isValid) {
                placeholderElement.find('.modal').modal('hide');
            //}
        });
    });

});




