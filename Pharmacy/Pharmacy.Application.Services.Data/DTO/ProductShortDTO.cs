using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Data.DTO
{
    public class ProductShortDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
    }
}
