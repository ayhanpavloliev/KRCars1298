using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models.ViewModels
{
    public class ManageModelViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string VehicleType { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string BrandName { get; set; }
    }
}
