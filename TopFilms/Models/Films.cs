using System.ComponentModel.DataAnnotations;

namespace TopFilms.Models
{
    public class Film
    {
        [Display(Name = "Ідентифікатор")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву")]
        [Display(Name = "Назва")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть Режисера")]
        [Display(Name = "Режисер")]
        public required string Director { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть Жанр")]
        [Display(Name = "Жанр")]
        public required string Genre { get; set; }


        [Display(Name = "Рік")]
        public int Year { get; set; }

        [Display(Name = "Постер")]
        public string? Poster { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть опис")]
        [Display(Name = "Опис")]
        public required string Info { get; set; }
    }
}
