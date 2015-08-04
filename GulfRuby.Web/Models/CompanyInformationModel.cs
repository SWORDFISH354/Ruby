using System;

namespace GulfRuby.Web.Models
{
    public class CompanyInformationModel
    {

        public byte ID { get; set; }

        public string TourismLicense { get; set; }

        public string TourismExpiry { get; set; }

        public string Rent { get; set; }

        public string RentExpiry { get; set; }

        public string WasteNumber { get; set; }

        public string WasteNumberExpiry { get; set; }

        public string CivilDefence { get; set; }

        public string CivilDefenceExpiry { get; set; }

        public string Sponsorship { get; set; }

        public string SponsorshipExpiry { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime AddedTime { get; set; }

    }
}