$(function () {
    //$('#VolumeByQuality').highcharts(columnSettings());
    //pieChart("/api/CommercialContracts/VolumeByQuality", $('#VolumeByQuality').highcharts());
    $(function () {
        var url = '/api/CommercialContracts/VolumeByQualityByClient?ClientId=' + ClientId;
        $.get(url, function (data) {
            console.log(data)
            console.log(data[0])
            $('#VolumeByQuality').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: data.Title
                },
                xAxis: {
                    categories: data.Categories,
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
                        text: data.YTitle
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false
                },
                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        //dataLabels: {
                        //    enabled: true,
                        //    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                        //    style: {
                        //        textShadow: '0 0 3px black'
                        //    }
                        //},
                        //pointPadding: 0.2,
                        //borderWidth: 0
                    }
                },
                series: data.Items
            });
        });
    });
});