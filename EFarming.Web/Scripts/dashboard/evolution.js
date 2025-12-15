$(function () {
    loadSustainability();
    loadVolume();
});

function loadVolume() {
    $('#volume').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionVolume", $('#volume').highcharts());
}

function loadSustainability() {
    $('#sustainability').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionSustainability", $('#sustainability').highcharts());
}