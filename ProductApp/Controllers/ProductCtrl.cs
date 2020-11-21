using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductApp.DAL;
using ProductApp.Models;
using ProductApp.ViewModel;

namespace ProductApp.Controllers
{
    
    public class ProductCtrl : Controller
    {

        private readonly IRepoproduct db;

        public ProductCtrl(IRepoproduct _db)
        {
            this.db = _db;
        }



        //public FileResult GetFileFromBytes(byte[] bytesIn)
        //{
        //    return File(bytesIn, "image/png");
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetProductImage()
        //{
        //    List<byte[]> ProductImages = new List<byte[]>();
        //    var images = await context.Product.Select(p => p.ProductByteImage).to();
        //    ProductImages = images;
        //    for (int i = 0; i <= ProductImages.Count; i++)
        //    {
        //        FileResult ImageOfProduct = GetFileFromBytes();
        //    }
            
        //    return;
        //}

        // GET: ProductCtrl
        public IActionResult Index()
        {
            return View();
        }

        // GET: /ProductCtrl/IndexDisplay
        public ActionResult IndexDisplay()
        {
            
            return View(db.ProductList());
        }


        // GET: ProductCtrl/Details/5
        public IActionResult Details(int? id)
        {

            return View(db.GetProductDetails(id));
        }


        // GET: /ProductCtrl/Create
        //[HttpGet]
        //public ActionResult Create()
        //{

        //    ViewBag.ProductCategoryId = new SelectList(db.ProductCategoryListByBrandId(), "ProductCategoryId", "ProductCategoryName", "Select Category");
        //    ViewBag.BrandId = new SelectList(db.BrandList(), "BrandId", "BrandName", "Select brand");
        //    return View();
        //}




        [HttpPost]
        public ActionResult Create(VmProduct_Brand_Category model)
        {
            db.PostProductBandCategory(model);
            return RedirectToAction("IndexDisplay");
        }


        // POST: /ProductCtrl/CreateProduct
        [HttpPost]
        public JsonResult CreateProduct(VmProduct_Brand_Category model)
        {
            db.PostProductBandCategory(model);
            return Json("Inserted data successfully");
        }


        //public JsonResult GetEditProductBrandCategory(int? id)
        //{
        //    ViewBag.ProductCategoryId = new SelectList(db.ProductCategoryListByBrandId(id), "ProductCategoryId", "ProductCategoryName", "Select Category");
        //    ViewBag.BrandId = new SelectList(db.BrandList(), "BrandId", "BrandName", "Select brand");

        //    return Json(db.GetProductDetails(id));
        //}

        //// GET: ProductCtrl/Edit/5
        //public IActionResult Edit(int? id)
        //{

        //    ViewBag.ProductCategoryId = new SelectList(db.ProductCategoryList(), "ProductCategoryId", "ProductCategoryName","Select Category");
        //    ViewBag.BrandId = new SelectList(db.BrandList(), "BrandId", "BrandName","Select brand");
            
        //    return View(db.GetProductDetails(id));
        //}

        // POST: ProductCtrl/Edit/5
        [HttpPost]
        public IActionResult Edit(VmProduct_Brand_Category model)
        {
            db.EditPostProductBandCategory(model);
            return RedirectToAction("IndexDisplay");
        }

        // GET: ProductCtrl/Delete/5
        public IActionResult Delete(int? id)
        {
            return View(db.GetProductDetails(id));
        }

        // POST: ProductCtrl/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
           string reponse =db.RemoveProductRecord(id);
            return RedirectToAction("IndexDisplay");
        }

        
    }
}
