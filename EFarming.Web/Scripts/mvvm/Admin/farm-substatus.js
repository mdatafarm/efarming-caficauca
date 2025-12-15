$(function () {
    function farmSubstatusesViewModel() {
        var self = this;
        self.farmStatus = ko.observable();
        self.farmStatuses = ko.observableArray();
        self.getFarmStatuses = function () {
            $.ajax({
                url: '/api/farmstatuses/',
                type: 'GET',
                dataType: 'json'
            }).done(function (data) {
                self.farmStatuses([]);
                $.map(data, function (dept, i) {
                    self.farmStatuses.push(new SelectOption(dept.Name, dept.Id));
                });
                self.assignFarmStatus();
            });
        };
        self.assignFarmStatus = function () {
            if (selected !== undefined) {
                self.farmStatus(selected);
            }
        }
        self.getFarmStatuses();
    };

    var vm = new farmSubstatusesViewModel();
    ko.applyBindings(vm);
});