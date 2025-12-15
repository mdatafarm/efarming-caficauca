$(function () {
    $.get("/api/dashboard/OverviewSustainability?CountryId=" + countryId, function (result) {
        $('#sustainability').highcharts({

            chart: {
                polar: true,
                type: 'line'
            },

            title: {
                text: result.Title,
                x: -80
            },

            pane: {
                size: '80%'
            },

            xAxis: {
                categories: result.Categories,
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
                pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
            },

            legend: {
                align: 'right',
                verticalAlign: 'top',
                y: 70,
                layout: 'vertical'
            },

            series: result.Items

        });
    });
});