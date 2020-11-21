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
    
    public class ProductSellCtrl : Controller
    {
        private readonly IRepoproduct db;

        public ProductSellCtrl(IRepoproduct _db)
        {
            this.db = _db;
        }


        // GET: ProductSellCtrl/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductSellCtrl/ProductSell_Index
        public ActionResult ProductSell_Index(int currentPage = 1, int pageSize = 3)
        {
            IEnumerable<VmProductSellDetails> Db_Data = db.GetProductSellInfoList().Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return Json(new { totalPage = Math.Ceiling(1.0 * db.GetProductSellInfoList().Count() / pageSize), data = Db_Data });
        }




        // GET: ProductSellCtrl/Details/5
        public JsonResult Details(int id)
        {
            return Json(db.GetVmProductSellDetailsBy_ProductSellInfoId(id));
        }

        // POST: ProductSellCtrl/Create
        [HttpPost]
        public JsonResult Create([FromBody] ProductSellInfo frmObj)
        {
            db.PostProductSellInfo(frmObj);
            return Json("Inserted Data Successfully!!");
        }

        // POST: ProductSellCtrl/Edit
        [HttpPost]
        public JsonResult Edit_ProductSellInfo([FromBody] ProductSellInfo frmData )
        {
            db.UpdatePost_ProductSellInfo(frmData);
            return Json("Updated Data Successfully!!");
        }

        // POST: ProductSellCtrl/SearchSellDetails
        [HttpPost]
        public JsonResult SearchProductSellDetails([FromBody] VmSearchProductSellInfo frmData)
        {
            return Json(db.Search_SellInfoBy_EmployeeAndCustomerMobile(frmData));
        }

        // POST: ProductSellCtrl/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            db.RemovePost_ProductSellInfoBySellId(id);

            return Json("Deleted Data Successfully!!");
        }

       
    }
}
