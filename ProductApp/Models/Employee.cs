using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Employee
    {
       
        [Key]
        public int EmployeeId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "EmployeeName is required.")]
        public string EmployeeName { get; set; }
        [Display(Name = "Employee Address")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        public string EmployeeAddress { get; set; }
        [Display(Name = "Employee Mobile")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Required(ErrorMessage = "EmployeeMobileNo is required.")]
        public string EmployeeMobileNo { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        [Required(ErrorMessage = "EmployeeMobileNo is required.")]
        public string Email { get; set; }

        [ForeignKey("GenderId")]
        public int GenderId { get; set; }
        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        [ForeignKey("District")]
        public int? DistrictId { get; set; }
        [ForeignKey("Thana")]
        public int? ThanaId { get; set; }

        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        public virtual Thana Thana { get; set; }
        public virtual Gender Gender { get; set; }

        public virtual ICollection<ProductCostInfo> ProductCostInfo { get; set; }
        public virtual ICollection<ProductSellInfo> ProductSellInfo { get; set; }
    }
}
