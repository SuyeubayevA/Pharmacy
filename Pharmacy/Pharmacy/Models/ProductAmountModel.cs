using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class ProductAmountModel
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public float Discount { get; set; }
    }
}
