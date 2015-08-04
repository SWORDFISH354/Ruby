appMainModule.controller("TicketStep1ViewModel", function ($scope, $http, $location, viewModelHelper, validator) {

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    $scope.viewModelHelper = viewModelHelper;
    var ticketStep1ModelRules = [];
    $scope.ticketStep1Model = new GulfRuby.TicketStep1Model();
    $scope.ticketStep1Model.CareOf = 
    $scope.showWalkin = true;
    $scope.showCorporate = false;
    $scope.showSupplier = false;
    $scope.selectedCustomerType = 1;
    $scope.selectedIssueMode = 1;
    $scope.openedDueDate = false;

    var validateCareOf = function() {
        if ($scope.selectedCustomerType === 1) {
            return !($scope.ticketStep1Model.CareOf.toString().trim() === '');
        }
        return true;
    };
    var validateCustomerName = function() {
        if ($scope.selectedCustomerType === 1) {
            return !($scope.ticketStep1Model.CustomerName.toString().trim() === '');
        }
        return true;
    };
    var validateCorporateClient = function() {
        if ($scope.selectedCustomerType === 2) {
            return !($scope.ticketStep1Model.CorporateClient.toString().trim() === '');
        }
        return true;
    };
    var validateItineraries = function() {
        return !($scope.ticketStep1Model.Itineraries.length <= 0);

    };
    var validatePassengers = function() {
        return !($scope.ticketStep1Model.Passengers.length <= 0);

    };
    var setupRules = function () {
        ticketStep1ModelRules.push(new validator.PropertyRule("CareOf", {
            custom: {
                validator: validateCareOf,
                message: "CareOf is required",
                params: function () { return true; } // must be function so it can be obtained on-demand
            }
        }));
        ticketStep1ModelRules.push(new validator.PropertyRule("CustomerName", {
            custom: {
                validator: validateCustomerName,
                message: "Customer Name is required",
                params: function () { return true; } // must be function so it can be obtained on-demand
            }
        }));
        ticketStep1ModelRules.push(new validator.PropertyRule("CorporateClient", {
            custom: {
                validator: validateCorporateClient,
                message: "Corporate Client is required",
                params: function () { return true; } // must be function so it can be obtained on-demand
            }
        }));
        ticketStep1ModelRules.push(new validator.PropertyRule("Itineraries", {
            custom: {
                validator: validateItineraries,
                message: "Please add an Itinerary",
                params: function () { return true; } 
            }
        }));
        ticketStep1ModelRules.push(new validator.PropertyRule("Passengers", {
            custom: {
                validator: validatePassengers,
                message: "Please add atleast one Passenger",
                params: function () { return true; }
            }
        }));
        ticketStep1ModelRules.push(new validator.PropertyRule("BaseFare", {
            required: { message: "Base Fare is required" }
        }));
        ticketStep1ModelRules.push(new validator.PropertyRule("QuotedFare", {
            required: { message: "Quoted Fare is required" }
        }));
    };




    // test Data for later hookup to DB
    $scope.CareOfSet = [{ Id: 1, Name: "Gulf Ruby" }, { Id: 2, Name: "Shajan Joseph" }, { Id: 3, Name: "Shiju Thomas" }];
    $scope.CorporateClientSet = [{ Id: 0, Name: "" }, { Id: 1, Name: "ABC Industries" }, { Id: 2, Name: "Etisalat" }, { Id: 3, Name: "Ashraf & Partners" }];
    $scope.SupplierSet = [{ Id: 0, Name: "" }, { Id: 1, Name: "ABC Suppliers" }, { Id: 2, Name: "Emirate Suppliers" }, { Id: 3, Name: "Al Futtaim Suppliers" }];

    $scope.newValue = function(selectedCustomerType) {
        if (selectedCustomerType === "2") {
            $scope.showWalkin = false;
            $scope.showCorporate = true;
        }
        else if (selectedCustomerType === "1") {
            $scope.showCorporate = false;
             $scope.showWalkin = true;
        }
    };


    $scope.selectedValue = function(selectedIssueMode) {
        if (selectedIssueMode === "2") {
            $scope.showSupplier = true;
        }
        else $scope.showSupplier = false;
    };

    $scope.AddNewItinerary = function () {

        var itinerary = new GulfRuby.ItineraryModel();
        $scope.ticketStep1Model.Itineraries.push(itinerary);

    };
    $scope.AddNewPassenger = function () {

        var passenger = new GulfRuby.PassengerInfo();
        $scope.ticketStep1Model.Passengers.push(passenger);

    };
    $scope.RemovePassengerPassenger = function () {

        var passenger = new GulfRuby.PassengerInfo();
        $scope.ticketStep1Model.Passengers.push(passenger);

    };
    $scope.openDate = function ($event, itinerary) {
        $event.preventDefault();
        $event.stopPropagation();
        itinerary.DateIsOpen = true;
    };

    $scope.openDueDate = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedDueDate = true;
    };

    $scope.save = function() {

        validator.ValidateModel($scope.ticketStep1Model, ticketStep1ModelRules);
        viewModelHelper.modelIsValid = $scope.ticketStep1Model.isValid;
        viewModelHelper.modelErrors = $scope.ticketStep1Model.errors;
        if (viewModelHelper.modelIsValid) {

            viewModelHelper.apiGet('api/hr/getemployees', null,
                    function (result) {
                        $scope.Employees = result.data;
                        $scope.displayedCollection = [].concat($scope.Employees);
                    });
        } else {
            viewModelHelper.modelErrors = $scope.ticketStep1Model.errors;
        }
    };


    $scope.confirm = function () {

        validator.ValidateModel($scope.ticketStep1Model, ticketStep1ModelRules);
        viewModelHelper.modelIsValid = $scope.ticketStep1Model.isValid;
        viewModelHelper.modelErrors = $scope.ticketStep1Model.errors;
        if (viewModelHelper.modelIsValid) {
            $scope.ticketStep1Model.Status = 2;
        } else {
            viewModelHelper.modelErrors = $scope.ticketStep1Model.errors;
        }
    };


    $scope.generateInvoice = function () {

        validator.ValidateModel($scope.ticketStep1Model, ticketStep1ModelRules);
        viewModelHelper.modelIsValid = $scope.ticketStep1Model.isValid;
        viewModelHelper.modelErrors = $scope.ticketStep1Model.errors;
        if (viewModelHelper.modelIsValid) {
            $scope.ticketStep1Model.InvoiceNumber = 2;
        } else {
            viewModelHelper.modelErrors = $scope.ticketStep1Model.errors;
        }
    };


    setupRules();
});