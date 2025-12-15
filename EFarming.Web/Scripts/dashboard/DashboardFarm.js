function BasicColumns(url, component) {
    //console.log(component+"url en basic columns1"+url);
    $('#' + component).loading({
        stoppable: true
    });
    $.get(url, function (result) {
       // console.log("url en basic columns2" + url);
        var objJSON = JSON.parse(result);
        Highcharts.chart(component, objJSON);
        $('#' + component).loading('stop');
    });
} 