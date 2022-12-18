using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Data.DTO
{
    public class WarehouseDTO
    {
        //Primary Key
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }

        //Navigation Property
        public virtual ICollection<ProductAmountDTO>? ProductAmounts { get; private set; }
    }
}
