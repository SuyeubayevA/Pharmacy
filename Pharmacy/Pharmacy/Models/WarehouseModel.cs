using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class WarehouseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
