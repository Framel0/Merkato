var MechanismViewModel = function () {
    var self = this;
    self.Products = ko.observableArray([]);
    self.selectedProduct = ko.observable();
    self.ProductsList = ko.observableArray([]);

    console.log(clientId);
    self.LoadProducts = function () {


        $.ajax({
            url: baseUrl + 'api/ProductMechanismApi/getClientProducts/' + clientId,
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

    self.Quantity = ko.observable();
    self.Id = ko.observable();

    console.log(mechanismId);
    self.SaveProduct = function () {
        var pm = {
            Id: self.Id,
            MechanismId: mechanismId,
            ProductId: self.selectedProduct(),
            Quantity: self.Quantity(),
            ProductOrder: 0,
            Product: null,
            UserId: null,
            LastDateModified: null
        };


        $.ajax({
            url: basePath + 'api/ProductMechanismApi/saveProduct',
            type: 'POST',
            cache: false,
            headers: { 'Access-Control-Allow-Origin': '*' },
            contentType: "application/json",
            dataType: 'json',
            data: JSON.stringify(pm),
            success: function (data) {
                console.log(data);

            },
            error: function (request, error) {
                console.log(error);
            }
        });
    };

    self.LoadProductsList = function () {
        $.ajax({
            url: baseUrl + 'api/ProductMechanismApi/getProductsList/' + mechanismId,
            type: 'GET',
            data: 'Json',
            contentType: 'application/json',
            success: function (data) {
                console.log(data);
                self.ProductsList(data);

            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    console.log('The quantity is', pm.Quantity);

    self.DeleteProduct = function (pm) {

        $.ajax({
          
            url: basePath + 'api/ProductMechanismApi/deleteProduct/' + pm.Id,
            contentType: 'application/json',
            data: 'json',
            type: 'Delete',
            crossDomain: true,
            success: function (data) {
                self.ProductsList.remove(pm);
                console.log(data);
                alert("Data Deleted");
            },
            error: function (data) {
                console.log("Is not answered");
            }
        });
    };



    self.LoadProducts();
    self.SaveProduct();
    self.LoadProductsList();

};

ko.applyBindings(new MechanismViewModel());