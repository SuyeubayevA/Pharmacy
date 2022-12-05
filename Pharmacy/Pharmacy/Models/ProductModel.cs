using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class ProductModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
