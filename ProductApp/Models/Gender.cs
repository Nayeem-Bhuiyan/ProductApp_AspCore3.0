using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [Required(ErrorMessage = "GenderName is required.")]
        [Display(Name = "Gender Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your input should not be longer than 50 characters.")]
        public string GenderName { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
