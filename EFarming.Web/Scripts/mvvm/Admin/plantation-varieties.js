$(function () {
    function plantationVarietiesViewModel() {
        var self = this;
        self.plantationType = ko.observable();
        self.plantationTypes = ko.observableArray();
        self.getPlantationTypes = function () {
            $.ajax({
                url: '/api/plantationtypes/',
                type: 'GET',
                dataType: 'json'
            }).done(function (data) {
                self.plantationTypes([]);
                $.map(data, function (dept, i) {
                    self.plantationTypes.push(new SelectOption(dept.Name, dept.Id));
                });
                self.assignplantationType();
            });
        };
        self.assignplantationType = function () {
            if (selected !== undefined) {
                self.plantationType(selected);
            }
        }
        self.getPlantationTypes();
    };

    var vm = new plantationVarietiesViewModel();
    ko.applyBindings(vm);
});