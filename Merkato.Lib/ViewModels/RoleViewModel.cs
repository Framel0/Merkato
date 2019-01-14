using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "RoleName")]
        public string Name { get; set; }

        //[Display(Name = "Role Name")]
        //[Required(ErrorMessage = "Enter Role name")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //public string Name { get; set; }
    }
}
