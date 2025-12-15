$(function () {
    $.get('/api/dashboard/QualityByLocation?year=' + new Date().getFullYear(), function (data) {
        $('#location-quality').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: data.Title
            },
            xAxis: {
                categories: data.Categories
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: data.Items
        });
    });
});