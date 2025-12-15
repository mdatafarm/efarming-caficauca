var reflow = false;
var maploaded = false;
$(function () {
    //$('#evolution').highcharts(lineEvolutionSettings());
    //loadLineChart("/api/dashboard/FarmEvolution?id=" + farmId, $('#evolution').highcharts());

    //$.get("/api/dashboard/FarmVolumeEvolution?id=" + farmId, function (result) {
    //    $('#volume').highcharts({
    //        title: {
    //            text: result.Title,
    //            x: -80
    //        },

    //        pane: {
    //            size: '80%'
    //        },

    //        xAxis: {
    //            categories: result.Categories
    //        },

    //        yAxis: {
    //            title: {
    //                text: ''
    //            },
    //            plotLines: [{
    //                value: 0,
    //                width: 1,
    //                color: '#808080'
    //            }]
    //        },

    //        tooltip: {
    //            shared: true,
    //            pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
    //        },

    //        legend: {
    //            align: 'right',
    //            verticalAlign: 'top',
    //            y: 70,
    //            layout: 'vertical'
    //        },

    //        series: result.Items

    //    });
    //});

    //$('#sustainability').highcharts(pieSettings());
    //loadLineChart("/api/dashboard/FarmOverview?id=" + farmId, $('#sustainability').highcharts());

    $.get("/api/dashboard/SalesByYear?farmId=" + farmId, function (result) {
        $('#workers').highcharts({
            chart: {
                type: 'line'
            },
            title: {
                text: 'Sales by year (kg)'
            },
            xAxis: {
                categories: result.Year
            },
            yAxis: {
                title: {
                    text: 'kg'
                }
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: [{
                name: 'Sales (kg)',
                data: result.Value
            }]

        });
    });

    $.get("/api/dashboard/SalesByHaByYear?farmId=" + farmId, function (result) {
        $('#salesByHa').highcharts({
            chart: {
                type: 'line'
            },
            title: {
                text: 'Sales by Ha by year'
            },
            xAxis: {
                categories: result.Year
            },
            yAxis: {
                title: {
                    text: 'kg'
                }
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: [{
                name: 'Sales (kg)',
                data: result.Value
            }]

        });
    });

    $.get("/api/dashboard/FertilizersByYear?farmId=" + farmId, function (result) {
        $('#FertilizersByYear').highcharts({
            chart: {
                type: 'line'
            },
            title: {
                text: 'Fertilizers by year (bags)'
            },
            xAxis: {
                categories: result.Year
            },
            yAxis: {
                title: {
                    text: 'bags'
                }
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: [{
                name: 'Fettilizers (bags)',
                data: result.Value
            }]

        });
    });

    $.get("/api/dashboard/FetilizersByHaByYear?farmId=" + farmId, function (result) {
        $('#FertilizersByHaByYear').highcharts({
            chart: {
                type: 'line'
            },
            title: {
                text: 'Fertilizers by Ha by Year'
            },
            xAxis: {
                categories: result.Year
            },
            yAxis: {
                title: {
                    text: 'bags'
                }
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: [{
                name: 'Fertilizers (bags)',
                data: result.Value
            }]

        });
    });

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        if (e.target.id == "chart-tab") {
            if (!reflow) {
                reflow = true;
                $('.charts').each(function (idx) {
                    chart = $(this).highcharts();
                    chart.reflow();
                });


            }
        }
        else if (e.target.id == "location-tab") {
            if (!maploaded) {
                loadMap();
                maploaded = true;
            }
        }
    });
});

function loadMap() {
    var coords = new google.maps.LatLng(latitude, longitude);
    var options = {
        zoom: 13,
        center: coords,
        navigationControlOptions: {
            style: google.maps.NavigationControlStyle.SMALL
        },
        mapTypeId: google.maps.MapTypeId.HYBRID,
    };

    var map = new google.maps.Map(document.getElementById("map-canvas"), options);

    var marker = new google.maps.Marker({
        position: coords,
        map: map
    });
}