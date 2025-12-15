$(function () {
    var departmentFilter = function () {
        var self = this;
        self.name = ko.observable();
    }

    function departmentViewModel() {
        var self = this;

        self.filter = ko.observable(new departmentFilter());

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
            });
        };

        self.getDepartments();
    };
    var vm = new departmentViewModel();
    ko.applyBindings(vm);
});