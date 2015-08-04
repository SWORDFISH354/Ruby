appMainModule.controller("AllEmployeesViewModel", function ($scope, $http, $location, viewModelHelper) {
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    $scope.viewModelHelper = viewModelHelper;
    $scope.Employees = [];
    $scope.displayedCollection = [];



    $scope.GetAllEmployees = function () {

        viewModelHelper.apiGet('api/hr/getemployees', null,
                function (result) {
                    $scope.Employees = result.data;
                    $scope.displayedCollection = [].concat($scope.Employees);
                });
    }


    //remove to the real data holder
    $scope.SelectEmployee = function(row) {
        var index = $scope.Employees.indexOf(row);
        if (index !== -1) {
            var uri = 'hr/employee/';
            window.location.href = window.GulfRuby.rootPath + uri + $scope.Employees[index].Id;
        }
    }


    $scope.GetAllEmployees();
   
});