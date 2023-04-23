using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models.ViewModels.AdViewModels
{
    public class AllAdsListViewModel : AdsListBaseViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string City { get; set; }
    }
}
