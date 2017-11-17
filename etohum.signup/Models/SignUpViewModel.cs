using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Models
{
    // the model used for data binding
    public class SignUpViewModel
    {
        [Required]
        [MaxLength(20)]
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name ="Surname")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [MaxLength(50)]
        [Display(Name ="Frenid's Email Address")]
        public string FreindEmail { get; set; }
    }
}
