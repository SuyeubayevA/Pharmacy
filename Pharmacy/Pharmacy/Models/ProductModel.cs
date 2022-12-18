using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ProductTypeId { get; set; }

        public float Price { get; set; }
    }
}
