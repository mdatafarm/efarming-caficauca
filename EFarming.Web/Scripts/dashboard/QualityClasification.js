$(function () {
    console.log($("#start").val() + ' ' + $("#end").val())
    $('#clasification').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/DefectsReport?CooperativeId=''&startDate=" + $("#start").val() + "&endDate=" + $("#end").val(), $('#clasification').highcharts());

    $('#ByType').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/AssessmentType?CooperativeId=''&startDate=" + $("#start").val() + "&endDate=" + $("#end").val(), $('#ByType').highcharts());

    $('#QDA').highcharts(spiderWebSettings());
    spiderWebChart("/api/qualitydashboard/QDA?CooperativeId=''&startDate=" + $("#start").val() + "&endDate=" + $("#end").val(), $('#QDA').highcharts());

    $('#farms-per-location').highcharts(pieSettings());
    pieChart("/api/qualitydashboard/ClasificationReport?CooperativeId=''&startDate=" + $("#start").val() + "&endDate=" + $("#end").val(), $('#farms-per-location').highcharts());

    //$('#decision').highcharts(pieSettings());
    //pieChart("/api/qualitydashboard/DecisionReport?startDate=" + $("#start").val() + "&endDate=" + $("#end").val(), $('#decision').highcharts());

});