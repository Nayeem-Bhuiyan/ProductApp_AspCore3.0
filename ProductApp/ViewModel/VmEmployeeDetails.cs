using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.ViewModel
{
    public class VmEmployeeDetails
    {

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeMobileNo { get; set; }
        public string Email { get; set; }

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public int ThanaId { get; set; }
        public string ThanaName { get; set; }



    }
}
