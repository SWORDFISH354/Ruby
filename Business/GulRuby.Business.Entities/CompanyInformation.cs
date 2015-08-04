using System;
using Core.Common.Contracts;
using Core.Common.Core;

namespace GulRuby.Business.Entities
{
    public class CompanyInformation : ObjectBase, IIdentifiableEntity
    {
        [NotNavigable]
        public int EntityId
        {
            get { return (int)ID; }
            set { ID = (byte)value; }
        }

        public byte ID { get; set; }

        public string TourismLicense { get; set; }

        public DateTime TourismExpiry { get; set; }

        public string Rent { get; set; }

        public DateTime RentExpiry { get; set; }

        public string WasteNumber { get; set; }

        public DateTime WasteNumberExpiry { get; set; }


        public string CivilDefence { get; set; }

        public DateTime CivilDefenceExpiry { get; set; }

        public string Sponsorship { get; set; }

        public DateTime SponsorshipExpiry { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime AddedTime { get; set; }


    }
}