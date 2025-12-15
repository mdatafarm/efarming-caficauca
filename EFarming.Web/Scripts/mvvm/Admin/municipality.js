$(function () {
    function municipalityViewModel() {
        var self = this;
        self.department = ko.observable('7d3e2efa-0e17-4a26-a994-e1cd962f9bc1');
        self.departments = ko.observableArray();
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
        self.assignDepartment = function () {
            if (selected !== undefined) {
                self.department(selected);
            }
        }
        self.getDepartments();
    };

    var vm = new municipalityViewModel();
    ko.applyBindings(vm);
});