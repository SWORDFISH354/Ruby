appMainModule.controller("CompanyInformationViewModel", function ($scope, $http, $location, viewModelHelper, validator) {
    viewModelHelper.modelIsValid = true;
    viewModelHelper.sucessAlert = false;
    viewModelHelper.modelErrors = [];
    $scope.viewModelHelper = viewModelHelper;
    $scope.companyInformationModel = new GulfRuby.CompanyInformationModel();
    var companyInformationModelRules = [];

    var setupRules = function () {
        companyInformationModelRules.push(new validator.PropertyRule("TourismLicense", {
            required: { message: "Tourism License is required" }
        }));
        companyInformationModelRules.push(new validator.PropertyRule("Rent", {
            required: { message: "Rent amount is required" }
        }));
        companyInformationModelRules.push(new validator.PropertyRule("RentExpiry", {
            required: { message: "Rent Expiry is required" } //,
            //minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));

        companyInformationModelRules.push(new validator.PropertyRule("WasteNumber", {
            required: { message: "Waste management # is required" } //,
            //minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
        companyInformationModelRules.push(new validator.PropertyRule("WasteNumberExpiry", {
            required: { message: "Waste management # Expiry is required" } //,
            //minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
        companyInformationModelRules.push(new validator.PropertyRule("CivilDefence", {
            required: { message: "•	Civil Defense certificate # is required" } //,
            //minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
        companyInformationModelRules.push(new validator.PropertyRule("CivilDefenceExpiry", {
            required: { message: "Civil Defense certificate Expiry is required" } //,
            //minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
        companyInformationModelRules.push(new validator.PropertyRule("Sponsorship", {
            required: { message: "•	Sponsorship amount is required" } //,
            //minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
        companyInformationModelRules.push(new validator.PropertyRule("SponsorshipExpiry", {
            required: { message: "Sponsorship amount Expiry date is required" } //,
            //minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));


        //TODO: Add more form validations
    }

    $scope.setupDatesOnSubmit = function () {

        if ($scope.companyInformationModel.TourismExpiry != null && $scope.companyInformationModel.TourismExpiry !== '')
            $scope.companyInformationModel.TourismExpiry = moment($scope.companyInformationModel.TourismExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.companyInformationModel.TourismExpiry = '';

        if ($scope.companyInformationModel.RentExpiry != null && $scope.companyInformationModel.RentExpiry !== '')
            $scope.companyInformationModel.RentExpiry = moment($scope.companyInformationModel.RentExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.companyInformationModel.RentExpiry = '';

        if ($scope.companyInformationModel.WasteNumberExpiry != null && $scope.companyInformationModel.WasteNumberExpiry !== '')
            $scope.companyInformationModel.WasteNumberExpiry = moment($scope.companyInformationModel.WasteNumberExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.companyInformationModel.WasteNumberExpiry = '';

        if ($scope.companyInformationModel.CivilDefenceExpiry != null && $scope.companyInformationModel.CivilDefenceExpiry !== '')
            $scope.companyInformationModel.CivilDefenceExpiry = moment($scope.companyInformationModel.CivilDefenceExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.companyInformationModel.CivilDefenceExpiry = '';

        if ($scope.companyInformationModel.SponsorshipExpiry != null && $scope.companyInformationModel.SponsorshipExpiry !== '')
            $scope.companyInformationModel.SponsorshipExpiry = moment($scope.companyInformationModel.SponsorshipExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.companyInformationModel.SponsorshipExpiry = '';

    }

    var initialize = function () {

        viewModelHelper.apiGet('api/hr/companyinformation', null,
                function (result) {
                    $scope.companyInformationModel = result.data;
                    $scope.companyInformationModel.AddedTime = moment(result.data.AddedTime).format('DD/MM/YYYY');
                });
    }


    $scope.submit = function () {
        viewModelHelper.sucessAlert = false;
        if ($scope.companyInformationModel.ID > 0) {
           $scope.setupDatesOnSubmit();
            validator.ValidateModel($scope.companyInformationModel, companyInformationModelRules);
            viewModelHelper.modelIsValid = $scope.companyInformationModel.isValid;
            viewModelHelper.modelErrors = $scope.companyInformationModel.errors;
            if (viewModelHelper.modelIsValid) {
                viewModelHelper.apiPost('api/hr/submitcompanyinformation', $scope.companyInformationModel,
                    function (result) {
                        initialize();
                        viewModelHelper.sucessAlert = true;
                        viewModelHelper.sucessAlertMessage = 'Updated Successfully!';

                    });
            }
            else {
                viewModelHelper.modelErrors = $scope.companyInformationModel.errors;
            }
        }
    }

    $scope.openTourismExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedTourismExpiry = true;

    }



    $scope.openRentExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedRentExpiry = true;

    }


    $scope.openWasteNumberExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedWasteNumberExpiry = true;

    }


    $scope.openCivilDefenceExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedCivilDefenceExpiry = true;

    }

    $scope.openSponsorshipExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedSponsorshipExpiry = true;

    }


    setupRules();
    initialize();

});