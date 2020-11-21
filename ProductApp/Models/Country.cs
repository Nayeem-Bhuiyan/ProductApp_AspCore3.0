using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Country Name should not be longer than 50 characters.")]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public virtual ICollection<District> District { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }


    }
}
