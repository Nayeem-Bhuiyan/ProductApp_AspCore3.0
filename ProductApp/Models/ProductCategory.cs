using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Display(Name = "Product Category Name")]
        [Required(ErrorMessage = "ProductCategoryName is required.")]
        public string ProductCategoryName { get; set; }

        [ForeignKey("BrandId")]
        public int BrandId { get; set; }

        public virtual ICollection<ProductCostInfo> ProductCostInfo { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductSellInfo> ProductSellInfo { get; set; }
    }
}
