using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ex1.Models
{
    public class AddTag
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}
