using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductApp.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using ProductApp.DAL;

namespace ProductApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepoproduct db;

        public HomeController(IRepoproduct _db)
        {
            this.db = _db;
        }


        //GET:/Home/LoadEmployee
        public JsonResult LoadEmployee()
        {
            return Json(db.EmployeeList());
        }

        //GET:/Home/LoadCustomer
        public JsonResult LoadCustomer()
        {
            return Json(db.CustomerList());
        }



        //Product Data

        //GET:/Home/LoadProduct
        public JsonResult LoadProduct()
        {
            return Json(db.AllProductList());
        }

        //GET:/Home/LoadBrand/id
        public JsonResult LoadBrand(int? id)
        {
            return Json(db.BrandListByProductId(id));
        }

        //GET:/Home/LoadProductCategory/id
        public JsonResult LoadProductCategory(int? id)
        {
            return Json(db.ProductCategoryListByBrandId(id));
        }



        //Country Data

        //GET:/Home/LoadCountry
        public JsonResult LoadCountry()
        {
            return Json(db.CountryList());
        }

        //GET:/Home/LoadDistrict/id
        public JsonResult LoadDistrict(int? id)
        {
            return Json(db.DistrictListByCountryId(id));
        }

        //GET:/Home/LoadThana/id
        public JsonResult LoadThana(int? id)
        {
            return Json(db.ThanaListByDistrictId(id));
        }


















        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
