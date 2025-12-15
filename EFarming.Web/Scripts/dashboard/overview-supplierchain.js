$(function () {
    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/ComertialDashboard/NumberofFarms?type=SupplyChain&id=" + supplierChainId, $('#farms-per-location').highcharts());

    $('#volume').highcharts(columnSettings());
    columnChart("/api/ComertialDashboard/OverviewSalesVolume?type=SupplierChain&id=" + supplierChainId + '&startDate=' + startDate + '&endDate=' + endDate, $('#volume').highcharts());

    $('#workers').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewWorkersSupplierChain?supplierChainId=" + supplierChainId, $('#workers').highcharts());

    $('#ownershipTypes').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewOwnershipTypesSupplierChain?supplierChainId=" + supplierChainId, $('#ownershipTypes').highcharts());

    $('#size').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/ClasificationReportOverview?startDate=" + startDate + '&endDate=' + endDate, $('#size').highcharts());

    $('#varieties').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/DefectsReportOverview?startDate=" + startDate + '&endDate=' + endDate, $('#varieties').highcharts());
});