$(function () {
    function FamilyUnitMemberViewModel() {
        self = this;
        self.identification = ko.observable();
        self.firsName = ko.observable();
        self.age = ko.observable();
        self.lastName = ko.observable();
        self.education = ko.observable(selectedEducation);
        self.relationship = ko.observable(selectedRelationship);
        self.maritalStatus = ko.observable(selectedMaritalStatus);
        self.educationList = ko.observableArray();
        self.relationshipList = ko.observableArray();
        self.maritalStatusList = ko.observableArray();
        self.owner = ko.observable();

        self.initializeSelects = function () {
            $.ajax({
                url: '/api/familyUnitMembers/initialize',
                type: 'GET',
                dataType: 'json',
                async: false
            }).done(function (data) {
                data = JSON.parse(data);
                self.maritalStatusList(data.MaritalStatusList);
                self.educationList(data.EducationList);
                self.relationshipList(data.RelationshipList);
            });
        };

        self.initializeSelects();
    }


    pvm = new FamilyUnitMemberViewModel();
    p = document.getElementById("family-unit-members-form");

    ko.cleanNode(p);
    ko.applyBindings(pvm, p);
});

$(".dateField").each(function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        /*Configuracion para mostrar la fecha abajo del textbox*/
        beforeShow: function (input, inst) {
            var rect = input.getBoundingClientRect();
            setTimeout(function () {
                inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
            }, 0);
        }
    })
});

$("#Age").change(function () {
    // alert("The text has been changed.");
    
    $("#Age").attr("readonly", true);
    var split = $("#Age").val().split('/');
    //console.log(split[2]);

    if (split[2] == "0001" || split[2] < "1900") {
        console.log("Deshabilitar boton");
        $("#btnSubmit").attr("disabled", true);
    }
    else {
        //alert("entro");
        $("#btnSubmit").attr("disabled", false);
    }
});