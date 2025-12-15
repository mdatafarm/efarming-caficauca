$(function () {

    $('.unit').on('input', function (e) {
        //$(this).closest("form").find(".LotsNumber").text(Math.round($(this).closest("form").find("#Volume").text() / $(this).val()));
        $(this).closest("form").find(".LotsNumberInput").val(Math.round($(this).closest("form").find("#Volume").text() / $(this).val()));
    });

});