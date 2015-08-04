using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;

namespace GulRuby.Business.Entities
{
    public class Employee : ObjectBase, IIdentifiableEntity
    {
        [NotNavigable]
        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }

        public int Id { get; set; }

        public string Name { get; set; }    

        public string Position { get; set; }

        public string PassportNumber { get; set; }

        public DateTime? PassportExpiry { get; set; }

        public string EmiratesID { get; set; }

        public DateTime? EmiratesIDExpiry { get; set; }

        public string VisaNumber { get; set; }

        public DateTime? VisaExpiry { get; set; }

        public string InsuranceNumber { get; set; }

        public DateTime? InsuranceExpiry { get; set; }

        public DateTime? JoiningDate { get; set; }

        public decimal? BasicSalary { get; set; }

        public decimal? Allowance { get; set; }

        public bool IsActive { get; set; }

        public string UserName { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime AddedTime { get; set; }


    }
}
