using System.ComponentModel.DataAnnotations;

namespace Ex1.Models
{
    public class PostCreate
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }
    }
}
