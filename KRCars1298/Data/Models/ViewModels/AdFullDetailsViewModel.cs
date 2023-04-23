using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models.ViewModels
{
    public class AdFullDetailsViewModel : AdDetailsBaseViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OwnerFullName => $"{FirstName} {LastName}";

        public string City { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
