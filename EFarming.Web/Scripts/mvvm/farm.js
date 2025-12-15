$(function () {

    var productivity = function () {
        var self = this;
        self.totalHectares = ko.observable();
        self.infraestructureHectares = ko.observable();
        self.forestProtectedHectares = ko.observable();
        self.conservationHectares = ko.observable();
        self.shadingPercentage = ko.observable();
    };

    var farm = function (parent, country, supplier, supplyChain,
            department, municipality, village,
            farmStatus, farmSubstatus, cooperative, ownershipType,
            code, name, longitude, latitude, elevation, associatedPerson,
            farmerName, farmerIdentification) {
        var self = this;
        self.country = ko.observable(country);
        self.supplier = ko.observable(supplier);
        self.supplyChain = ko.observable(supplyChain);
        self.department = ko.observable(department);
        self.municipality = ko.observable(municipality);
        self.village = ko.observable(village);
        self.code = ko.observable(code);
        self.name = ko.observable(name);
        self.longitude = ko.observable(longitude);
        self.latitude = ko.observable(latitude);
        self.elevation = ko.observable(elevation);
        self.cooperative = ko.observable(cooperative);
        self.farmStatus = ko.observable(farmStatus);
        self.farmSubstatus = ko.observable(farmSubstatus);
        self.ownershipType = ko.observable(ownershipType);
        self.associatedPerson = ko.observable(associatedPerson);
        self.farmerIdentification = ko.observable(farmerIdentification);
        self.farmerName = ko.observable(farmerName);

        self.productivity = ko.observable(new productivity());

        self.name.subscribe(function (new_value) {
            parent.geoLocate();
        });
        self.longitude.subscribe(function (new_value) {
            parent.geoLocate();
        });
        self.latitude.subscribe(function (new_value) {
            parent.geoLocate();
        });
    };

    function farmViewModel() {
        var self = this;

        self.farm = ko.observable(new farm(self,
            selectedCountry,
            selectedSupplier,
            selectedSupplyChain,
            selectedDepartment,
            selectedMunicipality,
            selectedVillage, selectedFarmStatus, selectedFarmSubstatus, selectedCooperative,
            selectedOwnershipType, selectedCode, selectedName, selectedLongitude, selectedLatitude,
            selectedElevation, selectedAssociatedPeople, selectedFarmerName, selectedFarmerIdentification));

        self.countries = ko.observableArray();
        self.suppliers = ko.observableArray();
        self.supplyChains = ko.observableArray();
        self.departments = ko.observableArray();
        self.municipalities = ko.observableArray();
        self.villages = ko.observableArray();
        self.farmStatuses = ko.observableArray();
        self.farmSubstatuses = ko.observableArray();
        self.ownershipTypes = ko.observableArray();
        self.cooperatives = ko.observableArray();
        self.associatedPeople = ko.observableArray();
        

        self.assignCountry = function (country) {
            if (country !== undefined && country != null && country != "") {
                self.getSuppliers();
            }
        };
        self.getCountries = function () {
            $.ajax({
                url: '/api/countries/',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.countries([]);
                $.map(data, function (coun, i) {
                    self.countries.push(new SelectOption(coun.Name, coun.Id));
                });
                self.assignCountry(selectedCountry);
            });
        };

        self.assignSupplier = function (supplier) {
            if (supplier !== undefined && supplier != null && supplier != "") {
                self.getSupplyChains();                
            }
        };
        self.getSuppliers = function () {
            var url = '/api/suppliers/' + self.farm().country();
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {                
                self.suppliers([]);
                $.map(data, function (sup, i) {
                    self.suppliers.push(new SelectOption(sup.Name, sup.Id));
                });
                self.assignSupplier(selectedSupplier);
            });
        };

        self.getSupplyChains = function () {            
            var url = '/api/SupplierChains/' + self.farm().supplier();
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.supplyChains([]);
                $.map(data, function (sc, i) {
                    self.supplyChains.push(new SelectOption(sc.Name, sc.Id));
                });
            });
        };


        self.assignDepartment = function (department) {
            if (department !== undefined && department != null && department != "") {
                self.getMunicipalities();
            }
        };
        self.getDepartments = function () {
            $.ajax({
                url: '/api/departments/',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.departments([]);
                $.map(data, function (dept, i) {
                    self.departments.push(new SelectOption(dept.Name, dept.Id));
                });
                self.assignDepartment(selectedDepartment);
            });
        };

        self.assignMunicipality = function (municipality) {
            if (municipality !== undefined && municipality != null && municipality != "") {
                self.getVillages();
            }
        };
        self.getMunicipalities = function () {
            var url = '/api/municipalities/';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false,
                data: 'departmentId=' + self.farm().department()
            }).done(function (data) {
                self.municipalities([]);
                $.map(data, function (dept, i) {
                    self.municipalities.push(new SelectOption(dept.Name, dept.Id));
                });
                self.assignMunicipality(selectedMunicipality);
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
                    municipalityId: self.farm().municipality()
                }
            }).done(function (data) {
                self.villages([]);
                $.map(data, function (dept, i) {
                    self.villages.push(new SelectOption(dept.Name, dept.Id));
                });
            });
        };

        self.assignFarmStatus = function (farmStatus) {
            if (farmStatus !== undefined && farmStatus != null && farmStatus != "") {
                self.getFarmSubstatuses();
            }
        };
        self.getFarmStatuses = function () {
            var url = '/api/farmstatuses';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.farmStatuses([]);
                $.map(data, function (item, i) {
                    self.farmStatuses.push(new SelectOption(item.Name, item.Id));
                });
                self.assignFarmStatus(selectedFarmStatus);
            });
        };

        self.getFarmSubstatuses = function () {
            var url = '/api/farmsubstatuses';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false,
                data: {
                    farmStatusId: self.farm().farmStatus()
                }
            }).done(function (data) {
                self.farmSubstatuses([]);
                $.map(data, function (item, i) {
                    self.farmSubstatuses.push(new SelectOption(item.Name, item.Id));
                });
            });
        };

        self.getOwnershipTypes = function () {
            var url = '/api/ownershipTypes';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.ownershipTypes([]);
                $.map(data, function (item, i) {
                    self.ownershipTypes.push(new SelectOption(item.Name, item.Id));
                });
            });
        };

        self.getCooperatives = function () {
            var url = '/api/cooperatives';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.cooperatives([]);
                $.map(data, function (item, i) {
                    self.cooperatives.push(new SelectOption(item.Name, item.Id));
                });
            });
        };

        self.getTechnicians = function () {
            var url = '/api/technicians';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.associatedPeople([]);
                $.map(data, function (item, i) {
                    self.associatedPeople.push(item);
                });
            });
        };

        self.geoLocate = function () {
            var lt = self.farm().latitude().replace(",", ".");
            var lg = self.farm().longitude().replace(",", ".");
            var coords = new google.maps.LatLng(lt, lg);
            var options = {
                zoom: 15,
                center: coords,
                mapTypeControl: false,
                navigationControlOptions: {
                    style: google.maps.NavigationControlStyle.SMALL
                },
	      // Para la vista con Satelite y Calles se utiliza HYBRID
                mapTypeId: google.maps.MapTypeId.SATELLITE,
                // Para la vista tipo satelite se define aqui
                // mapTypeId: google.maps.MapTypeId.SATELLITE,                
                // Para la vista tipo calle se define
                // mapTypeId: google.maps.MapTypeId.ROADMAP,
                draggable: false,
                zoomControl: true,
                scrollwheel: false,
                disableDoubleClickZoom: true,
                rotateControl: false,
                streetViewControl: false,
                overviewMapControl: false,
                panControl: false
            };

            var map = new google.maps.Map(document.getElementById("map-canvas"), options);

            var marker = new google.maps.Marker({
                position: coords,
                map: map,
                title: self.farm().name()
            });
            self.getElevation(coords);
        }

        self.getElevation = function (position) {
            elevator = new google.maps.ElevationService();
            var positions = [];
            positions.push(position);
            var positionalRequest = {
                'locations': positions
            };
            elevator.getElevationForLocations(positionalRequest, function (results, status) {
                if (status == google.maps.ElevationStatus.OK) {
                    if (results[0]) {
                        self.farm().elevation(results[0].elevation);
                        return;
                    }
                }
                self.farm().elevation(0);
            });
        };

        self.initializeData = function () {
            self.getCountries();
            self.getDepartments();
            self.getFarmStatuses();
            self.getCooperatives();
            self.getOwnershipTypes();
            self.getTechnicians();
        }

        self.initializeData();
    };
    var vm = new farmViewModel();
    ko.applyBindings(vm, $("#basic-information").get(0));
    setTimeout(function () {
        vm.geoLocate();
    }, 2500);
    
});