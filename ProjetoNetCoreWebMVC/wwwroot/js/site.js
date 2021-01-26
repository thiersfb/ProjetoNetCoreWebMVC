// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(function () {
    var placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var id = $(this).data('id');

        if (id) {
            url = url + '/' + id;
        }

        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
                
                var urlRedirect = "";
                if (actionUrl.indexOf("Departments") != -1) { //indexOf verifica se contém a string informada
                    urlRedirect = '/Departments/Index';
                }

                if (actionUrl.indexOf("Sellers") != -1) {
                    urlRedirect = '/Sellers/Index';
                }

                window.location.href = urlRedirect;
            }
        });
    });
});
