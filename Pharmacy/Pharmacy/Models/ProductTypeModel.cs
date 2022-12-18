using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class ProductTypeModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Properties { get; set; }
    }
}
