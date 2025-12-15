function gaugeSettings() {
    return {

        chart: {
            height: 200,
            type: 'solidgauge'
        },

        title: null,

        pane: {
            size: '80%',
            startAngle: -90,
            endAngle: 90,
            background: {
                backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
                innerRadius: '60%',
                outerRadius: '100%',
                shape: 'arc'
            }
        },

        tooltip: {
            enabled: false
        },

        // the value axis
        yAxis: {
            stops: [
                [0.1, '#55BF3B'], // green
                [0.5, '#DDDF0D'], // yellow
                [0.9, '#DF5353'] // red
            ],
            lineWidth: 0,
            minorTickInterval: null,
            tickPixelInterval: 400,
            tickWidth: 0,
            title: {
                y: -70
            },
            labels: {
                y: 16
            }
        },

        plotOptions: {
            solidgauge: {
                dataLabels: {
                    y: 5,
                    borderWidth: 0,
                    useHTML: true
                }
            }
        }
    };
}

function spiderWebSettings() {
    return {
        chart: {
            polar: true,
            type: 'line'
        },
        title: {
            text: ''
        },
        pane: {
            size: '100%'
        },
        xAxis: {
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
        }
    }
};

function lineEvolutionSettings() {
    return {
        title: {
            text: '',
        },
        pane: {
            size: '100%'
        },
        xAxis: {
        },
        yAxis: {
            title: {
                text: ''
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            shared: true,
            pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y}</b><br/>'
        },
        legend: {
            align: 'right',
            verticalAlign: 'top',
            y: 70,
            layout: 'vertical'
        }
    }
}

function pieSettings() {
    return {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: ""
        },
        tooltip: {
            pointFormat: '{series.name}: <i>{point.percentage:.1f}%</i>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: "{point.percentage:.1f}%"
                },
                showInLegend: true
            }
        }
    };
}

function pieDrillDownSettings(urlToDrill, urlDrill) {

    var chart = {};

    chart = {
        chart: {
            type: 'pie',
            events: {
                drilldown: function (e) {
                    if (!e.seriesOptions) {
                        var chart = this;
                        chart.showLoading('Loading ...');
                        $.get(urlDrill + '&Type=' + e.point.name, function (data) {
                            chart.hideLoading();
                            chart.addSeriesAsDrilldown(e.point, data.Items[0]);
                        });
                    }
                }
            }
        },
        title: {
            text: 'Contacts by Type'
        },
        xAxis: {
            type: 'category'
        },

        legend: {
            enabled: false
        },

        plotOptions: {
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                }
            }
        },

        drilldown: {
            series: []
        }
    };
    return chart
    
}

function columnSettings() {
    return {
        chart: {
            type: 'column'
        },
        title: {
            text: ''
        },
        xAxis: {
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
                text: ''
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#000000',
                    align: 'left',
                    x: 4,
                    y: 0,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }
        }
    };
}

function stackedColumnSettings() {
    return {
        chart: {
            type: 'column'
        },
        title: {
            text: ''
        },
        xAxis: {
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
                text: ''
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                stacking: 'percent',
                pointPadding: 0.2,
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#000000',
                    align: 'left',
                    x: 4,
                    y: 0,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }
        }
    };
}

function pieChart(url, chart) {
    chart.showLoading();
    $.get(url, function (result) {
        var cat = result.Categories;
        chart.setTitle({ text: result.Title });
        result.Items.forEach(function (serie) {
            chart.addSeries(serie);
        })
        chart.xAxis[0].setCategories(result.Categories);
        chart.hideLoading();
    });
}

function drilldownChart(url, chart) {
    chart.showLoading();
    $.get(url, function (result) {
        var cat = result.Categories;
        chart.setTitle({ text: result.Title });
        result.forEach(function (serie) {
            chart.addSeries(serie);
        })
        chart.xAxis[0].setCategories(result.Categories);
        chart.hideLoading();
    });
}

function spiderWebChart(url, chart) {
    chart.showLoading();
    $.get(url, function (result) {
        console.log("el result es:" + JSON.stringify(result))
        var cat = result.Categories;
        //chart.setTitle({ text: result.Title });
        result.Series.forEach(function (serie) {
            chart.addSeries(serie);
        })
        chart.xAxis[0].setCategories(result.Categories);
        chart.hideLoading();
    });
}

function loadLineChart(url, chart) {
    chart.showLoading();
    $.get(url, function (result) {
        console.log(result);
        var cat = result.Categories;
        chart.setTitle({ text: result.Title });
        result.Items.forEach(function (serie) {
            chart.addSeries(serie);
        })
        chart.xAxis[0].setCategories(result.Categories);
        chart.yAxis[0].setTitle({ text: result.YTitle });
        chart.hideLoading();
    });
}

function columnChart(url, chart) {
    chart.showLoading();
    $.get(url, function (result) {
        var cat = result.Categories;
        chart.setTitle({ text: result.Title });
        result.Items.forEach(function (serie) {
            chart.addSeries(serie);
        })
        chart.xAxis[0].setCategories(result.Categories);
        chart.yAxis[0].setTitle({ text: result.YTitle });
        chart.hideLoading();
    });
}

function drawMap(url, canvas) {
    var map = new google.maps.Map(document.getElementById(canvas), {
        zoom: 10,
        //center: new google.maps.LatLng(-33.92, 151.25),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var infowindow = new google.maps.InfoWindow();

    var marker, i;
    $.get(url, function (result) {
        for (i = 0; i < result.length; i++) {
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(result[i][1], result[i][2]),
                map: map
            });

            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    infowindow.setContent(result[i][0]);
                    infowindow.open(map, marker);
                }
            })(marker, i));
        }
    });
}