$(function () {
    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewFarmsSupplier?supplierId=" + supplierId, $('#farms-per-location').highcharts());

    $('#volume').highcharts(columnSettings());
    pieChart("/api/ComertialDashboard/OverviewSalesVolume?type=Supplier&id=" + supplierId + '&startDate=' + startDate + '&endDate=' + endDate, $('#volume').highcharts());

    $('#workers').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewWorkersSupplier?supplierId=" + supplierId, $('#workers').highcharts());

    $('#ownershipTypes').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewOwnershipTypesSupplier?supplierId=" + supplierId, $('#ownershipTypes').highcharts());

    $('#size').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/ClasificationReportBy?type=Supplier&id=" + supplierId + '&startDate=' + startDate + '&endDate=' + endDate, $('#size').highcharts());

    $('#varieties').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/DefectsReportBy?type=Supplier&id=" + supplierId + '&startDate=' + startDate + '&endDate=' + endDate, $('#varieties').highcharts());
});