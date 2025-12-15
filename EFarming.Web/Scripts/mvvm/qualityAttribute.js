$(function () {
    function OptionsAttribute(opt, par) {
        var self = this;

        self.Parent = par;
        self.Id = opt.Id;
        self.Description = ko.observable(opt.Description).extend({ required: true });

        self.ToJSON = function () {
            return { Id: self.Id, Description: self.Description() };
        }

        self.Delete = function () {
            var idx = self.Parent.OptionAttributes.indexOf(self);
            self.Parent.OptionAttributes.splice(idx, 1);
        }
    }
    var emptyOption = { Id: '', Description: '' };

    function OpenTextAttribute(openText, par) {
        var self = this;

        self.Parent = par;
        self.Id = openText.Id;
        self.Number = ko.observable(openText.Number);

        self.ToJSON = function () {
            return { Id: self.Id, Number: self.Number() };
        }
    }
    var emptyOpenText = { Id: '', Number: false };

    function RangeAttribute(range, par) {
        var self = this;

        self.Parent = par;
        self.Id = range.Id;
        self.MinVal = ko.observable(range.MinVal).extend({ required: true });
        self.MaxVal = ko.observable(range.MaxVal).extend({ required: true });
        self.Step = ko.observable(range.Step).extend({ required: true });

        self.ToJSON = function () {
            return { Id: self.Id, MinVal: self.MinVal(), MaxVal: self.MaxVal(), Step: self.Step() };
        }
    }
    var emptyRange = { Id: '', MinVal: '', MaxVal: '', Step: '' };

    function QualityAttribute(attr, par) {
        var self = this;
        self.Parent = par;
        self.Id = attr.Id;
        self.Description = ko.observable(attr.Description);
        self.Position = ko.observable(attr.Position);
        self.TypeOf = ko.observable(attr.TypeOf);
        self.AssessmentTemplateId = ko.observable(attr.AssessmentTemplateId);
        self.Html = attr.Html;
        self.Class = ko.observable('list-group-item');

        self.RangeAttribute = ko.observable(new RangeAttribute(emptyRange, self));
        self.OpenTextAttribute = ko.observable(new OpenTextAttribute(emptyOpenText, self));
        self.OptionAttributes = ko.observableArray();

        self.ShowRange = ko.observable(false);
        self.ShowOptions = ko.observable(false);
        self.ShowOpenText = ko.observable(false);
        self.IsNew = ko.computed(function () {
            return !(self.Id !== undefined && self.Id !== '');
        })

        self.Setup = function (attr) {
            if (self.TypeOf() === 'RANGE') {
                self.RangeAttribute(new RangeAttribute(attr.RangeAttribute, self));
            }
            else if (self.TypeOf() === 'OPTIONS') {
                self.OptionAttributes([]);
                attr.OptionAttributes.forEach(function (opt) {
                    self.OptionAttributes.push(new OptionsAttribute(opt, self));
                })
            }
            else if (self.TypeOf() === 'OPEN_TEXT') {
                self.OpenTextAttribute(new OpenTextAttribute(attr.OpenTextAttribute, self));
            }
        }


        self.Click = function () {
            self.Parent.QualityAttributes().forEach(function (attr) {
                attr.Class('list-group-item');
            });
            self.Class('list-group-item active');
            this.Parent.New(self);
            self.ChangeType();
        }

        self.AddOption = function () {
            self.OptionAttributes.push(new OptionsAttribute(emptyOption, self));
        }

        self.ChangeType = function () {
            if (self.TypeOf() === undefined) {
                self.ShowOptions(false);
                self.ShowRange(false);
                self.ShowOpenText(false);
            }
            else if (self.TypeOf() === 'RANGE') {
                self.ShowOptions(false);
                self.ShowRange(true);
                self.ShowOpenText(false);
            }
            else if (self.TypeOf() === 'OPTIONS') {
                self.ShowOptions(true);
                self.ShowRange(false);
                self.ShowOpenText(false);
            }
            else if (self.TypeOf() === 'OPEN_TEXT') {
                self.ShowOptions(false);
                self.ShowRange(false);
                self.ShowOpenText(true);
            }
            else {
                self.ShowOptions(false);
                self.ShowRange(false);
                self.ShowOpenText(false);
            }
        };

        self.ToJSON = function () {
            var data = {
                Id: self.Id,
                Description: self.Description(),
                TypeOf: self.TypeOf(),
                Position: self.Position(),
                AssessmentTemplateId: self.AssessmentTemplateId()
            };
            if (self.TypeOf() === undefined) { }
            else if (self.TypeOf() === 'RANGE') {
                data.RangeAttribute = self.RangeAttribute().ToJSON();
            }
            else if (self.TypeOf() === 'OPTIONS') {
                data.OptionAttributes = [];
                self.OptionAttributes().forEach(function (option) {
                    data.OptionAttributes.push(option.ToJSON());
                });
            }
            else if (self.TypeOf() === 'OPEN_TEXT') {
                data.OpenTextAttribute = self.OpenTextAttribute().ToJSON();
            }
            return data;
        }

        self.Delete = function () {
            var data = self.ToJSON();
            $.ajax({
                url: '/api/qualityattributes/' + self.Id,
                type: 'DELETE',
                dataType: 'JSON',
                data: data
            }).done(function (data) {
                self.Parent.QualityAttributes([]);
                data.forEach(function (attr) {
                    var qa = new QualityAttribute(attr, self.Parent);
                    qa.Setup(attr);
                    self.Parent.QualityAttributes.push(qa);
                });
                self.Parent.New(new QualityAttribute(emptyAttribute, self.Parent));
                self.Parent.LoadPreview();
            });
        }

        self.Save = function () {
            var data = self.ToJSON();
            var url = '';
            var type = '';
            if (self.Id !== undefined && self.Id !== '') {
                url = '/api/qualityattributes/' + self.Id;
                type = 'PUT';
            }
            else {
                url = '/api/qualityattributes';
                type = 'POST';
            }

            $.ajax({
                type: type,
                url: url,
                dataType: 'json',
                data: data
            }).done(function (data) {
                self.Parent.QualityAttributes([]);
                data.forEach(function (attr) {
                    var qa = new QualityAttribute(attr, self.Parent);
                    qa.Setup(attr);
                    self.Parent.QualityAttributes.push(qa);
                });
                self.Parent.AddNew();
                self.Parent.LoadPreview();
            })
        };
    }
    var emptyAttribute = { Id: '', Description: '', TypeOf: '', Html: '', Position: 1 };

    function QualityAttributeViewModel() {
        var self = this;

        self.New = ko.observable(new QualityAttribute(emptyAttribute, self));

        self.AssessmentTemplate = ko.observable();

        self.AssessmentTemplates = ko.observableArray();
        self.QualityAttributes = ko.observableArray();
        self.Preview = ko.observable('');

        self.VisibleAttributes = ko.computed(function () {
            return self.AssessmentTemplate() != undefined;
        });

        self.GetAssessmentTemplates = function () {
            $.ajax({
                type: 'GET',
                dataType: 'JSON',
                url: '/api/assessmenttemplates?type=EFarming.Core.QualityModule.SensoryProfileAggregate.SensoryProfileAssessment',
                async: false
            }).done(function (data) {
                self.AssessmentTemplates([]);
                data.forEach(function (temp) {
                    self.AssessmentTemplates.push({ Description: temp.Name, Id: temp.Id });
                });
            });
        };

        self.GetQualityAttributes = function () {
            $.ajax({
                type: 'GET',
                dataType: 'JSON',
                url: '/api/qualityattributes',
                async: false,
                data: {
                    templateId: self.AssessmentTemplate().Id
                }
            }).done(function (data) {
                self.QualityAttributes([]);
                data.forEach(function (attr) {
                    var qa = new QualityAttribute(attr, self);
                    qa.Setup(attr);
                    self.QualityAttributes.push(qa);
                });
            });
            self.LoadPreview();
        };

        self.AddNew = function () {
            emptyAttribute.AssessmentTemplateId = self.AssessmentTemplate().Id;
            self.New(new QualityAttribute(emptyAttribute, self));
            self.QualityAttributes().forEach(function (attr) {
                attr.Class('list-group-item');
            });
        };

        self.LoadPreview = function () {
            $.ajax({
                type: 'GET',
                dataType: 'html',
                url: '/qualityattributes/preview',
                async: false,
                data: {
                    templateId: self.AssessmentTemplate().Id
                }
            }).done(function (data) {
                self.Preview(data);
            });
        }

        self.GetAssessmentTemplates();
    }

    var vm = new QualityAttributeViewModel();
    ko.applyBindings(vm);
});