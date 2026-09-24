using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TopFilms.Annotations;


namespace TopFilms.Models
{
    public class Film
    {
        [Display(Name = "Ідентифікатор")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Довжина має бути від 2 до 50 символів.")]
        [Remote(action: "CheckFilm", controller: "Films", AdditionalFields =  nameof(Director) + "," + nameof(Year),ErrorMessage = "Такий фільм вже є.")]
        [Display(Name = "Назва")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть Режисера")]
        [Display(Name = "Режисер")]
        public required string Director { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть Жанр")]
        [Display(Name = "Жанр")]
        public required string Genre { get; set; }

        [MyFilms(ErrorMessage = "Рік не може бути майбутнім")]
        [Required(ErrorMessage = "Будь ласка, введіть Рік")]
        [Display(Name = "Рік")]
        public int Year { get; set; }

        [Display(Name = "Постер")]
        public string? Poster { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть опис")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Довжина має бути від 5 до 500 символів.")]
        [Display(Name = "Опис")]
        public required string Info { get; set; }
    }
}
