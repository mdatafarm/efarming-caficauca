$(function () {

    $('#UpdateLotForm').submit(function (event) {
        $('#IcoMark').val($('#year').val() + '-' + $('#Country').val() + '-' + $('#SellerCode').val() + '-' + $('#lot').val());
        $('#MerchandiseDescription').val($('#MerchandiseType').val() + '|' + $('#MerchandiseOrigin').val());
    });

    $("#start").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "yy-mm-dd",
        beforeShow: function (input, inst) {
            var rect = input.getBoundingClientRect();
            setTimeout(function () {
                inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
            }, 0);
        }
    });
});