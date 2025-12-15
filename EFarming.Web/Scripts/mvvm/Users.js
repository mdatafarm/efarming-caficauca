$(function () {
    function User(us, p) {
        var self = this;
        this.FirstName = us.FirstName;
        this.LastName = us.LastName;
        this.Id = us.Id;
        this.Email = us.Email;
        this.Parent = p;
    }

    User.prototype = {
        Approve: function () {
            var usrs = [];
            var self = this;
            $.ajax({
                datatype: 'JSON',
                url: '/api/pendingusers/' + this.Id,
                type: 'PUT',
                async: false
            }).done(function (data) {
                data.forEach(function (user) {
                    var u = new User(user, self.Parent);
                    usrs.push(u);
                });
            });
            $("#pending-users").text(usrs.length);
            this.Parent.Users(usrs);
        },
        Deny: function () {
            var data = { Id: this.Id };
            var usrs = [];
            var self = this;
            $.ajax({
                datatype: 'JSON',
                url: '/api/pendingusers/' + this.Id,
                type: 'DELETE',
                async: false
            }).done(function (data) {
                data.forEach(function (user) {
                    var u = new User(user, self.Parent);
                    usrs.push(u);
                });
            });
            $("#pending-users").text(usrs.length);
            this.Parent.Users(usrs);
        }
    };

    function UsersViewModel() {
        var self = this;
        this.Users = ko.observableArray();
    }

    UsersViewModel.prototype = {
        GetUsers: function () {
            var usrs = [];
            var self = this;
            $.ajax({
                datatype: 'JSON',
                url: '/api/pendingusers',
                type: 'GET',
                async: false
            }).done(function (data) {
                data.forEach(function (user) {
                    var u = new User(user, self);
                    usrs.push(u);
                });

            });
            this.Users(usrs);
        }
    };

    var viewModel = new UsersViewModel();
    ko.applyBindings(viewModel);
    viewModel.GetUsers();
});