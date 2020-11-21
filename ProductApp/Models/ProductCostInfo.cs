using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class ProductCostInfo
    {
        [Key]
        public int ProductCostInfoId { get; set; }

        [Required(ErrorMessage = "ProductId is required.")]
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "BrandId is required.")]
        [ForeignKey("BrandId")]
        public int? BrandId { get; set; }
        [Required(ErrorMessage = "ProductCategoryId is required.")]
        [ForeignKey("ProductCategoryId")]
        public int? ProductCategoryId { get; set; }
        public string ProductSize { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double TransportCost { get; set; }
        public double Discount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Applied date is required")]
        [Display(Name = "DateOfOrder")]
        public DateTime DateOfOrder { get; set; }
        public double Subtotal { get; set; }
        public double GrandTotal { get; set; }
        public double CurrentPayment { get; set; }
        public double DueAmount { get; set; }
        [ForeignKey("EmployeeId")]
        public int? EmployeeId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
