var AgentReportViewModel = function () {
    var self = this;

    self.Genders = ko.observableArray([]);
    self.SelectedGender = ko.observable();
    self.MaritalStatus = ko.observableArray([]);
    self.SelectedMaritalStatus = ko.observable();
    self.Status = ko.observableArray([]);
    self.SelectedStatus = ko.observable();
    self.Products = ko.observableArray([]);
    self.SelectedProduct = ko.observable();




    self.LoadGenders = function () {

        $.ajax({

            url: 'api/AgentReportApi/getGenders',
            type: 'GET',
            data: 'Json',
            contentType: 'application/json',
            success: function (data) {

                if (data.successfull === 1) {
                    self.Genders(data.model);
                    console.log(data);
                    console.log(data.successfull);
                }
             
            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    self.LoadMaritalStatus = function () {
        $.ajax({

            url: 'api/AgentReportApi/getMaritalStatus',
            type: 'GET',
            data: 'Json',
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                self.MaritalStatus(data);
            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    self.LoadStatus = function () {       
        $.ajax({

            url: 'api/AgentReportApi/getStatus',
            type: 'GET',
            data: 'Json',
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                self.Status(data);

            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    self.LoadProducts = function () {
        $.ajax({

            url: 'api/ProductMechanismApi/getClientProducts/1',
            type: 'GET',
            data: 'Json',
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                self.Products(data);

            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    self.LoadGenders();
    self.LoadMaritalStatus();
    self.LoadStatus();
    self.LoadProducts();
}

ko.applyBindings(new AgentReportViewModel());
