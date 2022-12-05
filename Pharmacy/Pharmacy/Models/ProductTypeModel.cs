using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class ProductTypeModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Properties { get; set; }
    }
}
