$(function () {

    $(".dateField").each(function () {
        $(this).datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "yy-mm-dd",
            /*Configuracion para mostrar la fecha abajo del textbox*/
            beforeShow: function (input, inst) {
                var rect = input.getBoundingClientRect();
                setTimeout(function () {
                    inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
                }, 0);
            }
        });
    })

    $('#bytype').highcharts(pieSettings());
    pieChart("/api/SustainabilityDashboard/ContactsReport?CooperativeId=" + CooperativeId + "&startDate=" + startDate + "&endDate=" + endDate, $('#bytype').highcharts());

    $('#byuser').highcharts(pieSettings());
    pieChart("/api/SustainabilityDashboard/ContactsByUser?CooperativeId=" + CooperativeId + "&startDate=" + startDate + "&endDate=" + endDate, $('#byuser').highcharts());

    $('#drillDown').highcharts(pieDrillDownSettings("/api/SustainabilityDashboard/ContactsToDrillDown?CooperativeId=" + CooperativeId + "&startDate=" + startDate + "&endDate=" + endDate, "/api/SustainabilityDashboard/ContactsDrillDown?CooperativeId=" + CooperativeId + "&startDate=" + startDate + "&endDate=" + endDate));
    drilldownChart("/api/SustainabilityDashboard/ContactsToDrillDown?CooperativeId=" + CooperativeId + "&startDate=" + startDate + "&endDate=" + endDate, $('#drillDown').highcharts());
});