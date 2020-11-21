using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class ProductSellInfo
    {
       [Key]
        public int ProductSellInfoId { get; set; }
        [Required(ErrorMessage = "CustomerId is required.")]
        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }
        [Required(ErrorMessage = "ProductId is required.")]
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "BrandId is required.")]
        [ForeignKey("BrandId")]
        public int? BrandId { get; set; }
        [Required(ErrorMessage = "ProductCategoryId is required.")]
        [ForeignKey("ProductCategoryId")]
        public int? ProductCategoryId { get; set; }
        public double ProductSize { get; set; }
        public double UnitPrice { get; set; }
        [Range(1, 99999)]
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double Subtotal { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Applied date is required")]
        [Display(Name = "Date applied")]
        public DateTime DateOfSell { get; set; }
        public double GrandTotal { get; set; }
        public double CurrentPayment { get; set; }
        public double DueAmount { get; set; }
        [ForeignKey("EmployeeId")]
        public int? EmployeeId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
