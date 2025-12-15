$(function () {

    function farmViewModel() {

        var self = this;

        function assignDepartment(department) {
            if (department !== undefined && department != null && department != "") {
                self.getMunicipalities();
            }
        };

        $('#searchDepartment').on('change', function (department) {
            if (department !== undefined && department != null && department != "") {
                self.getMunicipalities();
            }
        });

        self.getDepartments = function () {
            $.ajax({
                url: '/api/departments/',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                $('#searchDepartment').append($('<option>').text('Select a Department').attr('value', ''));
                $.map(data, function (dept, i) {
                    $('#searchDepartment').append($('<option>').text(dept.Name).attr('value', dept.Id));
                });
                assignDepartment($('#searchDepartment').val());
            });
        };

        function assignMunicipality(municipality) {
            if (municipality !== undefined && municipality != null && municipality != "") {
                self.getVillages();
            }
        };

        $('#searchMunicipality').on('change', function (municipality) {
            if (municipality !== undefined && municipality != null && municipality != "") {
                self.getVillages();
            }
        });

        self.getMunicipalities = function () {
            var url = '/api/municipalities/';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false,
                data: 'departmentId=' + $('#searchDepartment').val()
            }).done(function (data) {
                $('#searchMunicipality').empty();
                $('#searchMunicipality').append($('<option>').text('Select a Municipality').attr('value', ''));
                $.map(data, function (dept, i) {
                    $('#searchMunicipality').append($('<option>').text(dept.Name).attr('value', dept.Id));
                });
                assignMunicipality($('#searchMunicipality').val());
            });
        };

        self.getVillages = function () {
            var url = '/api/villages';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false,
                data: {
                    municipalityId: $('#searchMunicipality').val()
                }
            }).done(function (data) {
                $('#searchVillage').empty();
                $('#searchVillage').append($('<option>').text('Select a Village').attr('value', ''));
                $.map(data, function (dept, i) {
                    $('#searchVillage').append($('<option>').text(dept.Name).attr('value', dept.Id));
                });
            });
        };

        $('#farmsfilter').on('click', function () {
            var url = '/api/Farms/Index';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false,
                data: {
                    currentDepartment: $('#searchDepartment').val(),
                    currentMunicipality: $('#searchMunicipality').val(),
                    currentVillage: $('#searchVillage').val(),
                    farmerName: $('#searchFarmerName').val(),
                    farmName: $('#searchName').val(),
                    farmCode: $('#searchCode').val(),
                    farmerIdentification: $('#searchFarmerIdentification').val(),
                }
            }).done(function (data) {
                var list = $("#listFarms").empty();
                var list = $("#listFarms");
                $.map(data, function (farm, i) {
                    list.append("<li class='ui-state-default ui-sortable-handle'><input type='hidden' value=" + farm.Id + " name='selectedfarms' />" + "<strong>(" + farm.Code + ") - " + farm.Name + " </strong> [" + farm.FarmerName + farm.FarmerLastName + "] </li>");
                });
            }).fail(function (msg) {
                console.log("error" + JSON.stringify(msg));
            });
        });

        //$("#saveContact2").on('click', function () {
        //    alert("entro");
        //    //console.log("Datos: "+JSON.stringify($("#selectedFarms")))
        //    //var farmsToSave = "";
        //    //$("#selectedFarms").find("li").each(function () {
        //    //    var current = $(this);
        //    //    console.log(current);
        //    //    farmsToSave = farmsToSave + current.find("input").val() + "|";
        //    //    console.log(farmsToSave);
        //    //})
        //    //$("#selectedFarmsString").val(farmsToSave);
        //});

        $("ul.droptrue").sortable({
            connectWith: "ul"
        });

        $("ul.dropfalse").sortable({
            connectWith: "ul"
        });

        $("#listFarms, #selectedFarms").disableSelection();

        self.getDepartments();
    };
    var vm = new farmViewModel();
});