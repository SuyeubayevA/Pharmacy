using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class SalesInfoModel
    {
        [Required]
        public int Sales { get; set; }
        [Required]
        public int ProductReminder { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
