$(function () {
    function SensoryProfileAssessment(assessment, parent) {
        var self = this;
        self.Parent = parent;

        self.Id = assessment.Id;
        self.Date = assessment.Date;
        self.Description = assessment.Description;
        self.AssessmentTemplateId = assessment.AssessmentTemplateId;
    }
    var emptySensoryProfileAssessment = { Id: '', Date: '', Description: '' };

    function SensoryProfileViewModel() {
        var self = this;

        // These properties are for new Assessments only
        self.Date = ko.observable();
        self.Description = ko.observable();
        self.AssessmentTemplate = ko.observable();

        self.Assessments = ko.observableArray();
        self.Assessment = ko.observable(new SensoryProfileAssessment(emptySensoryProfileAssessment, self));
        self.AssessmentTemplates = ko.observableArray();

        self.Form = ko.observable('');

        self.SaveAssessment = function () {
            $.ajax({
                url: '/api/sensoryprofileassessments',
                dataType: 'JSON',
                type: 'POST',
                data: {
                    Date: self.Date(),
                    Description: self.Description(),
                    FarmId: FARMID,
                    AssessmentTemplateId: self.AssessmentTemplate().Id
                }
            }).done(function (data) {
                var assessment = new SensoryProfileAssessment(data, self);
                self.Assessments.push(assessment);
                self.Assessment(assessment)
                $('#new-sensory-profile-assessment').modal('hide')
                self.CancelAssessment();
                self.ChangeAssessment();
            });
        }

        self.CancelAssessment = function () {
            self.Date('');
            self.Description('');
        }

        self.GetAssessments = function () {
            $.ajax({
                url: '/api/sensoryprofileassessments',
                dataType: 'JSON',
                type: 'GET',
                data: { farmId: FARMID }
            }).done(function (data) {
                self.Assessments([]);
                data.forEach(function (assessment) {
                    self.Assessments.push(new SensoryProfileAssessment(assessment, self));
                });
            });
        }

        self.ChangeAssessment = function () {
            if (self.Assessment() === undefined) {
                self.Form('');
            }
            else {
                $.ajax({
                    url: '/SensoryProfile/create',
                    datatype: 'html',
                    type: 'GET',
                    data: { assessmentId: self.Assessment().Id }
                }).done(function (data) {
                    self.Form(data);
                });
            }
        }

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

        self.GetAssessments();
        self.GetAssessmentTemplates();
    }

    ko.applyBindings(new SensoryProfileViewModel());
});