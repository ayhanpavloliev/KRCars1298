using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KRCars1298.Data.Models
{
    public class Model : BaseModel 
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
