$(function () {
    loadSustainability();
    loadVolume();
});
function loadVolume() {
    $('#volume').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionVolumeSupplierChain?supplierChainId=" + supplierChainId, $('#volume').highcharts());
}

function loadSustainability() {
    $('#sustainability').highcharts(lineEvolutionSettings());
    loadLineChart("/api/dashboard/EvolutionSustainabilitySupplierChain?supplierChainId=" + supplierChainId, $('#sustainability').highcharts());
}