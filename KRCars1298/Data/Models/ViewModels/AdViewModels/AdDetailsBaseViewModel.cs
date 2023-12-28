using System;
using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models.ViewModels.AdViewModels
{
    public class AdDetailsBaseViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string VehicleTypeName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string BrandName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ModelName { get; set; }

        public string CarFullName => $"{BrandName} {ModelName}";

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
        [StringLength(1_000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 100_000_000)]
        public int Price { get; set; }
    }
}
