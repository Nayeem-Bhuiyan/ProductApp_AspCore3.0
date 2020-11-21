using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Mobile")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        public string CustomerMobile { get; set; }
        [Display(Name = "Customer Address")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Required(ErrorMessage = "Customer Address is required.")]
        public string CustomerAddress { get; set; }


        public virtual ICollection<ProductSellInfo> ProductSellInfo { get; set; }
    }
}
