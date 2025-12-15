$(function () {    
    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/ComertialDashboard/ComertialSales1", $('#farms-per-location').highcharts());

    $('#clasification').highcharts(pieSettings());
    pieChart("/api/ComertialDashboard/ComertialSales2", $('#clasification').highcharts());

    $('#Sale3').highcharts(columnSettings());
    columnChart("/api/Comertialdashboard/ComertialSales3", $('#Sale3').highcharts());


});