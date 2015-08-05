appMainModule.controller("TicketViewModel", function($scope,$http,$location,viewModelHelper) {
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    $scope.viewModelHelper = viewModelHelper;
    $scope.Bookings = [];
    $scope.displayedCollection = [];


    $scope.GetPendingBookings = function () {

        viewModelHelper.apiGet('api/ticket/pendingBookingTickets', null,
                function (result) {
                    $scope.Bookings = result.data;
                    $scope.displayedCollection = [].concat($scope.Bookings);
                });
    }


    //remove to the real data holder
    $scope.SelectBooking = function (row) {
        var index = $scope.Bookings.indexOf(row);
        if (index !== -1) {
            var uri = 'tickets/ticket/';
            window.location.href = window.GulfRuby.rootPath + uri + $scope.Bookings[index].ID;
        }
    }


    $scope.GetPendingBookings();

});


