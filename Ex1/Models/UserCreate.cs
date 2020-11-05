using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ex1.Models
{
    public class UserCreate
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        [Required]
        [Display(Name = "Дата рождения")]
        public DateTime Date { get; set; }
    }
}
