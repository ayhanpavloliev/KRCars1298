using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models
{
    public class Brand : BaseModel 
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
