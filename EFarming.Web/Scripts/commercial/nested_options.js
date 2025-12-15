$(function () {

    function FillValues() {
        $('#AgentId').empty();
        $.get('/api/Commercial/Index?ClientId=' + $('#ClientId').val(), function (data, status) {
            $.each(data, function (key, value) {
                $('#AgentId').append($('<option>').text(value[1]).attr('value', value[0]));
            });
        });

        $.get('/api/Commercial/MoreInformation?ClientId=' + $('#ClientId').val(), function (data, status) {
            if ($('#ClientId option:selected').text() == 'Nestlé Nespresso S.A.') {
                $('#Terms').empty();
                $('#Terms1').empty();
                $('#Port').empty();
                document.getElementById('TermsNespresso').style.display = 'block'
                document.getElementById('TermsNespresso1').style.display = 'block'
                $.each(data, function (key, value) {
                    if (value.InformationType == "Terms") {
                        $('#Terms1').append($('<option>').text(value.Text).attr('value', value.Text));
                    };
                    if (value.InformationType == "Port") {
                        $('#Port').append($('<option>').text(value.Text).attr('value', value.Text));
                        $('#Terms').empty();
                        if ($('#Terms1').val() !== null) {
                            $('#Terms').append($('<option>').text($('#Terms1').val() + ' ' + $('#Port').val()).attr('value', $('#Terms1').val() + ' ' + $('#Port').val()));
                        } else {
                            $('#Terms').append($('<option>').text('FOB' + ' ' + $('#Port').val()).attr('value', 'FOB' + ' ' + $('#Port').val()));
                        }
                    };
                });
            } else {
                $('#Terms1').empty();
                $('#Port').empty();
                $('#Terms').empty();
                document.getElementById('TermsNespresso').style.display = 'none'
                document.getElementById('TermsNespresso1').style.display = 'none'
                $.each(data, function (key, value) {
                    if (value.InformationType == "Terms") {
                        $('#Terms').append($('<option>').text(value.Text).attr('value', value.Text));
                    };
                });
            };

            $('#Weights').empty();
            $('#Payment').empty();
            $('#Samples').empty();
            $('#Arbitration').empty();
            $('#OthersS').empty();
            $('#Quality').empty();
            $.each(data, function (key, value) {
                if (value.InformationType == "Quality") {
                    console.log(value);
                    $('#Quality').append($('<option>').text(value.Short).attr('value', value.Text));
                };
                if (value.InformationType == "Weights") {
                    $('#Weights').append($('<option>').text(value.Text).attr('value', value.Text));
                };
                if (value.InformationType == "Payments") {
                    $('#Payment').append($('<option>').text(value.Text).attr('value', value.Text));
                };
                if (value.InformationType == "Samples") {
                    $('#Samples').append($('<option>').text(value.Text).attr('value', value.Text));
                };
                if (value.InformationType == "Arbitration") {
                    $('#Arbitration').append($('<option>').text(value.Text).attr('value', value.Text));
                };
                if (value.InformationType == "Others") {
                    $('#OthersS').append($('<option>').text(value.Text).attr('value', value.Text));
                };
            });
        });
    };

    FillValues();

    $('#ClientId').on('change', function () {
        FillValues();
    });

    $('#Quality').on('change', function () {
        FTQuality = $('#Quality option:selected').text().indexOf("FT");
        if (FTQuality > -1) {
            $('#SellerId').find('option[value="9fd591fb-d3e4-49c8-a7ee-458472aeccb7"]').prop("selected", true);
        } else {
            $('#SellerId').find('option[value="73d2c77d-4915-4390-95b1-c033ba50480a"]').prop("selected", true);
        }
    });

    $('#Terms1').on('change', function () {
        $('#Terms').empty();
        $('#Terms').append($('<option>').text($('#Terms1').val() + ' ' + $('#Port').val()).attr('value', $('#Terms1').val() + ' ' + $('#Port').val()));
    });

    $('#Port').on('change', function () {
        $('#Terms').empty();
        $('#Terms').append($('<option>').text($('#Terms1').val() + ' ' + $('#Port').val()).attr('value', $('#Terms1').val() + ' ' + $('#Port').val()));
    });

    $('#PriceType').on('change', function () {
        if ($('#PriceType').val() == "Outright") {
            $('#Fixation').prop("disabled", false);
        } else {
            $('#Fixation').val("");
            $('#Fixation').prop("disabled", true);
        };
    });

    $('#Volume').on('input', function (e) {
        $('#LotsNumber').val(Math.round($('#Volume').val() / 243));
    });

    $('#OthersS').on('change', function () {
        $('#Others').val($('#OthersS').val());
    });

    $("#ShipmentDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "yy-mm-dd",
        beforeShow: function (input, inst) {
            var rect = input.getBoundingClientRect();
            setTimeout(function () {
                inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
            }, 0);
        }
    });

    
    $('#ShipmentDate').on('change', function (e) {
        
        switch ($('#ShipmentDate').val().substring(5, 7)) {
            case '12':
                NYPosition = "KCH(" + ($('#ShipmentDate').val().substring(2, 4) *1.0 + 1).toString() + ")";
                break;
            case '01':
            case '02':
                NYPosition = "KCH(" + ($('#ShipmentDate').val().substring(2, 4)).toString() + ")";
                break;
            case '03':
            case '04':
                NYPosition = "KCK(" + ($('#ShipmentDate').val().substring(2, 4)).toString() + ")";
                break;
            case '05':
            case '06':
                NYPosition = "KCN(" + ($('#ShipmentDate').val().substring(2, 4)).toString() + ")";
                break;
            case '07':
            case '08':
                NYPosition = "KCU(" + ($('#ShipmentDate').val().substring(2, 4)).toString() + ")";
                break;
            case '09':
            case '10':
            case '11':
                NYPosition = "KCZ(" + ($('#ShipmentDate').val().substring(2, 4)).toString() + ")";
                break;
        };
        $('#PriceDate').val(NYPosition);
        //switch ($('#ShipmentDate').val().substring(5, 7)) {
        //    case '01':
        //    case '02':
        //    case '03':
        //    case '04':
        //    case '05':
        //    case '06':
        //    case '07':
        //    case '08':
        //    case '09':
        //        Crop = "Crop(" + (Math.round(($('#ShipmentDate').val().substring(0, 4) - 1) / ($('#ShipmentDate').val().substring(0, 4)))).toString() + ")";
        //        break;
        //    case '10':
        //    case '11':
        //    case '12':
        //        Crop = "Crop(" + (Math.round((($('#ShipmentDate').val().substring(0, 4)) / (parseInt($('#ShipmentDate').val().substring(0, 4)) + 1)))).toString() + ")";
        //        break;
        //};
        //alert(Crop);
    });
});