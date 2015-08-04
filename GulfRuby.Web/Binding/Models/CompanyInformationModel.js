(function (cr) {
    var CompanyInformationModel = function () {

        var self = this;
        self.ID = 0;
        self.TourismLicense = '';
        self.TourismExpiry = '';
        self.Rent = '';
        self.RentExpiry = '';
        self.WasteNumber = '';
        self.WasteNumberExpiry = '';
        self.CivilDefence = '';
        self.CivilDefenceExpiry = '';
        self.Sponsorship = '';
        self.SponsorshipExpiry = '';
        self.AddedTime = '';
        self.LastModifiedBy = '';
    }
    cr.CompanyInformationModel = CompanyInformationModel;
}(window.GulfRuby));