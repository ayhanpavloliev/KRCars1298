using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KRCars1298.Data.Models
{
    public class Ad : BaseModel
    {
        public Model Model { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public string Fuel { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public User User { get; set; }
    }
}
