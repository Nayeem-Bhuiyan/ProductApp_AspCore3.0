using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Thana
    {
        [Key]
        public int ThanaId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Thana Name should not be longer than 50 characters.")]
        [Display(Name = "Thana Name")]
        public string ThanaName { get; set; }
        [ForeignKey("District")]
        public int? DistrictId { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual District District { get; set; }


    }
}
