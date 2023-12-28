using System;
using System.ComponentModel.DataAnnotations;

namespace KRCars1298.Data.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid();
        }

        [Required]
        public Guid Id { get; set; }
    }
}
