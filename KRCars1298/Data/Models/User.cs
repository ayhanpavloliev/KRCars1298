using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models
{
    public class User : IdentityUser
    {
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string City { get; set; }
    }
}
