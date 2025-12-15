$(function () {
    function villageViewModel() {
        var self = this;

        self.department = ko.observable();
        self.municipality = ko.observable();

        self.departments = ko.observableArray();
        self.municipalities = ko.observableArray();

        self.getDepartments = function () {
            $.ajax({
                url: '/api/departments/',
                type: 'GET',
                dataType: 'json'
            }).done(function (data) {
                self.departments([]);
                $.map(data, function (dept, i) {
                    self.departments.push(new SelectOption(dept.Name, dept.Id));
                });
                self.assignDepartment();
            });
        };

        self.getMunicipalities = function () {
            var url = '/api/municipalities/';
            $.ajax({
                url: url,
                type: 'GET',
                dataType: 'json',
                data: {
                    departmentId: self.department()
                }
            }).done(function (data) {
                self.municipalities([]);
                $.map(data, function (dept, i) {
                    self.municipalities.push(new SelectOption(dept.Name, dept.Id));
                });
                self.assignMunicipality();
            });
        };

        self.assignMunicipality = function () {
            if (selectedMunicipality !== undefined) {
                self.municipality(selectedMunicipality);
            }
        };

        self.assignDepartment = function () {
            if (selected !== undefined) {
                self.department(selected);
                self.getMunicipalities();
            }
        };

        self.getDepartments();
    };

    var vm = new villageViewModel();
    ko.applyBindings(vm);
});