using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Display(Name = "Brand Name")]
        [Required(ErrorMessage = "Brand Name is required.")]
        public string BrandName { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public virtual ICollection<ProductCostInfo> ProductCostInfo { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<ProductSellInfo> ProductSellInfo { get; set; }
    }
}
