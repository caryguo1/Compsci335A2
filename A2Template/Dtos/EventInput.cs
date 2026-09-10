//using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

namespace A2Template.Dtos
{
    public class EventInput
    {
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
