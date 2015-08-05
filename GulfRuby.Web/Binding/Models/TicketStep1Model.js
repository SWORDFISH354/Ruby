(function (cr) {

    var ItineraryModel = function () {

        var self = this;
        self.ID = 0;
        self.BookingId = 0;
        self.Date = '';
        self.DateIsOpen = false;
        self.From = '';
        self.To = '';
        self.Carrier= '';
        self.FlightNo = '';
        self.DepTime = '';
        self.ArrTime = '';
        self.Status = '';
        self.Class = '';
        self.IsActive = '';
    };


    var PassengerInfo = function () {
        var self = this;
        self.ID = 0;
        self.BookingId = 0;
        self.FirstName = '';
        self.SecondName = '';
        self.Type = '';
        self.PassportNo = '';
        self.Nationality = '';
        self.AddedTime = '';
        self.AddedBy = '';
        self.IsActive = '';
    };


    var TicketStep1Model = function() {

        var self = this;

        self.ID = 0;
        self.InvoiceNumber = 0;
        self.IssueDate = '';
        self.CustomerType = '';
        self.CareOf = '';
        self.CorporateClient = '';
        self.CustomerName = '';
        self.ContactNumber = '';
        self.Email = '';
        self.Itineraries = [];
        self.Passengers = [];
        self.BaseFare = '';
        self.Tax = '';
        self.QuotedFare = '';
        self.Status = '0';
        self.ModeOfIssue = '';
        self.DueDate = '';
        self.AddedTime = '';
        self.LastModifiedBy = '';
        self.IsActive = '';
        // ToDO: Leave details

    };




    cr.PassengerInfo = PassengerInfo;
    cr.ItineraryModel = ItineraryModel;
    cr.TicketStep1Model = TicketStep1Model;
}(window.GulfRuby));