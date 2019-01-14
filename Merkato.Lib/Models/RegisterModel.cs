using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.Models
{
    public class RegisterModel
    {  
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string AgentAppPassword { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string PersonalEmail { get; set; }
    }
}
