﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ex1.Models
{
    public class AddInformation
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Хобби")]
        public string Hobby { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Аватар")]
        public string Img { get; set; }
    }
}
