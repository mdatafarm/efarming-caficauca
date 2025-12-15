$(function () {
    var url = '/api/dashboard/InvoicesByFarm?farmId=' + farmid + '&start=' + start + "&end=" + end + "&lotId=" + lotid;
    $.get(url, function (data) {
        $('#weight-chart-container').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: data[0].Title
            },
            xAxis: {
                categories: data[0].Categories,
                type: 'category',
                labels: {
                    rotation: -45,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: data[0].YTitle
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} Kg</b></td></tr>',
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
            series: data[0].Items
        });

        $('#price-chart-container').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: data[1].Title
            },
            xAxis: {
                categories: data[1].Categories,
                type: 'category',
                labels: {
                    rotation: -45,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: data[1].YTitle
                }
            }, tooltip: {
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
            series: data[1].Items
        });
    });
});