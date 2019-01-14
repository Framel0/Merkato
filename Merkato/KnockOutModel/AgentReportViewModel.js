var agentReportViewModel = function () {
    var self = this;

    self.Genders = ko.observableArray([]);
    self.SelectedGender = ko.observable();
    self.MaritalStatus = ko.observableArray([]);
    self.SelectedMaritalStatus = ko.observable();
    self.Status = ko.observableArray([]);
    self.SelectedStatus = ko.observable();



    self.LoadGenders = function () {
        $.ajax({
            
            url: 'api/AgentReportApi/getGenders',
            type: 'GET',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                self.Genders(data);


                console.log(selectedGenderID);

                if (selectedGenderID.length > 0) {
                    var m = ko.utils.arrayFirst(self.Genders(), function (item) {
                        console.log(item.ID.trim() + ' ' + selectedGenderID);
                        return item.ID.trim() === selectedGenderID;
                    });

                    self.SelectedGender(m);
                    if (m !== null) {
                        self.LoadGenders();
                    }

                }


            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    self.LoadMaritalStatus = function () {
        $.ajax({

            url:'api/AgentReportApi/getMaritalStatus',
            type: 'GET',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                self.MaritalStatus(data);


                console.log(selectedMaritalStatusID);

                if (selectedMaritalStatusID.length > 0) {
                    var m = ko.utils.arrayFirst(self.MaritalStatus(), function (item) {
                        console.log(item.ID.trim() + ' ' + selectedMaritalStatusID);
                        return item.ID.trim() === selectedMaritalStatusID;
                    });

                    self.SelectedMaritalStatus(m);
                    if (m !== null) {
                        self.LoadMaritalStatus();
                    }

                }


            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    self.LoadStatus = function () {
        $.ajax({

            url:'api/AgentReportApi/getStatus',
            type: 'GET',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                self.Stauts(data);


                console.log(selectedStatusID);

                if (selectedStatusID.length > 0) {
                    var m = ko.utils.arrayFirst(self.Status(), function (item) {
                        console.log(item.ID.trim() + ' ' + selectedStatusID);
                        return item.ID.trim() === selectedStatusID;
                    });

                    self.SelectedStatus(m);
                    if (m !== null) {
                        self.LoadStatus();
                    }

                }


            },
            error: function (request, error) {
                console.log(error);
            }
        });

    };

    self.LoadGenders();
    self.LoadMaritalStatus();
    self.LoadStatus();
}

ko.applyBindings(new agentReportViewModel());
