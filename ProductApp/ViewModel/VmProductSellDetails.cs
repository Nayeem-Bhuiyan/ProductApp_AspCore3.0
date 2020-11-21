using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.ViewModel
{
    public class VmProductSellDetails
    {


        public int    BrandId { get; set; }
        public string BrandName { get; set; }

        public int    ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPicturePath { get; set; }
        public byte[] ProductByteImage { get; set; }

        public int    ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

        public int    ProductSellInfoId { get; set; }
        public double ProductSize { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public DateTime DateOfSell { get; set; }
        public double Subtotal { get; set; }
        public double GrandTotal { get; set; }
        public double CurrentPayment { get; set; }
        public double DueAmount { get; set; }


        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeMobileNo { get; set; }
        public string Email { get; set; }

    }
}
