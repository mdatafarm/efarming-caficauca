$(function () {
    $('#clasification').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/ClasificationReportByCooperative?cooperative=" + cooperative, $('#clasification').highcharts());

    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/DefectsReportByCooperative?cooperative=" + cooperative, $('#farms-per-location').highcharts());
});