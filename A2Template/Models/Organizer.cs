//using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

namespace A2Template.Models
{
    public class Organizer
    {
        [Key]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
