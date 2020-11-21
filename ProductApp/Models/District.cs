using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "District Name should not be longer than 50 characters.")]
        [Display(Name ="District Name")]
        public string DistrictName { get; set; }
        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }

        public virtual ICollection<Thana> Thana { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual Country Country { get; set; }
    }
}
