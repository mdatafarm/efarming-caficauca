var errors = [];

var errorClass = "has-error";
var successClass = "has-success";


function validateElement(element) {
    var parent = $(element).parent().parent();
    var span = $(element).next(".glyphicon.form-control-feedback");
    var help = span.next(".help-block");
    if (element.validity.valid) {
        parent.removeClass(errorClass).addClass(successClass);
        help.text("");
    }
    else {
        parent.removeClass(successClass).addClass(errorClass);
        help.text(element.validationMessage);
    }
}

function validateHorizontalElement(element) {
    var parent = $(element).parent();
    
    var help = $(element).next(".help-block");
    if (element.validity.valid) {
        parent.removeClass(errorClass).addClass(successClass);
        help.text("");
    }
    else {
        parent.removeClass(successClass).addClass(errorClass);
        help.text(element.validationMessage);
    }
}

function InitializeValidation(){
    $("form.to-validate input.form-control, form.to-validate select.form-control, form.to-validate textarea.form-control").on("blur", function (e) {
        validateElement(this);
    });

    $("form.to-validate").attr('novalidate', 'novalidate');

    $("form.to-validate").on("submit", function (e) {
        var elements = $(this).find("input.form-control, select.form-control, textarea.form-control");
        [].forEach.call(elements, function (element) {
            validateElement(element);
        });
        errors = $(this).find('.' + errorClass);
        if (errors.length > 0) {
            e.preventDefault();
        }
    });

    $("form.to-validate-horizontal input.form-control, form.to-validate-horizontal select.form-control, form.to-validate-horizontal textarea.form-control").on("blur", function (e) {
        validateHorizontalElement(this);
    });

    $("form.to-validate-horizontal").attr('novalidate', 'novalidate');

    $("form.to-validate-horizontal").on("submit", function (e) {
        var elements = $(this).find("input.form-control, select.form-control, textarea.form-control");
        [].forEach.call(elements, function (element) {
            validateHorizontalElement(element);
        });
        errors = $(this).find('.' + errorClass);
        if (errors.length > 0) {
            e.preventDefault();
        }
    });
}

$(function () {
    InitializeValidation();
});