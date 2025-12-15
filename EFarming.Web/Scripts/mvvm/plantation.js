$(function () {



    function PlantationViewModel() {
        self = this;
        self.hectares = ko.observable();
        self.estimatedProduction = ko.observable();
        self.age = ko.observable();
        self.numberOfPlants = ko.observable();
        self.plantationStatus = ko.observable(selectedPlantationStatus);
        self.plantationVariety = ko.observable(selectedPlantatonVariety);
        self.plantationType = ko.observable(selectedPlantationType);
        self.plantationStatuses = ko.observableArray();
        self.plantationTypes = ko.observableArray();
        self.plantationVarieties = ko.observableArray();

        self.getPlantationVarieties = function () {
            $.ajax({
                url: '/api/plantationvarieties/',
                type: 'GET',
                dataType: 'json',
                async: false,
                data: {
                    plantationTypeId: self.plantationType()
                }
            }).done(function (data) {
                self.plantationVarieties([]);
                $.map(data, function (type, i) {
                    self.plantationVarieties.push(new SelectOption(type.Name, type.Id));
                });
            });
        };

        self.getPlantationTypes = function () {
            $.ajax({
                url: '/api/plantationtypes/',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.plantationTypes([]);
                $.map(data, function (type, i) {
                    self.plantationTypes.push(new SelectOption(type.Name, type.Id));
                });
                if (typeof selectedPlantationType !== 'undefined' && selectedPlantationType !== '') {
                    self.getPlantationVarieties();
                }
            });
        };

        self.getPlantations = function () {
            $.ajax({
                url: '/api/plantationstatuses/',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.plantationStatuses([]);
                $.map(data, function (status, i) {
                    self.plantationStatuses.push(new SelectOption(status.Name, status.Id));
                });
            });
        };

        self.getPlantations();
        self.getPlantationTypes();
    }


    pvm = new PlantationViewModel();
    p = document.getElementById("plantation-form");

    ko.cleanNode(p);
    ko.applyBindings(pvm, p);

});

$(".dateField").each(function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        /*Configuracion para mostrar la fecha abajo del textbox*/
        beforeShow: function (input, inst) {
            var rect = input.getBoundingClientRect();
            setTimeout(function () {
                inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
            }, 0);
        }
    })
});

//$(document).on('keyup', '#TreesDistance', function (e) {
//    $('#Density').val(parseFloat(10000 / parseFloat($('#TreesDistance').val().replace(",", ".") * $('#GrooveDistance').val().replace(",", "."))).toFixed(2));
//    $('#NumberOfPlants').val(Math.round($('#Hectares').val().replace(",", ".") * $('#Density').val().replace(",", ".")));
//});

//$(document).on('keyup', '#GrooveDistance', function (e) {
//    $('#Density').val(parseFloat(10000 / parseFloat($('#TreesDistance').val().replace(",", ".") * $('#GrooveDistance').val().replace(",", "."))).toFixed(2));
//    $('#NumberOfPlants').val(Math.round($('#Hectares').val().replace(",", ".") * $('#Density').val().replace(",", ".")));
//});

//$(document).on('keyup', '#Hectares', function (e) {
//   $('#NumberOfPlants').val(Math.round($('#Hectares').val().replace(",", ".") * $('#Density').val().replace(",", ".")));
//});



$(document).ready(function () {

    if ($('#PlantationTypeId').val() == "d221bec9-5f73-43a0-9ebf-16417f5674f5" || $('#PlantationTypeId').val() == "") {

    }


    else {
        $('#NumberLot').attr('readonly', true);

        $('#EstimatedProduction').val(0);
        $('#EstimatedProduction').attr('readonly', true);

        $('#TreesDistance').val(0);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(0);
        $('#GrooveDistance').attr('readonly', true);

        if (!$('#Density').val() || $('#Density').val() == 0) {
            $('#Density').val(0);
        }
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(0);
        $('#NumberOfPlants').attr('readonly', true);

    }
});

$(document).on('change', '#PlantationTypeId', function (e) {

    if ($('#PlantationTypeId').val() == "d221bec9-5f73-43a0-9ebf-16417f5674f5" || $('#PlantationTypeId').val() == "") {

        $('#EstimatedProduction').attr('readonly', false);

        $('#TreesDistance').attr('readonly', false);

        $('#EstimatedProduction').attr('readonly', true);

        $('#GrooveDistance').attr('readonly', false);

        

        $('#NumberOfPlants').attr('readonly', false);

        $("#PlantationVarietyId").attr('readonly', false);

        $("#LabLot").attr('readonly', false);

        $("#NumEjeArbLot").attr('readonly', false);

        $("#PlantationStatusId").attr('readonly', false);

        $("#FormLot").attr('readonly', false);

        $('#TipoLot').attr('readonly', false);

        $('#EstimatedProductionManual').attr('readonly', false);

        $("#Hectares").attr('readonly', true);

    }




    else if ($('#PlantationTypeId').val() == "46496de0-3cd6-4f25-beb7-2446d0f6929d" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("55959369-0611-4180-adf9-2cf5a602420a");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica")

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica")

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }

    else if ($('#PlantationTypeId').val() == "80838f7d-a18e-4f25-aea3-5550ff159eb2" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("50bd3880-3c7f-4329-855e-2c75f1757b4f");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica")

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica")

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }

    else if ($('#PlantationTypeId').val() == "9e7b7f6b-03c9-49eb-aafd-5db7f4e8337e" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("f781c50b-c895-488c-9563-e61227f8ece0");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica")

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica")

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }

    else if ($('#PlantationTypeId').val() == "067ec55a-7b4b-436b-83df-713b26390929" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("6279c725-f82f-4057-a970-a64c9531d961");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica")

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica")

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }

    else if ($('#PlantationTypeId').val() == "b6cf7a3d-6070-4a16-9714-8ce60734d2af" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("b8b8cf51-22a8-4f35-8277-e48dcdd967cf");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica");

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica");

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }

    else if ($('#PlantationTypeId').val() == "ee9582c6-46f2-4c99-bca2-a307a09c5973" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("e6539371-25e7-46f2-a812-615e79938c12");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica")

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica")

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }

    else if ($('#PlantationTypeId').val() == "260c7669-f515-442e-be64-dcbbc539a991" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("2fcb2716-213a-45f4-9faa-5e0246bcf57e");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica");

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica");

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }

    else if ($('#PlantationTypeId').val() == "2cd08524-1be7-49d3-b67d-e7e691001f67" || $('#PlantationTypeId').val() == "") {


        $('#EstimatedProduction').val(1);
        $('#EstimatedProduction').attr('readonly', true);

        $('#EstimatedProductionManual').val(1);
        $('#EstimatedProductionManual').attr('readonly', true);

        $('#TreesDistance').val(1);
        $('#TreesDistance').attr('readonly', true);

        $('#GrooveDistance').val(1);
        $('#GrooveDistance').attr('readonly', true);

        $('#Density').val(1);
        $('#Density').attr('readonly', true);

        $('#NumberOfPlants').val(1);
        $('#NumberOfPlants').attr('readonly', true);


        $('#TipoLot').attr('disabled', true);
        $('#TipoLot').val("No aplica");

        $("#PlantationVarietyId").attr('disabled', true);

        $("#PlantationVarietyId").val("9d198f69-e10a-4e48-a348-5b8408e9badb");

        $("#PlantationStatusId").attr('disabled', true);
        $("#PlantationStatusId").val("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");

        $("#LabLot").attr('disabled', true);
        $("#LabLot").val("No aplica");

        $("#FormLot").attr('disabled', true);
        $("#FormLot").val("No aplica");

        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("No aplica");


        $("#Hectares").attr('readonly', false);

    }


    
});

$(document).on('change', '#LabLot', function (e) {
    if($('#LabLot').val() == "Zoca") {
        $("#NumEjeArbLot").attr('disabled', true);
        $("#NumEjeArbLot").val("2");
    }
});





