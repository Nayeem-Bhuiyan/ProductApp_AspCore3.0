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
    
    public class ProductCostCtrl : Controller
    {
        private readonly IRepoproduct db;

        public ProductCostCtrl(IRepoproduct _db)
        {
            this.db = _db;
        }

        // GET: ProductCostCtrl
        public IActionResult Index()
        {
            return View();
        }


        // GET: /ProductCostCtrl/Index_ProductCostList
        public ActionResult Index_ProductCostList(int currentPage = 1, int pageSize = 3)
        {
            // get the data of current page
            IEnumerable<VmProductCostList> Db_Data = db.GetProductCostList().Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            //return totalPage and the data of ProductCostList
            return Json(new { totalPage = Math.Ceiling(1.0 * db.GetProductCostList().Count() / pageSize), data = Db_Data });
        }


        // GET: /ProductCostCtrl/ProductCostList
        [EnableCors("AllowOrigin")]
        public ActionResult ProductCostList()
        {
            // get the data of current page
            IEnumerable<VmProductCostList> Db_Data = db.GetProductCostList();
            //return totalPage and the data of ProductCostList
            return Json(db.GetProductCostList());
        }


        // GET: ProductCostCtrl/Details/5
        public JsonResult Details(int? id)
        {
            return Json(db.GetVmProductCostById(id));
        }


        // POST: ProductCostCtrl/SearchProductCostInfo
        [HttpPost]
        public JsonResult SearchProductCostInfo([FromBody]VmSearchProductCost Vmojb)
        {
            return Json(db.Search_VmCostProductInfo(Vmojb));
        }

        [HttpPost]
        // GET: /ProductCostCtrl/Create
        public JsonResult  Create([FromBody] ProductCostInfo pc)
        {

            db.PostProductCostInfo(pc);
            return Json("Inserted Data Successfully!!");
        }

 
        [HttpPost]
        // GET: /ProductCostCtrl/Edit
        public JsonResult Edit([FromBody] ProductCostInfo frmData)
        {
            db.UpdatePost_ProductCostInfo(frmData);
            return Json("Updated Data successfully!!"); 
        }

        // POST: /ProductCostCtrl/Delete/5
        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            db.RemovePost_ProductCostInfoByCostId(id);
            return Json("Deleted Data successfully!!");
        }


    }
}
