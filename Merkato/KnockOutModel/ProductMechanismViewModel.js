var productMechanismViewModel;

// use as register mechanism views view model
function Mechanism(id, Name, ClientId, ProductId, Price, ProductList) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(id);
    self.Name = ko.observable(Name);
    self.ClientId = ko.observable(ClientId);
    self.ProductId = ko.observable(ProductId);
    self.Price = ko.observable(Price);
    self.Products = ko.observableArray([]);

    // Non-editable catalog data - should come from the server


    self.addMechanism = function () {
        var dataObject = ko.toJSON(this);

        $.ajax({
            url: '/api/MechanismApi',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                studentRegisterViewModel.studentListViewModel.students.push(new Student(data.Id, data.FirstName, data.LastName, data.Age, data.Description, data.Gender));

                self.Id(null);
                self.Name('');
                self.ClientId('');
                self.ProductId('');
                self.Price('');
                self.Products('');
            }
        });
    };
}

// use as student list view's view model
function MechanismList() {

    var self = this;

    // observable arrays are update binding elements upon array changes
    self.mechanisms = ko.observableArray([]);

    self.getMechanims = function () {
        self.mechanisms.removeAll();

        // retrieve mechanisms list from server side and push each object to model's mechanism list
        $.getJSON('/api/MechanismApi', function (data) {
            $.each(data, function (key, value) {
                self.mechanisms.push(new Mechanism(value.Id, value.Name, value.ClientId, value.ProductId, value.Price, value.Products));
            });
        });
    };


    // remove mechanism. current data context object is passed to function automatically.
    self.removeMechanism = function (mechanism) {
        $.ajax({
            url: '/api/MechanismApi/' + mechanism.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.mechanisms.remove(mechanism);
            }
        });
    };
}


// create index view view model which contain two models for partial views
productMechanismViewModel = { addMechanismViewModel: new Mechanism(), mechanismListViewModel: new MechanismList() };


// on document ready
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(productMechanismViewModel);

    // load mechanism data
    productMechanismViewModel.mechanismListViewModel.getMechanisms();
});
