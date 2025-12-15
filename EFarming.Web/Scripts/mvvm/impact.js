$(function () {

    function Option(opt, parent) {
        this.parent = parent;
        this.Id = ko.observable(opt.Id);
        this.Description = ko.observable(opt.Description);
        this.Value = ko.observable(opt.Value);

        this.DOMName = ko.observable(opt.CriteriaId);
    }

    Option.prototype = {
    };

    function Criteria(cri, par) {
        var self = this;
        this.parent = par;
        this.Description = ko.observable(cri.Description);
        this.Value = ko.observable(cri.Value);
        this.Mandatory = ko.observable(cri.Mandatory);
        this.Options = ko.observableArray([]);
        this.SelectedOption = ko.observable();
        this.SelectedOption.subscribe(function () {
            self.parent.Recalculate();
        });

        this.MandatoryClass = ko.computed(function () {
            if (self.Mandatory())
                return "notify notify-red";
            return "notify";
        });
        cri.OrderedCriteriaOptions.forEach(function (option) {
            var opt = new Option(option, self);
            self.Options.push(opt);
            
            if (self.parent.parent.parent.ImpactAssessment() !== undefined &&
                self.parent.parent.parent.ImpactAssessment().Answers !== undefined &&
                self.parent.parent.parent.ImpactAssessment().Answers.indexOf(opt.Id()) > -1) {
                self.SelectedOption(opt);
            }
        });
    }

    Criteria.prototype = {

    };

    function Indicator(ind, parent) {
        var self = this;
        this.parent = parent;
        this.Name = ko.observable(ind.Name);
        this.Description = ko.observable(ind.Description);
        this.Scale = ko.observable(ind.Scale);
        this.Class = ko.observable('');
        this.Criteria = ko.observableArray([]);
        this.CountCriteria = ko.observable(0);
        this.SumCriteria = ko.observable(0);
        this.criteriaHelper = ko.observable('');

        
        ind.Criteria.forEach(function (criteria) {
            self.Criteria.push(new Criteria(criteria, self));
        });
        self.CountCriteria(self.Criteria.length);
    }

    Indicator.prototype = {
        Select: function () {
            this.parent.CurrentIndicator(this);
        },
        Recalculate: function () {
            var sum = 0;
            var total = 0;
            this.Criteria().forEach(function (criteria) {
                total = total + criteria.Value();
                if (criteria.SelectedOption() !== undefined) {
                    sum = sum + parseInt(criteria.SelectedOption().Value());
                }
            });
            this.SumCriteria(sum);
            this.criteriaHelper(sum + ' / ' + total);
        }
    };

    function ImpactAssessment(imp, parent) {
        var self = this;
        this.parent = parent;
        this.Date = imp.Date;
        this.Description = imp.Description;
        this.AssessmentTemplateId = imp.AssessmentTemplateId;
        this.Id = imp.Id;
        this.FarmId = FARM_ID;
        var answers = [];
        imp.Answers.forEach(function (answer) {
            answers.push(answer.Id);
        });
        this.Answers = answers;
        this.LocalDate = ko.computed(function () {
            var d = new Date(self.Date);
            return d.toLocaleDateString();
        });
        this.Title = ko.computed(function () {
            return self.LocalDate() + " - " + self.Description;
        });
    };

    ImpactAssessment.prototype = {
        Save: function () {
            var assessments = [];
            var parent = this.parent;
            var data = {
                farmId: this.FarmId,
                Date: this.Date,
                Description: this.Description,
                AssessmentTemplateId: parent.AssessmentTemplate().Id
            };
            $.ajax({
                url: '/api/impactassessments',
                datatype: 'JSON',
                type: 'POST',
                data: data
            }).done(function (data) {
                var imp = new ImpactAssessment(data, parent);
                parent.ImpactAssessments.push(imp);
                parent.ImpactAssessment(imp);
                $("#new-impact-assessment").modal('hide');
                parent.LoadCategories();
            });
        },
        DrawChart: function () {
        }
    };




    function Category(cat, parent) {
        var self = this;
        this.parent = parent;
        this.Id = ko.observable(cat.Id);
        this.Name = ko.observable(cat.Name);
        this.Score = ko.observable(cat.Score);
        this.AssessmentTemplateId = ko.observable(cat.AssessmentTemplateId);

        indicatorObjects = [];
        cat.Indicators.forEach(function (indicator) {
            var ind = new Indicator(indicator, self);
            ind.Recalculate();
            indicatorObjects.push(ind);
        });

        this.Indicators = ko.observableArray(indicatorObjects);
        this.Indicator = ko.observable();
        this.Class = ko.observable('');
        this.HTML_ID = ko.computed(function () {
            return "#" + self.Id();
        });
        this.ContentClass = ko.computed(function () {
            return "tab-pane " + self.Class();
        });
    };

    Category.prototype = {
        LoadIndicators: function () {
            var parent = this;
            var indicatorsObjects = [];
            $.ajax({
                url: '/api/indicators',
                type: 'GET',
                datatype: 'JSON',
                async: false,
                data: {
                    templateId: parent.parent.AssessmentTemplate().Id
                }
            }).done(function (data) {
                $.each(data, function (index, ind) {
                    var indicator = new Indicator(ind, parent);
                    indicatorsObjects.push(indicator);
                });
            });
            this.Indicators(indicatorsObjects);

            this.Indicators().forEach(function (indicator) {
                indicator.Recalculate();
            });

            this.CurrentIndicator(this.Indicators()[0]);
        },
        CurrentIndicator: function (ind) {
            this.UnselectAll();
            ind.Class('active');
            this.Indicator(ind);
        },
        UnselectAll: function () {
            this.Indicators().forEach(function (indicator) {
                indicator.Class('');
            });
        }
    };




    function ImpactViewModel() {
        this.Categories = ko.observableArray([]);
        this.Category = ko.observable();
        this.AssessmentTemplate = ko.observable();
        this.ImpactAssessment = ko.observable();
        this.NewImpactAssessment = ko.observable();
        this.ImpactAssessments = ko.observableArray();
        this.AssessmentTemplates = ko.observableArray();
        return this;
    };

    ImpactViewModel.prototype = {
        SaveAnswers: function () {
            var self = this;
            var data = {
                Id: this.ImpactAssessment().Id,
                FarmId: this.ImpactAssessment().FarmId,
                Date: this.ImpactAssessment().Date,
                Description: this.ImpactAssessment().Description,
                AssessmentTemplateId: this.ImpactAssessment().AssessmentTemplateId,
                Answers: []
            };

            this.Categories().forEach(function (category) {
                category.Indicators().forEach(function (indicator) {
                    indicator.Criteria().forEach(function (criteria) {
                        if (criteria.SelectedOption() !== undefined) {
                            data.Answers.push({ Id: criteria.SelectedOption().Id() });
                        }
                    });
                });
            });

            $.ajax({
                url: '/api/ImpactAssessments/' + data.Id,
                type: 'PUT',
                datatype: 'JSON',
                data: data
            });

            this.ImpactAssessment().DrawChart();
        },
        NewAssessment: function () {
            this.NewImpactAssessment(new ImpactAssessment({ Date: new Date(), Description: "", Answers: [] }, this));
        },
        GetAssessments: function () {
            var parent = this;
            var assessments = [];
            $.ajax({
                url: '/api/impactassessments',
                datatype: 'JSON',
                type: 'GET',
                data: { farmId: FARM_ID },
                async: false
            }).done(function (data) {
                data.forEach(function (value) {
                    assessments.push(new ImpactAssessment(value, parent));
                });
            });
            this.ImpactAssessments(assessments);
        },
        GetAssessmentTemplates: function () {
            var self = this;
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
        },
        LoadCategories: function () {
            this.NewImpactAssessment(null);
            var parent = this;
            var categoryObjects = [];
            $.ajax({
                url: '/api/categories',
                type: 'GET',
                datatype: 'JSON',
                async: false,
                data: {
                    templateId: parent.ImpactAssessment().AssessmentTemplateId
                }
            }).done(function (data) {
                $.each(data, function (index, cat) {
                    var category = new Category(cat, parent);
                    categoryObjects.push(category);
                });
            });
            this.Categories(categoryObjects);
            if (this.Categories()[0] != undefined && this.Categories()[0] != null)
                this.CurrentCategory(this.Categories()[0]);
        },
        CurrentCategory: function (cat) {
            this.UnselectAll();
            cat.Class('active');
            this.Category(cat);
        },
        UnselectAll: function () {
            this.Categories().forEach(function (category) {
                category.Class('');
            });
        }
    };

    var viewModel = new ImpactViewModel();
    ko.applyBindings(viewModel);
    viewModel.GetAssessments();
    viewModel.GetAssessmentTemplates();
});