$(function () {
    $.get('/api/dashboard/TrackImpactByLocation?year='+ new Date().getFullYear(), function (data) {
        $('#resume-impact').highcharts({
            chart: {
                type: 'line'
            },
            title: {
                text: data.title
            },
            xAxis: {
                categories: data.categories
            },
            yAxis: {
                title: {
                    text: ''
                }
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: true
                }
            },
            series: data.series
        });
    });
});