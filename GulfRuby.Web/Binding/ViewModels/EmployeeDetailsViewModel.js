appMainModule.controller("EmployeeDetailsViewModel", function($scope, $http, $location, viewModelHelper, validator) {
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    $scope.viewModelHelper = viewModelHelper;
    $scope.employeeDetailsModel = new GulfRuby.EmployeeDetailsModel();
    var employeeDetailsModelRules = [];

    var setupRules = function() {
        employeeDetailsModelRules.push(new validator.PropertyRule("UserName", {
            required: { message: "UserName is required" }
        }));
        employeeDetailsModelRules.push(new validator.PropertyRule("Name", {
            required: { message: "Name is required" }
        }));
        employeeDetailsModelRules.push(new validator.PropertyRule("Position", {
            required: { message: "Position is required" } //,
           // minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));

        //TODO: Add more form validations
    };

    $scope.setupEmployeeDetails = function() {
        if ($scope.EmployeeId > 0) {

            viewModelHelper.apiGet('api/hr/getemployeeById/' + $scope.EmployeeId, null,
                function(result) {
                    $scope.employeeDetailsModel = result.data;
                });
        }

    };


    $scope.setupDatesOnSubmit = function ()
    {

        if ($scope.employeeDetailsModel.PassportExpiry !== null && $scope.employeeDetailsModel.PassportExpiry !== '') 
            $scope.employeeDetailsModel.PassportExpiry = moment($scope.employeeDetailsModel.PassportExpiry).format("DD/MM/YYYY").toString();
         else 
            $scope.employeeDetailsModel.PassportExpiry = '';

        if ($scope.employeeDetailsModel.EmiratesIDExpiry !== null && $scope.employeeDetailsModel.EmiratesIDExpiry !== '')
            $scope.employeeDetailsModel.EmiratesIDExpiry = moment($scope.employeeDetailsModel.EmiratesIDExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.employeeDetailsModel.EmiratesIDExpiry = '';

        if ($scope.employeeDetailsModel.VisaExpiry !== null && $scope.employeeDetailsModel.VisaExpiry !== '')
            $scope.employeeDetailsModel.VisaExpiry = moment($scope.employeeDetailsModel.VisaExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.employeeDetailsModel.VisaExpiry = '';

        if ($scope.employeeDetailsModel.InsuranceExpiry != null && $scope.employeeDetailsModel.InsuranceExpiry !== '')
            $scope.employeeDetailsModel.InsuranceExpiry = moment($scope.employeeDetailsModel.InsuranceExpiry).format("DD/MM/YYYY").toString();
        else
            $scope.employeeDetailsModel.InsuranceExpiry = '';

        if ($scope.employeeDetailsModel.JoiningDate != null && $scope.employeeDetailsModel.JoiningDate !== '')
            $scope.employeeDetailsModel.JoiningDate = moment($scope.employeeDetailsModel.JoiningDate).format("DD/MM/YYYY").toString();
        else
            $scope.employeeDetailsModel.JoiningDate = '';

    }


    $scope.submit = function () {

      $scope.setupDatesOnSubmit();
        validator.ValidateModel($scope.employeeDetailsModel, employeeDetailsModelRules);
        viewModelHelper.modelIsValid = $scope.employeeDetailsModel.isValid;
        viewModelHelper.modelErrors = $scope.employeeDetailsModel.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/hr/addemployee', $scope.employeeDetailsModel,
                function (result) {
                    $scope.viewMode = 'success';
                    var uri = 'hr/employees/';
                    window.location.href = window.GulfRuby.rootPath + uri;
                });
        }
        else {
  
            viewModelHelper.modelErrors = $scope.employeeDetailsModel.errors;
        }
    }

    $scope.openPassportExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedPassportExpiry = true;

    }

    $scope.openEmiratesExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedEmiratesExpiry = true;

    }


    $scope.openVisaExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedVisaExpiry = true;

    }


    $scope.openInsuranceExpiry = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedInsuranceExpiry = true;

    }

    $scope.openJoiningDate = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedJoiningDate = true;

    }


    setupRules();
   // $scope.setupEmployeeDetails();
});