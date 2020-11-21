using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "ProductName is required.")]
        public string ProductName { get; set; }
        [Display(Name = "Product PicturePath")]

        public string ProductPicturePath { get; set; }
        public byte[] ProductByteImage { get; set; }


        public virtual ICollection<Brand> Brand { get; set; }

        public virtual ICollection<ProductCostInfo> ProductCostInfo { get; set; }

        public virtual ICollection<ProductSellInfo> ProductSellInfo { get; set; }
    }
}
