using System;
using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models
{
    public class Model : BaseModel 
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public Brand Brand { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public VehicleType VehicleType { get; set; }

        [Required]
        public Guid VehicleTypeId { get; set; }
    }
}
