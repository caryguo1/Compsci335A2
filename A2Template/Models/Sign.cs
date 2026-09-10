//using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

namespace A2Template.Models
{
    public class Sign
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
