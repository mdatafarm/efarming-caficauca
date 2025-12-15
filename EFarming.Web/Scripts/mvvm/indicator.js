var ivm = null;

$(function () {
    var CriteriaOption = function (indId, criId, opt, p) {
        var parent = p;
        var self = this;

        self.Id = opt != null ? opt.Id : "";
        self.Description = ko.observable(opt != null ? opt.Description : "").extend({ required: true });
        self.Value = ko.observable(opt != null ? opt.Value : "").extend({ required: true, max: parent.Value() });
        self.IndicatorId = indId;
        self.CriteriaId = criId;

        self.oldValues = {};

        self.visibleForm = ko.observable(false);
        self.visibleInfo = ko.observable(true);
        self.isNew = opt == null ? ko.observable(true) : ko.observable(false);
        self.errors = ko.validation.group(self);

        self.DOMId = ko.computed(function () {
            return "#" + self.Id;
        });

        self.ShowForm = function () {
            self.visibleForm(true);
            self.visibleInfo(false);
            self.oldValues = { Description: self.Description(), Value: self.Value() };
        };

        self.HideForm = function () {
            self.visibleForm(false);
            self.visibleInfo(true);
            self.errors.showAllMessages(false);
        };

        self.Cancel = function () {
            self.Description(self.oldValues.Description);
            self.Value(self.oldValues.Value);
            self.HideForm();
        }

        self.Save = function () {
            if (self.errors().length > 0) {
                self.errors.showAllMessages();
            }
            else {
                var url = '/api/criteriaoptions/' + self.Id;
                if (self.Id == "")
                    url = '/api/criteriaoptions/';
                $.ajax({
                    url: url,
                    type: self.Id == "" ? 'POST' : 'PUT',
                    dataType: 'json',
                    data: {
                        Id: self.Id,
                        Description: self.Description,
                        Value: self.Value,
                        CriteriaId: self.CriteriaId,
                        IndicatorId: self.IndicatorId
                    },
                    async: false
                }).done(function (data) {
                    parent.IsCollapsed(true);
                    parent.LoadCriteriaOptions(data);
                });
            }
        };

        self.Delete = function () {
            var data = { indicatorId: self.IndicatorId, criteriaId: self.CriteriaId };
            var url = '/api/criteriaoptions/' + self.Id;
            $.ajax({
                url: url,
                type: 'delete',
                dataType: 'json',
                data: data,
                async: false
            }).done(function (data) {
                parent.IsCollapsed(true);
                parent.LoadCriteriaOptions(data);
            });
        };

        if (self.isNew()) {
            self.ShowForm();
        }
    };

    var Criteria = function (indId, indScale, cri, p) {
        var parent = p;
        var self = this;
        self.IndicatorScale = indScale;

        self.Id = cri != null ? cri.Id : "";
        self.Description = ko.observable(cri != null ? cri.Description : "").extend({ required: true });
        self.Value = ko.observable(cri != null ? cri.Value : 0).extend({ required: true, sumCriteria: { parent: parent, total: self.IndicatorScale } });
        self.Mandatory = ko.observable(cri != null ? cri.Mandatory : false);
        self.IndicatorId = indId;
        self.oldValues = {};

        self.CriteriaOptions = ko.observableArray([]);

        self.visibleForm = ko.observable(false);
        self.visibleInfo = ko.observable(true);
        self.isNew = cri == null ? ko.observable(true) : ko.observable(false);
        self.IsCollapsed = ko.observable(true);
        self.errors = ko.validation.group(self);

        self.DOMId = ko.computed(function () {
            return "#" + self.Id;
        });

        self.ShowForm = function () {
            parent.HideAllCriteria();
            self.visibleForm(true);
            self.visibleInfo(false);
            self.oldValues = { Description: self.Description(), Value: self.Value() };
        };

        self.HideForm = function () {
            self.visibleForm(false);
            self.visibleInfo(true);
            self.errors.showAllMessages(false);
        };

        self.Cancel = function () {
            self.Description(self.oldValues.Description);
            self.Value(self.oldValues.Value);
            self.HideForm();
        }

        self.GetCriteriaOptions = function () {
            $.ajax({
                url: '/api/criteriaoptions',
                type: 'GET',
                dataType: 'json',
                data: {
                    indicatorId: self.IndicatorId,
                    criteriaId: self.Id
                },
                async: true
            }).done(function (data) {
                self.LoadCriteriaOptions(data);
            });
        };

        self.LoadCriteriaOptions = function (data) {
            if (!self.IsCollapsed()) {
                self.IsCollapsed(true);
                self.CriteriaOptions([]);
            }
            else {
                self.IsCollapsed(false);
                self.CriteriaOptions([]);
                self.CriteriaOptions.push(new CriteriaOption(self.IndicatorId, self.Id, null, self));
                $.each(data, function (index, value) {
                    var co = new CriteriaOption(self.IndicatorId, self.Id, value, self);
                    self.CriteriaOptions.push(co);
                });
            }
        }

        self.Save = function () {
            if (self.errors().length > 0) {
                self.errors.showAllMessages();
            }
            else {
                var url = '/api/criteria/' + self.Id;
                if (self.Id == "")
                    url = '/api/criteria/';
                $.ajax({
                    url: url,
                    type: self.Id == "" ? 'POST' : 'PUT',
                    dataType: 'json',
                    data: {
                        Id: self.Id,
                        Name: self.Name,
                        Description: self.Description,
                        Value: self.Value,
                        IndicatorId: self.IndicatorId,
                        Mandatory: self.Mandatory
                    },
                    async: false
                }).done(function (data) {
                    parent.LoadCriteria(self.IndicatorId, self.IndicatorScale, data);
                });
            }
        };

        self.Delete = function () {
            var data = { indicatorId: self.IndicatorId };
            var url = '/api/criteria/' + self.Id;
            $.ajax({
                url: url,
                type: 'delete',
                dataType: 'json',
                data: data,
                async: false
            }).done(function (data) {
                parent.LoadCriteria(self.IndicatorId, self.IndicatorScale, data);
            });
        };
    };

    var Indicator = function (ind, parent) {
        var self = this;
        self.Id = ind != null ? ind.Id : "";
        self.Name = ko.observable(ind != null ? ind.Name : "").extend({ required: true });;
        self.Description = ko.observable(ind != null ? ind.Description : "");
        self.Scale = ko.observable(ind != null ? ind.Scale : 100).extend({ required: true });
        self.CategoryName = ko.observable(ind != null ? ind.CategoryName : '');
        self.CategoryId = ko.observable(ind != null ? ind.CategoryId : "");
        self.visibleForm = ko.observable(false);
        self.visibleInfo = ko.observable(true);
        self.isNew = ind == null ? ko.observable(true) : ko.observable(false);
        self.errors = ko.validation.group(self);
        self.oldValues = { Description: "" };

        self.ShowForm = function () {
            parent.HideAll();
            self.visibleForm(true);
            self.visibleInfo(false);
            self.oldValues = { Name: self.Name(), Description: self.Description(), Scale: self.Scale() };
        };

        self.HideForm = function () {
            self.visibleForm(false);
            self.visibleInfo(true);
            self.errors.showAllMessages(false);
        };
        self.Cancel = function () {
            self.Name(self.oldValues.Name);
            self.Description(self.oldValues.Description);
            self.Scale(self.oldValues.Scale);
            self.HideForm();
        }

        self.UnloadDetails = function () {
            $("#" + self.Id).removeClass("active");
        }

        self.Save = function () {
            if (self.errors().length > 0) {
                self.errors.showAllMessages();
            }
            else {
                var url = '/api/indicators/' + self.Id;
                if (self.Id == "")
                    url = '/api/indicators';
                $.ajax({
                    url: url,
                    type: self.Id == "" ? 'POST' : 'PUT',
                    dataType: 'json',
                    data: {
                        Id: self.Id,
                        Name: self.Name(),
                        Description: self.Description(),
                        Scale: self.Scale(),
                        CategoryID: self.CategoryId()
                    },
                    async: false
                }).done(function (data) {
                    parent.LoadIndicators(data);
                });
            }
        };

        self.Delete = function () {
            var data = { id: self.Id };
            var url = '/api/indicators/' + self.Id;
            $.ajax({
                url: url,
                type: 'delete',
                dataType: 'json',
                data: data,
                async: false
            }).done(function (data) {
                parent.LoadIndicators(data);
            });
        };

        self.LoadCriteria = function () {
            parent.HideAll();
            $("#" + self.Id).addClass("active");

            $.ajax({
                url: '/api/criteria',
                type: 'GET',
                dataType: 'json',
                data: {
                    indicatorId: self.Id
                },
                async: true
            }).done(function (data) {
                parent.LoadCriteria(self.Id, self.Scale(), data);
            });
        }
    };

    function IndicatorViewModel() {
        var self = this;
        self.AssessmentTemplate = ko.observable();

        self.AssessmentTemplates = ko.observableArray();
        self.indicators = ko.observableArray([]);
        self.Criteria = ko.observableArray();
        self.Categories = ko.observableArray();

        self.LoadCriteria = function (indicatorId, indicatorScale, data) {
            self.Criteria([]);
            self.Criteria.push(new Criteria(indicatorId, indicatorScale, null, self));
            $.each(data, function (index, value) {
                self.Criteria.push(new Criteria(indicatorId, indicatorScale, value, self));
            });
        }

        self.countCriteria = ko.computed(function () {
            var count = 0;
            $.each(self.Criteria(), function (index, value) {
                count = count + parseInt(value.Value());
            });
            return count;
        });

        self.SelectAssessmentTemplate = function () {
            self.GetCategories();
            self.GetAll();
        }
        
        self.VisibleIndicators = ko.computed(function () {
            return self.AssessmentTemplate() != undefined;
        });

        self.GetAssessmentTemplates = function () {
            $.ajax({
                type: 'GET',
                dataType: 'JSON',
                url: '/api/assessmenttemplates?type=EFarming.Core.ImpactModule.ImpactAggregate.ImpactAssessment',
                async: false
            }).done(function (data) {
                self.AssessmentTemplates([]);
                data.forEach(function (temp) {
                    self.AssessmentTemplates.push({ Description: temp.Name, Id: temp.Id });
                });
            });
        };

        self.GetCategories = function () {
            $.ajax({
                url: '/api/categories',
                type: 'GET',
                dataType: 'json',
                async: false,
                data: {
                    templateId: self.AssessmentTemplate().Id
                }
            }).done(function (data) {
                self.Categories(data);
            });
        };

        self.GetAll = function () {
            $.ajax({
                url: '/api/indicators',
                type: 'GET',
                dataType: 'json',
                async: true,
                data: {
                    templateId: self.AssessmentTemplate().Id
                }
            }).done(function (data) {
                self.LoadIndicators(data);
            });
        };

        self.LoadIndicators = function (data) {
            self.indicators([]);
            self.indicators.push(new Indicator(null, self));
            $.each(data, function (index, value) {
                self.indicators.push(new Indicator(value, self));
            });
        }

        self.HideAll = function () {
            $.each(self.indicators(), function (index, value) {
                value.HideForm();
                value.UnloadDetails();
            });
        }
        self.HideAllCriteria = function () {
            $.each(self.Criteria(), function (index, value) {
                value.HideForm();
            });
        }

        self.GetAssessmentTemplates();
    };

    ivm = new IndicatorViewModel();
    ko.applyBindings(ivm);
});