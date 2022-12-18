using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Data.DTO
{
    public class ProductTypeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Properties { get; set; }
    }
}
