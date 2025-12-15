$(function () {
    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/ComertialDashboard/NumberofFarms?type=Cooperative&id=" + cooperativeId, $('#farms-per-location').highcharts());

    $('#volume').highcharts(columnSettings());
    columnChart("/api/ComertialDashboard/OverviewSalesVolume?type=Cooperative&id=" + cooperativeId + '&startDate=' + startDate + '&endDate=' + endDate, $('#volume').highcharts());

    $('#workers').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewWorkersCooperative?cooperativeId=" + cooperativeId, $('#workers').highcharts());

    $('#ownershipTypes').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewOwnershipTypeCooperative?cooperativeId=" + cooperativeId, $('#ownershipTypes').highcharts());

    $('#size').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/OverviewClasificationReportByCooperative?cooperative=" + cooperativeId + '&startDate=' + startDate + '&endDate=' + endDate, $('#size').highcharts());

    $('#varieties').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/OverviewDefectsReportByCooperative?cooperative=" + cooperativeId + '&startDate=' + startDate + '&endDate=' + endDate, $('#varieties').highcharts());
});