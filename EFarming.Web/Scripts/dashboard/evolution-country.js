$(function () {
    loadSustainability();
    loadVolume();
});
function loadVolume() {
    $('#volume').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionVolumeCountry?CountryId=" + countryId, $('#volume').highcharts());
}

function loadSustainability() {
    $('#sustainability').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionSustainabilityCountry?CountryId=" + countryId, $('#sustainability').highcharts());
}