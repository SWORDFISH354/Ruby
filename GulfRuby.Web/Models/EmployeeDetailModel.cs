using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GulfRuby.Web.Models
{
    public class EmployeeDetailModel
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string PassportNumber { get; set; }

        public string PassportExpiry { get; set; }

        public string EmiratesID { get; set; }

        public string EmiratesIDExpiry { get; set; }

        public string VisaNumber { get; set; }

        public string VisaExpiry { get; set; }

        public string InsuranceNumber { get; set; }

        public string InsuranceExpiry { get; set; }

        public string JoiningDate { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowance { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime AddedTime { get; set; }

        public bool IsActive { get; set; }

    }
}