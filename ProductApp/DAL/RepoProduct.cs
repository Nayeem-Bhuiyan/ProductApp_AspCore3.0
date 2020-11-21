using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Models;
using ProductApp.ViewModel;
using ProductApp.DAL;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace ProductApp.DAL
{
    public class RepoProduct : IRepoproduct
    {

        private readonly ProductDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;


        public RepoProduct(ProductDbContext _db, IWebHostEnvironment hostEnvironment)
        {
            this.db = _db;
            webHostEnvironment = hostEnvironment;
        }



        //Customer Controller Method



        public Customer SearchCustomerByCustomerMobileNo(Customer vm)
        {
            Customer cust = new Customer();

            var Dbobj = db.Customer.Where(c => c.CustomerMobile.Contains(vm.CustomerMobile) || c.CustomerMobile.StartsWith(vm.CustomerMobile)||(c.CustomerMobile==vm.CustomerMobile)).FirstOrDefault();
            if (Dbobj!=null)
            {


                cust.CustomerAddress = Dbobj.CustomerAddress;
                cust.CustomerId = Dbobj.CustomerId;
                cust.CustomerMobile = Dbobj.CustomerMobile;
                cust.CustomerName = Dbobj.CustomerName;
                
            }

            return cust;
        }






        public string DeleteCustomer(int? id)
        {
            string msg = "";

            Customer DbObj = db.Customer.Where(c => c.CustomerId == id).FirstOrDefault();

            if (DbObj != null)
            {


                db.Customer.Remove(DbObj);
                db.SaveChanges();

                msg = "Deleted Data successfully";
            }
            else
            {
                msg = "Data can not Deleted";
            }

            return msg;

        }



        public string EditPostCustomer(int? id,Customer cm)
        {
            string msg = "";

            Customer DbObj = db.Customer.Where(c => c.CustomerId == id).FirstOrDefault();

            if (cm.CustomerName != null && cm.CustomerMobile != null && cm.CustomerAddress != null)
            {
                DbObj.CustomerAddress = cm.CustomerAddress;
                DbObj.CustomerMobile = cm.CustomerMobile;
                DbObj.CustomerName = cm.CustomerName;

                db.Entry(DbObj).State = EntityState.Modified;
                db.SaveChanges();

                msg = "Updated Data successfully";
            }
            else
            {
                msg = "Data can not Updated";
            }

            return msg;

        }


        public string CreateNewCustomer(Customer cm)
        {
            string msg = "";

            if (cm.CustomerName!=null&& cm.CustomerMobile != null&& cm.CustomerAddress != null)
            {
                db.Customer.Add(cm);
                db.SaveChanges();

                msg = "Inserted Data successfully";
            }
            else
            {
                msg = "Data can not Saved";
            }

            return msg;

        }


       public Customer CustomerByCustomerId(int? id)
        {
            return db.Customer.Where(c => c.CustomerId == id).FirstOrDefault();
        }










        //Drop Data Collection

        public IEnumerable<Customer> CustomerList()
        {
            return db.Customer.ToList();
        }

        public IEnumerable<Employee> EmployeeList()
        {
            return db.Employee.ToList();
        }

        public IEnumerable<Country> CountryList()
        {
            return db.Country.ToList();
        }


        public IEnumerable<District> DistrictListByCountryId(int? countryId)
        {

            List<District> ListOfDistrict = db.District.Where(c => c.CountryId == countryId).ToList();

            return ListOfDistrict;
        }

        public IEnumerable<Thana> ThanaListByDistrictId(int? districtId)
        {

            List<Thana> ListOfThana = db.Thana.Where(t => t.DistrictId == districtId).ToList();

            return ListOfThana;
        }

        public IEnumerable<Product> AllProductList()
        {
            return db.Product.ToList();
        }

        public IEnumerable<Brand> BrandListByProductId(int? productId)
        {

            List<Brand> ListOfBrand = db.Brand.Where(b => b.ProductId == productId).ToList();
            return ListOfBrand;
        }

        public IEnumerable<ProductCategory> ProductCategoryListByBrandId(int? brandId)
        {
            List<ProductCategory> pcList = db.ProductCategory.Where(pc => pc.BrandId == brandId).ToList();
            return pcList;
        }

        public IEnumerable<VmEmployeeDetails> SearchEmployeeRecord(VmSearchEmployeeRecord SearchParameters)
        {

            List<VmEmployeeDetails> SearchEmpResultList = new List<VmEmployeeDetails>();

            var DbObj = (from emp in db.Employee
                         join cn in db.Country on emp.CountryId equals cn.CountryId
                         join gn in db.Gender on emp.GenderId equals gn.GenderId
                         join ds in db.District on emp.DistrictId equals ds.DistrictId
                         join th in db.Thana on emp.ThanaId equals th.ThanaId
                         where emp.EmployeeAddress.ToString().ToLower().StartsWith(SearchParameters.EmployeeAddress.ToString().ToLower()) || emp.EmployeeMobileNo.ToString().ToLower().StartsWith(SearchParameters.EmployeeMobileNo.ToString().ToLower()) || emp.EmployeeName.ToString().ToLower().StartsWith(SearchParameters.EmployeeName.ToString().ToLower()) || emp.Email.ToString().ToLower().StartsWith(SearchParameters.Email.ToString().ToLower())
                         select new VmEmployeeDetails()
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


        public IEnumerable<Employee> SearchEmployeeData(VmSearchEmployeeRecord SearchParameters)
        {
            var DbObj = db.Employee.Where(em => em.EmployeeName.ToLower().ToString().StartsWith(SearchParameters.EmployeeName.ToLower().ToString())).Select(emp => new { emp.EmployeeId, emp.EmployeeAddress, emp.EmployeeMobileNo, emp.EmployeeName, emp.Email }).ToList();
                     

            List<Employee> SearchEmpResultList = new List<Employee>();
            foreach (var data in DbObj)
            {
                Employee empRecord = new Employee();

                empRecord.EmployeeId = data.EmployeeId;
                empRecord.EmployeeName = data.EmployeeName;
                empRecord.EmployeeAddress = data.EmployeeAddress;
                empRecord.EmployeeMobileNo = data.EmployeeMobileNo;
                empRecord.Email = data.Email;
                SearchEmpResultList.Add(empRecord);
            }


            return SearchEmpResultList;
        }


        public void CreateNewEmployee(Employee emp)
        {
            db.Employee.Add(emp);
            db.SaveChanges();
        }

        public void EditPostEmployee(Employee empObj)
        {
            var data = (from emp in db.Employee
                        join cn in db.Country on emp.CountryId equals cn.CountryId
                        join gn in db.Gender on emp.GenderId equals gn.GenderId
                        join ds in db.District on cn.CountryId equals ds.CountryId
                        join th in db.Thana on ds.DistrictId equals th.DistrictId
                        where emp.EmployeeId == empObj.EmployeeId
                        select new Employee()
                        {
                            EmployeeId = emp.EmployeeId,
                            EmployeeName = emp.EmployeeName,
                            EmployeeAddress = emp.EmployeeAddress,
                            EmployeeMobileNo = emp.EmployeeMobileNo,
                            Email = emp.Email,

                            GenderId = gn.GenderId,
                            CountryId = cn.CountryId,
                            DistrictId = ds.DistrictId,
                            ThanaId = th.ThanaId,

                        }).FirstOrDefault();





            data.EmployeeId = empObj.EmployeeId;
            data.EmployeeName = empObj.EmployeeName;
            data.EmployeeAddress = empObj.EmployeeAddress;
            data.EmployeeMobileNo = empObj.EmployeeMobileNo;
            data.Email = empObj.Email;
            data.GenderId = empObj.GenderId;
            data.CountryId = Convert.ToInt32(empObj.CountryId);
            data.DistrictId = Convert.ToInt32(empObj.DistrictId);
            data.ThanaId = Convert.ToInt32(empObj.ThanaId);
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();

        }
        public VmEmployeeDetails GetEmpRecordById(int? id)
        {


            var data = (from emp in db.Employee
                        join cn in db.Country on emp.CountryId equals cn.CountryId
                        join gn in db.Gender on emp.GenderId equals gn.GenderId
                        join ds in db.District on emp.DistrictId equals ds.DistrictId
                        join th in db.Thana on emp.ThanaId equals th.ThanaId
                        where emp.EmployeeId == id
                        select new VmEmployeeDetails()
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
                        }).FirstOrDefault();



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

            return empRecord;
        }




        public void DeleteEmployeeRecord(int? id)
        {
            Employee EmpRecord = db.Employee.Where(e => e.EmployeeId == id).FirstOrDefault();

            db.Employee.Remove(EmpRecord);
            db.SaveChanges();

        }


        public IEnumerable<VmEmployeeDetails> EmployeeListaData()
        {
            var DbObj = (from emp in db.Employee
                         join cn in db.Country on emp.CountryId equals cn.CountryId
                         join gn in db.Gender on emp.GenderId equals gn.GenderId
                         join ds in db.District on emp.DistrictId equals ds.DistrictId
                         join th in db.Thana on emp.ThanaId equals th.ThanaId
                         select new VmEmployeeDetails()
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

            List<VmEmployeeDetails> empList = new List<VmEmployeeDetails>();
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
                empList.Add(empRecord);
            }

            return empList;
        }


        public IEnumerable<VmProductCostList> GetProductCostList()
        {

            List<VmProductCostList> vmProductCostListData = new List<VmProductCostList>();

            var dataReceivedFromDatabase = (from p in db.Product
                                            join pc in db.ProductCostInfo on p.ProductId equals pc.ProductId
                                            join pct in db.ProductCategory on pc.ProductCategoryId equals pct.ProductCategoryId
                                            join b in db.Brand on pc.BrandId equals b.BrandId
                                            join em in db.Employee on pc.EmployeeId equals em.EmployeeId
                                            select new { b.BrandId, b.BrandName, em.EmployeeId, em.EmployeeName, p.ProductId, p.ProductName, pct.ProductCategoryId, pct.ProductCategoryName, pc.ProductCostInfoId, pc.ProductSize, pc.UnitPrice, pc.Quantity, pc.TransportCost, pc.Discount, pc.DateOfOrder, pc.Subtotal, pc.GrandTotal, pc.CurrentPayment, pc.DueAmount }

                      ).ToList();

            foreach (var data in dataReceivedFromDatabase)
            {
                VmProductCostList vm = new VmProductCostList();
                vm.BrandId = data.BrandId;
                vm.BrandName = data.BrandName;
                vm.CurrentPayment = data.CurrentPayment;
                vm.DateOfOrder = data.DateOfOrder;
                vm.Discount = data.Discount;
                vm.DueAmount = data.DueAmount;
                vm.EmployeeId = data.EmployeeId;
                vm.EmployeeName = data.EmployeeName;
                vm.GrandTotal = data.GrandTotal;
                vm.ProductCategoryId = data.ProductCategoryId;
                vm.ProductCategoryName = data.ProductCategoryName;
                vm.ProductCostInfoId = data.ProductCostInfoId;
                vm.ProductId = data.ProductId;
                vm.ProductName = data.ProductName;
                vm.ProductSize = data.ProductSize;
                vm.Quantity = data.Quantity;
                vm.Subtotal = data.Subtotal;
                vm.TransportCost = data.TransportCost;
                vm.UnitPrice = data.UnitPrice;

                vmProductCostListData.Add(vm);
            }


            return vmProductCostListData;
        }



        public VmProductCostDetails GetVmProductCostById(int? id)
        {
            var data = (from p in db.Product
                        join pc in db.ProductCostInfo on p.ProductId equals pc.ProductId
                        join pct in db.ProductCategory on pc.ProductCategoryId equals pct.ProductCategoryId
                        join b in db.Brand on pc.BrandId equals b.BrandId
                        join em in db.Employee on pc.EmployeeId equals em.EmployeeId
                        where pc.ProductCostInfoId == id
                        select new { b.BrandId, b.BrandName, em.EmployeeId, em.EmployeeName, em.EmployeeMobileNo, em.EmployeeAddress, p.ProductId, p.ProductName, p.ProductPicturePath, p.ProductByteImage, pct.ProductCategoryId, pct.ProductCategoryName, pc.ProductCostInfoId, pc.ProductSize, pc.UnitPrice, pc.Quantity, pc.TransportCost, pc.Discount, pc.DateOfOrder, pc.Subtotal, pc.GrandTotal, pc.CurrentPayment, pc.DueAmount }

                      ).FirstOrDefault();

            VmProductCostDetails vm = new VmProductCostDetails();
            vm.BrandId = data.BrandId;
            vm.BrandName = data.BrandName;
            vm.EmployeeAddress = data.EmployeeAddress;
            vm.EmployeeMobileNo = data.EmployeeMobileNo;
            vm.CurrentPayment = data.CurrentPayment;
            vm.DateOfOrder = data.DateOfOrder;
            vm.Discount = data.Discount;
            vm.DueAmount = data.DueAmount;
            vm.EmployeeId = data.EmployeeId;
            vm.EmployeeName = data.EmployeeName;
            vm.GrandTotal = data.GrandTotal;
            vm.ProductCategoryId = data.ProductCategoryId;
            vm.ProductCategoryName = data.ProductCategoryName;
            vm.ProductCostInfoId = data.ProductCostInfoId;
            vm.ProductId = data.ProductId;
            vm.ProductName = data.ProductName;
            vm.ProductPicturePath = data.ProductPicturePath;
            vm.ProductByteImage = data.ProductByteImage;

            vm.ProductSize = data.ProductSize;
            vm.Quantity = data.Quantity;
            vm.Subtotal = data.Subtotal;
            vm.TransportCost = data.TransportCost;
            vm.UnitPrice = data.UnitPrice;


            return vm;
        }
        public void UpdatePost_ProductCostInfo(ProductCostInfo data)
        {
            ProductCostInfo Db_EditRecord = db.ProductCostInfo.Where(c => c.ProductCostInfoId == data.ProductCostInfoId).FirstOrDefault();

            Db_EditRecord.BrandId = data.BrandId;
            Db_EditRecord.CurrentPayment = data.CurrentPayment;
            Db_EditRecord.DateOfOrder = data.DateOfOrder;
            Db_EditRecord.Discount = data.Discount;
            Db_EditRecord.DueAmount = data.DueAmount;
            Db_EditRecord.EmployeeId = data.EmployeeId;
            Db_EditRecord.GrandTotal = data.GrandTotal;
            Db_EditRecord.ProductCategoryId = data.ProductCategoryId;
            Db_EditRecord.ProductCostInfoId = data.ProductCostInfoId;
            Db_EditRecord.ProductId = data.ProductId;
            Db_EditRecord.ProductSize = data.ProductSize;
            Db_EditRecord.Quantity = data.Quantity;
            Db_EditRecord.Subtotal = data.Subtotal;
            Db_EditRecord.TransportCost = data.TransportCost;
            Db_EditRecord.UnitPrice = data.UnitPrice;

            db.Entry(Db_EditRecord).State = EntityState.Modified;
            db.SaveChanges();

        }


        public void PostProductCostInfo(ProductCostInfo vm)
        {
            ProductCostInfo ProductCostData = new ProductCostInfo()
            {
                ProductId = vm.ProductId,
                BrandId = vm.BrandId,
                ProductCategoryId = vm.ProductCategoryId,
                ProductSize = vm.ProductSize,
                UnitPrice = vm.UnitPrice,
                Quantity = vm.Quantity,
                TransportCost = vm.TransportCost,
                Discount = vm.Discount,
                DateOfOrder = vm.DateOfOrder,
                Subtotal = (vm.UnitPrice * vm.Quantity) - vm.Discount,
                GrandTotal = vm.GrandTotal,
                CurrentPayment = vm.CurrentPayment,
                DueAmount = vm.DueAmount,
                EmployeeId = vm.EmployeeId

            };

            db.ProductCostInfo.Add(ProductCostData);
            db.SaveChanges();

        }



        public void RemovePost_ProductCostInfoByCostId(int? id)
        {
            ProductCostInfo Db_Record = db.ProductCostInfo.Where(c => c.ProductCostInfoId == id).FirstOrDefault();
            db.Remove(Db_Record);
            db.SaveChanges();
        }


        public IEnumerable<VmProductCostDetails> Search_VmCostProductInfo(VmSearchProductCost vm)
        {
            List<VmProductCostDetails> VmProductCostDetailData = new List<VmProductCostDetails>();

            var dataReceivedFromDatabase = (from p in db.Product
                                            join pc in db.ProductCostInfo on p.ProductId equals pc.ProductId
                                            join pct in db.ProductCategory on pc.ProductCategoryId equals pct.ProductCategoryId
                                            join b in db.Brand on pc.BrandId equals b.BrandId
                                            join em in db.Employee on pc.EmployeeId equals em.EmployeeId
                                            //where p.ProductName.ToLower().ToString()==vm.ProductName.ToLower().ToString() ||pct.ProductCategoryName.ToLower().ToString()==vm.ProductCategoryName.ToLower().ToString() || b.BrandName.ToLower().ToString()==vm.BrandName.ToLower().ToString() || em.EmployeeName.ToLower().ToString()==vm.EmployeeName.ToLower().ToString() || em.EmployeeMobileNo.ToLower().ToString()==vm.EmployeeMobileNo.ToLower().ToString()
                                            where em.EmployeeMobileNo.StartsWith(vm.EmployeeMobileNo)
                                            select new { b.BrandId, b.BrandName, em.EmployeeId, em.EmployeeName, em.EmployeeMobileNo, em.EmployeeAddress, p.ProductId, p.ProductName, p.ProductByteImage, p.ProductPicturePath, pct.ProductCategoryId, pct.ProductCategoryName, pc.ProductCostInfoId, pc.ProductSize, pc.UnitPrice, pc.Quantity, pc.TransportCost, pc.Discount, pc.DateOfOrder, pc.Subtotal, pc.GrandTotal, pc.CurrentPayment, pc.DueAmount }

                      ).ToList();

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


            return VmProductCostDetailData;


        }





        public IEnumerable<VmProductCostDetails> CostProductList()
        {


            List<VmProductCostDetails> VmProductCostDetailData = new List<VmProductCostDetails>();

            var dataReceivedFromDatabase = (from p in db.Product
                                            join pc in db.ProductCostInfo on p.ProductId equals pc.ProductId
                                            join pct in db.ProductCategory on pc.ProductCategoryId equals pct.ProductCategoryId
                                            join b in db.Brand on pc.BrandId equals b.BrandId
                                            join em in db.Employee on pc.EmployeeId equals em.EmployeeId

                                            select new { b.BrandId, b.BrandName, em.EmployeeId, em.EmployeeName, em.EmployeeMobileNo, em.EmployeeAddress, p.ProductId, p.ProductName, p.ProductByteImage, p.ProductPicturePath, pct.ProductCategoryId, pct.ProductCategoryName, pc.ProductCostInfoId, pc.ProductSize, pc.UnitPrice, pc.Quantity, pc.TransportCost, pc.Discount, pc.DateOfOrder, pc.Subtotal, pc.GrandTotal, pc.CurrentPayment, pc.DueAmount }

                      ).ToList();

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


            return VmProductCostDetailData;

        }


        //VmProductSellDetails_List
        public IEnumerable<VmProductSellDetails> GetProductSellInfoList()
        {


            List<VmProductSellDetails> ListData = new List<VmProductSellDetails>();

            var dataReceivedFromDatabase = (from p in db.Product
                                            join ps in db.ProductSellInfo on p.ProductId equals ps.ProductId
                                            join pct in db.ProductCategory on ps.ProductCategoryId equals pct.ProductCategoryId
                                            join b in db.Brand on ps.BrandId equals b.BrandId
                                            join em in db.Employee on ps.EmployeeId equals em.EmployeeId
                                            join c in db.Customer on ps.CustomerId equals c.CustomerId

                                            select new { b.BrandId, b.BrandName, c.CustomerId, c.CustomerName, c.CustomerMobile, c.CustomerAddress, em.EmployeeId, em.EmployeeName, em.EmployeeMobileNo, em.EmployeeAddress, em.Email, p.ProductId, p.ProductName, p.ProductByteImage, p.ProductPicturePath, pct.ProductCategoryId, pct.ProductCategoryName, ps.ProductSellInfoId, ps.ProductSize, ps.UnitPrice, ps.Quantity, ps.DateOfSell, ps.Discount, ps.Subtotal, ps.GrandTotal, ps.CurrentPayment, ps.DueAmount }

                     ).ToList();




            foreach (var obj in dataReceivedFromDatabase)
            {
                VmProductSellDetails vm = new VmProductSellDetails();

                vm.BrandId = obj.BrandId;
                vm.BrandName = obj.BrandName;
                vm.CustomerId = obj.CustomerId;
                vm.CustomerName = obj.CustomerName;
                vm.CustomerMobile = obj.CustomerMobile;
                vm.CustomerAddress = obj.CustomerAddress;
                vm.EmployeeId = obj.EmployeeId;
                vm.EmployeeName = obj.EmployeeName;
                vm.EmployeeAddress = obj.EmployeeAddress;
                vm.EmployeeMobileNo = obj.EmployeeMobileNo;
                vm.Email = obj.Email;
                vm.ProductId = obj.ProductId;
                vm.ProductName = obj.ProductName;
                vm.ProductPicturePath = obj.ProductPicturePath;
                vm.ProductByteImage = obj.ProductByteImage;
                vm.ProductCategoryId = obj.ProductCategoryId;
                vm.ProductCategoryName = obj.ProductCategoryName;
                vm.ProductSellInfoId = obj.ProductSellInfoId;
                vm.ProductSize = obj.ProductSize;
                vm.UnitPrice = obj.UnitPrice;
                vm.Quantity = obj.Quantity;
                vm.Discount = obj.Discount;
                vm.Subtotal = obj.Subtotal;
                vm.DateOfSell = obj.DateOfSell;
                vm.GrandTotal = obj.GrandTotal;
                vm.CurrentPayment = obj.CurrentPayment;
                vm.DueAmount = obj.DueAmount;


                ListData.Add(vm);

            }

            return ListData;
        }
        //Create
        public void PostProductSellInfo(ProductSellInfo ps)
        {

            db.ProductSellInfo.Add(ps);
            db.SaveChanges();

        }


        public IEnumerable<VmProductSellDetails> Search_SellInfoBy_EmployeeAndCustomerMobile(VmSearchProductSellInfo frmObj)
        {

            List<VmProductSellDetails> vmSellList = new List<VmProductSellDetails>();

            var DbData = (from p in db.Product
                          join ps in db.ProductSellInfo on p.ProductId equals ps.ProductId
                          join pct in db.ProductCategory on ps.ProductCategoryId equals pct.ProductCategoryId
                          join b in db.Brand on ps.BrandId equals b.BrandId
                          join em in db.Employee on ps.EmployeeId equals em.EmployeeId
                          join c in db.Customer on ps.CustomerId equals c.CustomerId
                          where em.EmployeeMobileNo.StartsWith(frmObj.EmployeeMobileNo) || c.CustomerMobile.StartsWith(frmObj.EmployeeMobileNo)|| em.EmployeeMobileNo.Contains(frmObj.EmployeeMobileNo) || c.CustomerMobile.Contains(frmObj.EmployeeMobileNo)|| em.EmployeeMobileNo==frmObj.EmployeeMobileNo || c.CustomerMobile==frmObj.EmployeeMobileNo
                          select new { b.BrandId, b.BrandName, c.CustomerId, c.CustomerName, c.CustomerMobile, c.CustomerAddress, em.EmployeeId, em.EmployeeName, em.EmployeeMobileNo, em.EmployeeAddress, em.Email, p.ProductId, p.ProductName, p.ProductByteImage, p.ProductPicturePath, pct.ProductCategoryId, pct.ProductCategoryName, ps.ProductSellInfoId, ps.ProductSize, ps.UnitPrice, ps.Quantity, ps.DateOfSell, ps.Discount, ps.Subtotal, ps.GrandTotal, ps.CurrentPayment, ps.DueAmount }
                     ).ToList();

            foreach (var obj in DbData)
            {
                VmProductSellDetails vm = new VmProductSellDetails();

                vm.BrandId = obj.BrandId;
                vm.BrandName = obj.BrandName;

                vm.CustomerId = obj.CustomerId;
                vm.CustomerName = obj.CustomerName;
                vm.CustomerMobile = obj.CustomerMobile;
                vm.CustomerAddress = obj.CustomerAddress;

                vm.EmployeeId = obj.EmployeeId;
                vm.EmployeeName = obj.EmployeeName;
                vm.EmployeeAddress = obj.EmployeeAddress;
                vm.EmployeeMobileNo = obj.EmployeeMobileNo;
                vm.Email = obj.Email;

                vm.ProductId = obj.ProductId;
                vm.ProductName = obj.ProductName;
                vm.ProductPicturePath = obj.ProductPicturePath;
                vm.ProductByteImage = obj.ProductByteImage;

                vm.ProductCategoryId = obj.ProductCategoryId;
                vm.ProductCategoryName = obj.ProductCategoryName;

                vm.ProductSellInfoId = obj.ProductSellInfoId;
                vm.ProductSize = obj.ProductSize;
                vm.UnitPrice = obj.UnitPrice;
                vm.Quantity = obj.Quantity;
                vm.Discount = obj.Discount;
                vm.Subtotal = obj.Subtotal;
                vm.DateOfSell = obj.DateOfSell;
                vm.GrandTotal = obj.GrandTotal;
                vm.CurrentPayment = obj.CurrentPayment;
                vm.DueAmount = obj.DueAmount;

                vmSellList.Add(vm);
            }



            return vmSellList;
        }

        //GetEditData
        public VmProductSellDetails GetVmProductSellDetailsBy_ProductSellInfoId(int? id)
        {
            var obj = (from p in db.Product
                       join ps in db.ProductSellInfo on p.ProductId equals ps.ProductId
                       join pct in db.ProductCategory on ps.ProductCategoryId equals pct.ProductCategoryId
                       join b in db.Brand on ps.BrandId equals b.BrandId
                       join em in db.Employee on ps.EmployeeId equals em.EmployeeId
                       join c in db.Customer on ps.CustomerId equals c.CustomerId
                       where ps.ProductSellInfoId == id
                       select new { b.BrandId, b.BrandName, c.CustomerId, c.CustomerName, c.CustomerMobile, c.CustomerAddress, em.EmployeeId, em.EmployeeName, em.EmployeeMobileNo, em.EmployeeAddress, em.Email, p.ProductId, p.ProductName, p.ProductByteImage, p.ProductPicturePath, pct.ProductCategoryId, pct.ProductCategoryName, ps.ProductSellInfoId, ps.ProductSize, ps.UnitPrice, ps.Quantity, ps.DateOfSell, ps.Discount, ps.Subtotal, ps.GrandTotal, ps.CurrentPayment, ps.DueAmount }

                     ).FirstOrDefault();





            VmProductSellDetails vm = new VmProductSellDetails();

            vm.BrandId = obj.BrandId;
            vm.BrandName = obj.BrandName;
            vm.CustomerId = obj.CustomerId;
            vm.CustomerName = obj.CustomerName;
            vm.CustomerMobile = obj.CustomerMobile;
            vm.CustomerAddress = obj.CustomerAddress;
            vm.EmployeeId = obj.EmployeeId;
            vm.EmployeeName = obj.EmployeeName;
            vm.EmployeeAddress = obj.EmployeeAddress;
            vm.EmployeeMobileNo = obj.EmployeeMobileNo;
            vm.Email = obj.Email;
            vm.ProductId = obj.ProductId;
            vm.ProductName = obj.ProductName;
            vm.ProductPicturePath = obj.ProductPicturePath;
            vm.ProductByteImage = obj.ProductByteImage;
            vm.ProductCategoryId = obj.ProductCategoryId;
            vm.ProductCategoryName = obj.ProductCategoryName;
            vm.ProductSellInfoId = obj.ProductSellInfoId;
            vm.ProductSize = obj.ProductSize;
            vm.UnitPrice = obj.UnitPrice;
            vm.Quantity = obj.Quantity;
            vm.Discount = obj.Discount;
            vm.Subtotal = obj.Subtotal;
            vm.DateOfSell = obj.DateOfSell;
            vm.GrandTotal = obj.GrandTotal;
            vm.CurrentPayment = obj.CurrentPayment;
            vm.DueAmount = obj.DueAmount;





            return vm;
        }




        //EditPost
        public void UpdatePost_ProductSellInfo(ProductSellInfo frmData)
        {
            ProductSellInfo DbObj = db.ProductSellInfo.Where(s => s.ProductSellInfoId == frmData.ProductSellInfoId).FirstOrDefault();


            DbObj.BrandId = frmData.BrandId;
            DbObj.CustomerId = frmData.CustomerId;
            DbObj.EmployeeId = frmData.EmployeeId;
            DbObj.ProductId = frmData.ProductId;
            DbObj.ProductCategoryId = frmData.ProductCategoryId;
            DbObj.ProductSellInfoId = frmData.ProductSellInfoId;
            DbObj.ProductSize = frmData.ProductSize;
            DbObj.UnitPrice = frmData.UnitPrice;
            DbObj.Quantity = frmData.Quantity;
            DbObj.Discount = frmData.Discount;
            DbObj.Subtotal = frmData.Subtotal;
            DbObj.DateOfSell = frmData.DateOfSell;
            DbObj.GrandTotal = frmData.GrandTotal;
            DbObj.CurrentPayment = frmData.CurrentPayment;
            DbObj.DueAmount = frmData.DueAmount;

            db.Entry(DbObj).State = EntityState.Modified;
            db.SaveChanges();

        }

        public void RemovePost_ProductSellInfoBySellId(int? id)
        {

            var DbData = db.ProductSellInfo.Where(s => s.ProductSellInfoId == id).FirstOrDefault();
            db.ProductSellInfo.Remove(DbData);
            db.SaveChanges();
        }

        //-----------------Product-Brand-Category-------------------------------------------

        public IEnumerable<VmProduct_Brand_Category> ProductList()
        {
            List<VmProduct_Brand_Category> vmProductList = new List<VmProduct_Brand_Category>();


            var DataReceivedFromDatabase = (from b in db.Brand
                                            join p in db.Product on b.ProductId equals p.ProductId
                                            join pc in db.ProductCategory on b.BrandId equals pc.BrandId
                                            select new VmProduct_Brand_Category()
                                            {
                                                ProductId = p.ProductId,
                                                ProductName = p.ProductName,
                                                ProductPicturePath = p.ProductPicturePath,
                                                ProductByteImage = p.ProductByteImage,
                                                ProductCategoryId = pc.ProductCategoryId,
                                                ProductCategoryName = pc.ProductCategoryName,
                                                BrandId = b.BrandId,
                                                BrandName = b.BrandName
                                            }).ToList();

            foreach (var item in DataReceivedFromDatabase)
            {
                VmProduct_Brand_Category vm = new VmProduct_Brand_Category()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPicturePath = item.ProductPicturePath,
                    ProductByteImage = item.ProductByteImage,
                    ProductCategoryId = item.ProductCategoryId,
                    ProductCategoryName = item.ProductCategoryName,
                    BrandId = item.BrandId,
                    BrandName = item.BrandName
                };
                vmProductList.Add(vm);

            }

            return vmProductList;
        }



        public void PostProductBandCategory(VmProduct_Brand_Category model)
        {


            byte[] bytes = null;
            if (model.ProductImage.FileName != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    model.ProductImage.OpenReadStream().CopyTo(ms);
                    bytes = ms.ToArray();
                }
            }






            if (model.ProductImage != null)
            {
                if (model.ProductImage.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(model.ProductImage.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        model.ProductImage.CopyTo(fs);
                        fs.Flush();

                        Product pr = new Product()
                        {
                            ProductName = model.ProductName,
                            ProductByteImage = bytes,
                            ProductPicturePath = filepath,
                        };
                        db.Product.Add(pr);
                        db.SaveChanges();
                    }
                }
            }





            int pId = db.Product.Max(p => p.ProductId);

            Brand b = new Brand()
            {
                BrandName = model.BrandName,
                ProductId = pId
            };
            db.Brand.Add(b);
            db.SaveChanges();


            int bId = db.Brand.Max(b => b.BrandId);

            ProductCategory pc = new ProductCategory()
            {
                ProductCategoryName = model.ProductCategoryName,
                BrandId= bId
            };

            db.ProductCategory.Add(pc);
            db.SaveChanges();


















            //string uniqueFileName = UploadedFile(model);


            //Product pr = new Product
            //{
            //    ProductName = model.ProductName,
            //    ProductByteImage = model.ProductByteImage,
            //    ProductPicturePath = uniqueFileName,
            //    ProductCategoryId = pc.ProductCategoryId,
            //    BrandId = b.BrandId

            //};


            //db.Product.Add(pr);
            //db.SaveChanges();


        }

        //private string UploadedFile(VmProduct_Brand_Category model)
        //{
        //    string uniqueFileName = null;

        //    if (model.ProductImage != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.ProductImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}



        public string RemoveProductRecord(int? id)
        {
            Product pd = db.Product.Where(p=>p.ProductId==id).FirstOrDefault();
            Brand bd = db.Brand.Where(b=>b.ProductId==pd.ProductId).FirstOrDefault();
            ProductCategory pc = db.ProductCategory.Where(pct=>pct.BrandId==bd.BrandId).FirstOrDefault();
           
            

           //remove product category
            db.Remove(pc);
            db.SaveChanges();

            //remove Brand
            db.Remove(bd);
            db.SaveChanges();

            //remove product
            db.Remove(pd);
            db.SaveChanges();





            return "Successfully deleted data !!";
        }

        


        public VmProduct_Brand_Category GetProductDetails(int? id)
        {

            var item = (from b in db.Brand
                        join p in db.Product on b.ProductId equals p.ProductId
                        join pc in db.ProductCategory on b.BrandId equals pc.BrandId
                        where p.ProductId == id
                        select new VmProduct_Brand_Category()
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            ProductPicturePath = p.ProductPicturePath,
                            ProductByteImage = p.ProductByteImage,
                            ProductCategoryId = pc.ProductCategoryId,
                            ProductCategoryName = pc.ProductCategoryName,
                            BrandId = b.BrandId,
                            BrandName = b.BrandName
                        }).FirstOrDefault();



            VmProduct_Brand_Category vm = new VmProduct_Brand_Category()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ProductPicturePath = item.ProductPicturePath,
                ProductByteImage = item.ProductByteImage,
                ProductCategoryId = item.ProductCategoryId,
                ProductCategoryName = item.ProductCategoryName,
                BrandId = item.BrandId,
                BrandName = item.BrandName
            };

     

            return vm;
        }

        public void EditPostProductBandCategory(VmProduct_Brand_Category model)
        {

            var DbRecord = (from p in db.Product
                                   join b in db.Brand on p.ProductId equals b.ProductId
                                   join pr in db.ProductCategory on b.BrandId equals pr.BrandId
                                   where p.ProductId == model.ProductId
                                   select new VmProduct_Brand_Category()
                                   {
                                       ProductId = p.ProductId,
                                       ProductName = p.ProductName,
                                       ProductPicturePath = p.ProductPicturePath,
                                       ProductByteImage = p.ProductByteImage,
                                       BrandId = b.BrandId,
                                       BrandName = b.BrandName,
                                       ProductCategoryId = pr.ProductCategoryId,
                                       ProductCategoryName = pr.ProductCategoryName
                                   }).FirstOrDefault();


            //VmProduct_Brand_Category DbObj = new VmProduct_Brand_Category()
            //{
            //    ProductId = DbProductRecord.ProductId,
            //    ProductName = DbProductRecord.ProductName,
            //    ProductPicturePath = DbProductRecord.ProductPicturePath,
            //    ProductByteImage = DbProductRecord.ProductByteImage,
            //    BrandId = DbProductRecord.BrandId,
            //    BrandName = DbProductRecord.BrandName,
            //    ProductCategoryId = DbProductRecord.ProductCategoryId,
            //    ProductCategoryName = DbProductRecord.ProductCategoryName
            //};



            Product dbProductRecord = db.Product.Where(p => p.ProductId == model.ProductId).FirstOrDefault();

            
            ProductCategory Post_dbProductCategoryRecord = db.ProductCategory.Where(pc => pc.ProductCategoryId == model.ProductCategoryId).FirstOrDefault();

           
            Brand Post_dbBrandRecord = db.Brand.Where(b => b.BrandId == model.BrandId).FirstOrDefault();



            DbRecord.BrandName = Post_dbBrandRecord.BrandName;
            Brand brand = new Brand();
            brand.BrandName = DbRecord.BrandName;
            brand.ProductId = DbRecord.ProductId;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();


            DbRecord.ProductCategoryName = Post_dbProductCategoryRecord.ProductCategoryName;
            ProductCategory pcd = new ProductCategory();
            pcd.ProductCategoryName = DbRecord.ProductCategoryName;
            pcd.BrandId = DbRecord.BrandId;

            db.Entry(pcd).State = EntityState.Modified;
            db.SaveChanges();


            byte[] bytes = null;
            if (model.ProductImage.FileName != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    model.ProductImage.OpenReadStream().CopyTo(ms);
                    bytes = ms.ToArray();
                }
            }

            if (model.ProductImage != null)
            {
                if (model.ProductImage.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(model.ProductImage.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        model.ProductImage.CopyTo(fs);
                        fs.Flush();

                        dbProductRecord.ProductName = model.ProductName;
                        dbProductRecord.ProductByteImage = bytes;
                        dbProductRecord.ProductPicturePath = filepath;
                        db.Entry(dbProductRecord).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }


        }
        













    }
}
