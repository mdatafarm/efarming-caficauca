$(function () {
    $.get('/api/dashboard/ImpactByLocation', function (data) {
        $('#location-impact').highcharts({
            chart: {
                polar: true,
                type: 'line'
            },

            title: {
                text: data.title,
                x: -80
            },

            pane: {
                size: '80%'
            },

            xAxis: {
                categories: data.categories,
                tickmarkPlacement: 'on',
                lineWidth: 0
            },

            yAxis: {
                gridLineInterpolation: 'polygon',
                lineWidth: 0,
                min: 0
            },

            tooltip: {
                shared: true,
                pointFormat: '<span style="color:{series.color}">{series.name}: <b>${point.y:,.0f}</b><br/>'
            },

            legend: {
                align: 'right',
                verticalAlign: 'top',
                y: 70,
                layout: 'vertical'
            },

            series: data.series

        });
    });
});