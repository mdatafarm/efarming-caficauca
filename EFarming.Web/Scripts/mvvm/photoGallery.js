$.ajaxSetup({ cache: false });

$(function () {
    'use strict';
    var url = '/PhotoGallery/upload';
    // Initialize the jQuery File Upload widget:
    var form = $('<form id="fileupload" action="/PhotoGallery/create" method="POST" enctype="multipart/form-data" />');
    var html = $('#upload-content').html();
    $("#upload-content").empty();
    form.append(html);
    $("#upload-content").append(form);

    $("#fileupload").fileupload({
        dataType: 'json',
        url: url
    });

    //$("#fileupload").bind('fileuploadprogress', function (e, data) {
    //    var percent = (data.progress().loaded / data.progress().total) * 100;

    //    $("#upload-progress").attr("aria-now", percent);
    //    $("#upload-progress").width(percent + "%");
    //});

    $.ajax({
        url: url,
        dataType: 'json',
        data:{
            farmId: farmId
        },
        context: $('#fileupload')[0]
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) {
        $(this).fileupload('option', 'done')
            .call(this, null, { result: result });
    });

    $(".files").on("click", ".delete-image", function (e) {
        var delete_url = $(this).prop("href");
        var row = $(this).parent().parent();
        $.ajax({
            type: "POST",
            url: delete_url,
            dataType: 'json',
            data: {
                farmId: farmId
            },
            context: $('#fileupload')[0]
        }).always(function () {
            $(this).removeClass('fileupload-processing');
        }).done(function (result) {
            row.remove();
        });
        e.preventDefault();
    });

    $(".files").on("click", ".set-principal-image", function (e) {
        var set_principal_url = $(this).prop("href");
        var row = $(this).parent().parent();
        $.ajax({
            type: "PUT",
            url: set_principal_url,
            dataType: 'json',
            data: {
                farmId: farmId
            },
            context: $('#fileupload')[0]
        }).always(function () {
            $(this).removeClass('fileupload-processing');
        }).done(function (result) {
            row.remove();
        });
        e.preventDefault();
    });
});