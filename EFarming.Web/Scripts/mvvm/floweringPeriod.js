$(function () {
    function floweringPeriodViewModel() {
        self = this;
        self.startDate = ko.observable();
        self.endDate = ko.observable();
        self.age = ko.observable();
        self.floweringPeriodQualification = ko.observable(selectedFloweringPeriodQualification);
        self.floweringPeriodQualifications = ko.observableArray();
        
        self.getFloweringPeriodQualifications = function () {
            $.ajax({
                url: '/api/floweringPeriodQualifications/',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                self.floweringPeriodQualifications([]);
                $.map(data, function (item, i) {
                    self.floweringPeriodQualifications.push(new SelectOption(item.Name, item.Id));
                });
            });
        };

        self.getFloweringPeriodQualifications();
    }

    fpvm = new floweringPeriodViewModel();
    fp = document.getElementById("flowering-period-form");

    ko.cleanNode(fp);
    ko.applyBindings(fpvm, fp);

});

$(".dateField").each(function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "yy-mm-dd",
        /*Configuracion para mostrar la fecha abajo del textbox*/
        beforeShow: function (input, inst) {
            var rect = input.getBoundingClientRect();
            setTimeout(function () {
                inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
            }, 0);
        }
    })
});