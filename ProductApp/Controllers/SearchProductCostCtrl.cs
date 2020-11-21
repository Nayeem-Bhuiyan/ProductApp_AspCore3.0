using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApp.Models;
using ProductApp.ViewModel;

namespace ProductApp.Controllers
{
    
    public class SearchProductCostCtrl : Controller
    {


        private readonly ProductDbContext db;


        public SearchProductCostCtrl(ProductDbContext _db)
        {
            this.db = _db;
        }









        // GET: SearchProductCostCtrl
        public ActionResult Index()
        {
            return View();
        }

        // POST: /SearchProductCostCtrl/Search_VmCostProductInfo
        [HttpPost]
        public async Task<IEnumerable<VmProductCostDetails>> Search_VmCostProductInfo([FromBody]VmSearchProductCost vm)
        {
            List<VmProductCostDetails> VmProductCostDetailData = new List<VmProductCostDetails>();

            var dataReceivedFromDatabase =await (from p in db.Product
                                            join pc in db.ProductCostInfo on p.ProductId equals pc.ProductId
                                            join pct in db.ProductCategory on pc.ProductCategoryId equals pct.ProductCategoryId
                                            join b in db.Brand on pc.BrandId equals b.BrandId
                                            join em in db.Employee on pc.EmployeeId equals em.EmployeeId
                                            //where p.ProductName.ToLower().ToString() == vm.ProductName.ToLower().ToString() || pct.ProductCategoryName.ToLower().ToString() == vm.ProductCategoryName.ToLower().ToString() || b.BrandName.ToLower().ToString() == vm.BrandName.ToLower().ToString() || em.EmployeeName.ToLower().ToString() == vm.EmployeeName.ToLower().ToString() || em.EmployeeMobileNo.ToLower().ToString() == vm.EmployeeMobileNo.ToLower().ToString()
                                            where em.EmployeeMobileNo.StartsWith(vm.EmployeeMobileNo)|| em.EmployeeMobileNo.Contains(vm.EmployeeMobileNo)|| em.EmployeeMobileNo==vm.EmployeeMobileNo
                                                 select new { b.BrandId, b.BrandName, em.EmployeeId, em.EmployeeName, em.EmployeeMobileNo, em.EmployeeAddress, p.ProductId, p.ProductName, p.ProductByteImage, p.ProductPicturePath, pct.ProductCategoryId, pct.ProductCategoryName, pc.ProductCostInfoId, pc.ProductSize, pc.UnitPrice, pc.Quantity, pc.TransportCost, pc.Discount, pc.DateOfOrder, pc.Subtotal, pc.GrandTotal, pc.CurrentPayment, pc.DueAmount }

                      ).ToListAsync();


            if (dataReceivedFromDatabase.Count()>0)
            {
                foreach (var data in dataReceivedFromDatabase)
                {
                    VmProductCostDetails vmData = new VmProductCostDetails();
                    vmData.BrandId = data.BrandId;
                    vmData.BrandName = data.BrandName;
                    vmData.CurrentPayment = data.CurrentPayment;
                    vmData.DateOfOrder = data.DateOfOrder;
                    vmData.Discount = data.Discount;
                    vmData.DueAmount = data.DueAmount;
                    vmData.EmployeeId = data.EmployeeId;
                    vmData.EmployeeName = data.EmployeeName;
                    vmData.EmployeeMobileNo = data.EmployeeMobileNo;
                    vmData.EmployeeAddress = data.EmployeeAddress;
                    vmData.GrandTotal = data.GrandTotal;
                    vmData.ProductCategoryId = data.ProductCategoryId;
                    vmData.ProductCategoryName = data.ProductCategoryName;
                    vmData.ProductCostInfoId = data.ProductCostInfoId;
                    vmData.ProductId = data.ProductId;
                    vmData.ProductName = data.ProductName;
                    vmData.ProductByteImage = data.ProductByteImage;
                    vmData.ProductPicturePath = data.ProductPicturePath;
                    vmData.ProductSize = data.ProductSize;
                    vmData.Quantity = data.Quantity;
                    vmData.Subtotal = data.Subtotal;
                    vmData.TransportCost = data.TransportCost;
                    vmData.UnitPrice = data.UnitPrice;

                    VmProductCostDetailData.Add(vmData);
                }

            }

            return VmProductCostDetailData;
        }

        // GET: SearchProductCostCtrl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchProductCostCtrl/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchProductCostCtrl/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchProductCostCtrl/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchProductCostCtrl/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchProductCostCtrl/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
