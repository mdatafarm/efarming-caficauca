$(function () {
    $('#clasification').highcharts(pieSettings());
    pieChart("/api/SustainabilityDashboard/ClasificationReport", $('#clasification').highcharts());
});