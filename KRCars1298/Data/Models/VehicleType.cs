using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models
{
    public class VehicleType : BaseModel
    {
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }
    }
}