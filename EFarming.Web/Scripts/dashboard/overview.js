$(function () {    
    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewFarms", $('#farms-per-location').highcharts());

    $('#volume').highcharts(columnSettings());
    pieChart("/api/ComertialDashboard/OverviewSalesVolume?startDate=" + startDate + '&endDate=' + endDate, $('#volume').highcharts());

    $('#clasification').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/ClasificationReportOverview?startDate=" + startDate + '&endDate=' + endDate, $('#clasification').highcharts());

    $('#defects').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/DefectsReportOverview?startDate=" + startDate + '&endDate=' + endDate, $('#defects').highcharts());
});
