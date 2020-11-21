using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.ViewModel
{
    public class VmProductCostList
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

        public int ProductCostInfoId { get; set; }
        public string ProductSize { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double TransportCost { get; set; }
        public double Discount { get; set; }
        public DateTime DateOfOrder { get; set; }
        public double Subtotal { get; set; }
        public double GrandTotal { get; set; }
        public double CurrentPayment { get; set; }
        public double DueAmount { get; set; }
    }
}
