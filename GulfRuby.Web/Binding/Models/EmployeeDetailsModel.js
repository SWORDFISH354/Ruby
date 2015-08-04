(function (cr) {
    var EmployeeDetailsModel = function () {

        var self = this;

        self.Id = 0;
        self.Name = '';
        self.UserName = '';
        self.Position = '';
        self.PassportNumber = '';
        self.EmiratesID = '';
        self.VisaNumber = '';
        self.InsuranceNumber = '';
        self.JoiningDate = '';
        self.BasicSalary = '';
        self.Allowance = '';
        self.PassportExpiry = '';
        self.EmiratesIDExpiry = '';
        self.VisaExpiry = '';
        self.InsuranceExpiry = '';
        self.IsActive = '';
        // ToDO: Leave details

    }
    cr.EmployeeDetailsModel = EmployeeDetailsModel;
}(window.GulfRuby));