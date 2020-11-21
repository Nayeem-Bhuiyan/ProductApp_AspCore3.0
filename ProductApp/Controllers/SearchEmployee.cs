using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApp.Models;
using ProductApp.ViewModel;

namespace ProductApp.Controllers
{
    public class SearchEmployee : Controller
    {

        private readonly ProductDbContext db;
  


        public SearchEmployee(ProductDbContext _db)
        {
            this.db = _db;

        }

        [HttpPost]
        
        public IEnumerable<VmEmployeeDetails> SearchEmployeeRecord([FromBody] VmSearchEmployeeRecord SearchParameters)
        {
            
                List<VmEmployeeDetails> SearchEmpResultList = new List<VmEmployeeDetails>();

           var DbObj = (from emp in db.Employee
                         join gn in db.Gender on emp.GenderId equals gn.GenderId
                         join cn in db.Country on emp.CountryId equals cn.CountryId
                         join ds in db.District on emp.DistrictId equals ds.DistrictId
                         join th in db.Thana on emp.ThanaId equals th.ThanaId
                         where emp.EmployeeAddress.ToString().ToLower().StartsWith(SearchParameters.EmployeeAddress.ToString().ToLower()) || emp.EmployeeMobileNo.ToString().ToLower().StartsWith(SearchParameters.EmployeeMobileNo.ToString().ToLower()) || emp.EmployeeName.ToString().ToLower().StartsWith(SearchParameters.EmployeeName.ToString().ToLower()) || emp.Email.ToString().ToLower().StartsWith(SearchParameters.Email.ToString().ToLower())
                         select new 
                         {
                             EmployeeId = emp.EmployeeId,
                             EmployeeName = emp.EmployeeName,
                             EmployeeAddress = emp.EmployeeAddress,
                             EmployeeMobileNo = emp.EmployeeMobileNo,
                             Email = emp.Email,

                             GenderId = gn.GenderId,
                             GenderName = gn.GenderName,

                             CountryId = cn.CountryId,
                             CountryName = cn.CountryName,

                             DistrictId = ds.DistrictId,
                             DistrictName = ds.DistrictName,

                             ThanaId = th.ThanaId,
                             ThanaName = th.ThanaName
                         }).ToList();




            foreach (var data in DbObj)
            {
                VmEmployeeDetails empRecord = new VmEmployeeDetails();

                empRecord.EmployeeId = data.EmployeeId;
                empRecord.EmployeeName = data.EmployeeName;
                empRecord.EmployeeAddress = data.EmployeeAddress;
                empRecord.EmployeeMobileNo = data.EmployeeMobileNo;
                empRecord.Email = data.Email;
                empRecord.GenderId = data.GenderId;
                empRecord.GenderName = data.GenderName;
                empRecord.CountryId = data.CountryId;
                empRecord.CountryName = data.CountryName;
                empRecord.DistrictId = data.DistrictId;
                empRecord.DistrictName = data.DistrictName;
                empRecord.ThanaId = data.ThanaId;
                empRecord.ThanaName = data.ThanaName;
                SearchEmpResultList.Add(empRecord);
            }

            return SearchEmpResultList;
        }

    }
}
