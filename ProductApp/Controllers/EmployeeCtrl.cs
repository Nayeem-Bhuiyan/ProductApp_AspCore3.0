using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductApp.DAL;
using ProductApp.Models;
using ProductApp.ViewModel;

namespace ProductApp.Controllers
{
    
    public class EmployeeCtrl : Controller
    {
        private readonly IRepoproduct db;

        public EmployeeCtrl(IRepoproduct _db)
        {
            this.db = _db;
        }



    
        //public ActionResult Index_Employee()
        //{
           
        //    return Json(db.EmployeeListaData());
        //}
        [HttpGet]
        public ActionResult Index_Employee(int currentPage = 1, int pageSize = 5)
        {
            // get the data of current page
            IEnumerable<VmEmployeeDetails> Db_Data = db.EmployeeListaData().Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            //return totalPage and the data of ProductCostList
            return Json(new { totalPage = Math.Ceiling(1.0 * db.EmployeeListaData().Count() / pageSize), data = Db_Data });
        }


        //POST:/EmployeeCtrl/SearchEmployee
        [HttpPost]
        public ActionResult SearchEmployee([FromBody] VmSearchEmployeeRecord vm)
        {
            //return Json(db.SearchEmployeeRecord(vm));
            return Json(db.SearchEmployeeData(vm));

        }



        // POST: EmployeeCtrl/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            db.DeleteEmployeeRecord(id);
            return Json("Deleted Data Successfully!!");
        }



        // GET: EmployeeCtrl/Details/5
        public IActionResult Details(int? id)
        {

            return Json(db.GetEmpRecordById(id));
        }

        //[HttpPost]
        //// POST: /EmployeeCtrl/SearchEmployee
        //public IActionResult SearchEmployee([FromBody] VmSearchEmployeeRecord searchPeram)
        //{
        //    return Json(db.SearchEmployeeRecord(searchPeram));
        //}

        // POST: /EmployeeCtrl/Create
        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            db.CreateNewEmployee(employee);
            return Json("Inserted Data Successfully!!");
        }



        // POST: EmployeeCtrl/Edit

        [HttpPost]
        public IActionResult Edit([FromBody] Employee employee)
        {
            db.EditPostEmployee(employee);
            return Json("Updated Data successfully!!");
        }




        // GET: EmployeeCtrl
        public IActionResult Index()
        {

            return View();
        }

    }
}
