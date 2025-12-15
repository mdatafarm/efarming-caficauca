$(function () {
    $.ajax({
        datatype: 'JSON',
        url: '/api/admin',
        type: 'GET'
    }).done(function (data) {
        $("#pending-users").text(data.PendingUsers);
    });
});