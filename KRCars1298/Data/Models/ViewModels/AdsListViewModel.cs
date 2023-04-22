namespace KRCars1298.Data.Models.ViewModels
{
    public class AdsListViewModel
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string CarFullName => $"{this.Brand} {this.Model}";

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string Fuel { get; set; }

        public int Price { get; set; }

        public string City { get; set; }
    }
}
