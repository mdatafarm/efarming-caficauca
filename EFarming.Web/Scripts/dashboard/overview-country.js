$(function () {
    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/dashboard/OverviewFarmsCountry?CountryId=" + countryId, $('#farms-per-location').highcharts());

    $('#volume').highcharts(columnSettings());
    pieChart("/api/ComertialDashboard/OverviewSalesVolume?type=Country&id=" + countryId + '&startDate=' + startDate + '&endDate=' + endDate, $('#volume').highcharts());

    $('#size').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/ClasificationReportBy?type=Country&id=" + countryId + '&startDate=' + startDate + '&endDate=' + endDate, $('#size').highcharts());

    $('#varieties').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/DefectsReportBy?type=Country&id=" + countryId + '&startDate=' + startDate + '&endDate=' + endDate, $('#varieties').highcharts());
});