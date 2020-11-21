using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductApp.DAL;
using ProductApp.Models;
using ProductApp.ViewModel;



namespace ProductApp.Controllers
{
    public class CountryDistrictThana : Controller
    {
        private readonly IRepoproduct db;

        public CountryDistrictThana(IRepoproduct _db)
        {
            this.db = _db;
        }


        public IActionResult Index()
        {
            return View();
        }


        //GET:/CountryDistrictThana/LoadCountry
        public JsonResult LoadCountry()
        {

            return Json(db.CountryList());

        }



        //GET:/CountryDistrictThana/LoadDistrictByConId/5
        public JsonResult LoadDistrictByConId(int? id)
        {
            if (id==null)
            {
                id = 1;
            }
            return Json(db.DistrictListByCountryId(id));

        }

        //GET:/CountryDistrictThana/LoadThanaByDistId/5
        public JsonResult LoadThanaByDistId(int? id)
        {
            if (id==null)
            {
                id = 1;
            }
            return Json(db.ThanaListByDistrictId(id));

        }


    }
}
