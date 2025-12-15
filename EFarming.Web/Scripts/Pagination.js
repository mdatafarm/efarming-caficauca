function paginate() {
    $("a[data-ajax=true]").click(function (e) {
        e.preventDefault();
        var target = $(this).attr("data-ajax-update");
        var url = $(this).attr("href");
        $.get(url, function (data) {
            $(target).html(data);
            paginateRemote(target);
        });
    })
}

function paginateRemote(selector) {
    $(selector + " a[data-ajax=true]").click(function (e) {
        e.preventDefault();
        var target = $(this).attr("data-ajax-update");
        var url = $(this).attr("href");
        $.get(url, function (data) {
            $(target).html(data);
            paginateRemote(target);
        });
    })
}

$(function () {
    paginate();
});