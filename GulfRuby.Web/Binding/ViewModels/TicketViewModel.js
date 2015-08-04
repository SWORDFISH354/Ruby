appMainModule.controller("TicketViewModel", function($scope,$http,$location,viewModelHelper) {
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    $scope.viewModelHelper = viewModelHelper;
  

});


