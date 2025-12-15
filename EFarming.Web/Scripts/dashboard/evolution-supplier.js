$(function () {
    loadSustainability();
    loadVolume();
});
function loadVolume() {
    $('#volume').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionVolumeSupplier?supplierId=" + supplierId, $('#volume').highcharts());
}

function loadSustainability() {
    $('#sustainability').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionSustainabilitySupplier?supplierId=" + supplierId, $('#sustainability').highcharts());
}