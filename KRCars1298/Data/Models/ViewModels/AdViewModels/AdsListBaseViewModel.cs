using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models.ViewModels.AdViewModels
{
    public class AdsListBaseViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Model { get; set; }

        public string CarFullName => $"{Brand} {Model}";

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(1920, 2023)]
        public int Year { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Fuel { get; set; }

        [Required]
        [Range(0, 100_000_000)]
        public int Price { get; set; }
    }
}
