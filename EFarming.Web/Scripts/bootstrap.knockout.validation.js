ko.validation.configure({
    insertMessages: true,
    messagesOnModified: true,
    decorateElement: true,
    errorElementClass: 'has-error',
    errorMessageClass: 'help-inline text-danger'
});

ko.validation.rules['sumCriteria'] = {
    validator: function (val, otherVal) {
        return parseInt(otherVal.parent.countCriteria()) <= parseInt(otherVal.total);
    },
    message: 'Criteria summatory is greater than Indicator Scale'
};

ko.validation.registerExtenders();