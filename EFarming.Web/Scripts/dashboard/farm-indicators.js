$('#density-indicator').highcharts(Highcharts.merge(gaugeSettings(), {
    yAxis: {
        stops: [
            [0.45, '#DF5353'], // red
            [0.55, '#DDDF0D'], // yellow
            [0.7, '#55BF3B'], // green
            [0.85, '#DDDF0D'], // yellow
            [1, '#DF5353'] // red
        ],
        min: 0,
        max: 10000
    },

    credits: {
        enabled: false
    },

    series: [{
        name: 'Density',
        data: [density_value]
    }]

}));

$('#productivity-indicator').highcharts(Highcharts.merge(gaugeSettings(), {
    yAxis: {
        stops: [
            [0.156, '#DF5353'], // red
            [0.216, '#FF9900'], // orange
            [0.3, '#DDDF0D'], // yellow
            [0.6, '#55BF3B'], // green
            [1, '#55BF3B'] // green
        ],
        min: 0,
        max: 500
    },

    credits: {
        enabled: false
    },

    series: [{
        name: 'Productivity',
        data: [productivity_value]
    }]

}));

$('#fertilizer-indicator').highcharts(Highcharts.merge(gaugeSettings(), {
    yAxis: {
        stops: [
            [0.34, '#DF5353'], // red
            [0.48, '#55BF3B'], // green
            [1, '#DF5353'] // red
        ],
        min: 0,
        max: 500
    },

    credits: {
        enabled: false
    },

    series: [{
        name: 'Fertilizer',
        data: [fertilizer_value]
    }]

}));

$('#age-indicator').highcharts(Highcharts.merge(gaugeSettings(), {
    yAxis: {
        stops: [
            [1, '#55BF3B'] // green
        ],
        min: 0,
        max: 100
    },

    credits: {
        enabled: false
    },

    series: [{
        name: 'Age',
        data: [age_value]
    }]

}));