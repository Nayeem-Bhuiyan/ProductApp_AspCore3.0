using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Models;
using ProductApp.ViewModel;

namespace ProductApp.DAL
{
   public interface IRepoproduct
    {

        //DropDown Data
        IEnumerable<Product> AllProductList();
        IEnumerable<Brand> BrandListByProductId(int? ProductId);
        IEnumerable<ProductCategory> ProductCategoryListByBrandId(int? brandId);

        IEnumerable<Country> CountryList();
        IEnumerable<District> DistrictListByCountryId(int? countryId);
        IEnumerable<Thana> ThanaListByDistrictId(int? districtId);

        IEnumerable<Employee> EmployeeList();
        IEnumerable<Customer> CustomerList();


        //Customer Data

        Customer CustomerByCustomerId(int? id);
        string EditPostCustomer(int? id, Customer cm);
        string CreateNewCustomer(Customer cm);
        string DeleteCustomer(int? id);
        Customer SearchCustomerByCustomerMobileNo(Customer vm);

        //ProductCost Data

        IEnumerable<VmProductCostList> GetProductCostList();
        VmProductCostDetails GetVmProductCostById(int? id);
        void UpdatePost_ProductCostInfo(ProductCostInfo data);
        void PostProductCostInfo(ProductCostInfo vm);
        void RemovePost_ProductCostInfoByCostId(int? id);
        IEnumerable<VmProductCostDetails> Search_VmCostProductInfo(VmSearchProductCost vm);
        IEnumerable<VmProductCostDetails> CostProductList();

        //ProductSell Data

        IEnumerable<VmProductSellDetails> GetProductSellInfoList();
        void PostProductSellInfo(ProductSellInfo ps);
        IEnumerable<VmProductSellDetails> Search_SellInfoBy_EmployeeAndCustomerMobile(VmSearchProductSellInfo frmObj);
        VmProductSellDetails GetVmProductSellDetailsBy_ProductSellInfoId(int? id);
        void UpdatePost_ProductSellInfo(ProductSellInfo frmData);
        void RemovePost_ProductSellInfoBySellId(int? id);


        //Employee Data
        public IEnumerable<Employee> SearchEmployeeData(VmSearchEmployeeRecord SearchParameters);
        IEnumerable<VmEmployeeDetails> SearchEmployeeRecord(VmSearchEmployeeRecord SearchParameters);
        IEnumerable<VmEmployeeDetails> EmployeeListaData();
        void DeleteEmployeeRecord(int? id);
        VmEmployeeDetails GetEmpRecordById(int? id);
        void EditPostEmployee(Employee empObj);
        void CreateNewEmployee(Employee emp);




        //Product Data
        IEnumerable<VmProduct_Brand_Category> ProductList();
        VmProduct_Brand_Category GetProductDetails(int? id);
        void EditPostProductBandCategory(VmProduct_Brand_Category model);
        string RemoveProductRecord(int? id);
        void PostProductBandCategory(VmProduct_Brand_Category vm);



    }
}
