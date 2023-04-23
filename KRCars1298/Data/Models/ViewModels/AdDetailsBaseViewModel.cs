using System;

namespace KRCars1298.Data.Models.ViewModels
{
    public class AdDetailsBaseViewModel
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }

        public string ModelName { get; set; }

        public string CarFullName => $"{this.BrandName} {this.ModelName}";

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string Fuel { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
    }
}
