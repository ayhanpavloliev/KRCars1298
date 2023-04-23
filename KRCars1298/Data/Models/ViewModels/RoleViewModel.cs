using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
