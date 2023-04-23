namespace KRCars1298.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
