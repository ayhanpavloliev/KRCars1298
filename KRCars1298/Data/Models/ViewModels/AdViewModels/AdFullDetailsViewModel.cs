using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models.ViewModels.AdViewModels
{
    public class AdFullDetailsViewModel : AdDetailsBaseViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string LastName { get; set; }

        public string OwnerFullName => $"{FirstName} {LastName}";


        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string City { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
