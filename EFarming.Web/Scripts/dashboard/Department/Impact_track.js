$(function () {
    $.get('/api/dashboard/TrackImpactByDepartment?year=' + new Date().getFullYear() + "&departmentId=" + departmentId, function (data) {
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