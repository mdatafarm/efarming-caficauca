$(function () {
    function otherActivityViewModel() {
        self = this;
        self.percentage = ko.observable();
        self.otherActivity = ko.observable(selectedOtherActivity);
        self.otherActivities = ko.observableArray();
        
        self.getOtherActivities = function () {
            $.ajax({
                url: '/api/otheractivities/',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.otherActivities([]);
                $.map(data, function (item, i) {
                    self.otherActivities.push(new SelectOption(item.Name, item.Id));
                });
            });
        };

        self.getOtherActivities();
    }


    oavm = new otherActivityViewModel();
    oa = document.getElementById("other-activity-form");

    ko.cleanNode(oa);
    ko.applyBindings(oavm, oa);

});