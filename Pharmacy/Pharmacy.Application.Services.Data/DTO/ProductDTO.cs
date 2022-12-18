using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Data.DTO
{
    public class ProductDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }

        //Foreign Key
        public int? ProductTypeId { get; set; }
        public int? SalesInfoId { get; set; }

        //Navigation Property
        public virtual ProductTypeDTO? ProductType { get; set; }
        public virtual SalesInfoDTO? SalesInfo { get; set; }
        public virtual ICollection<ProductAmountDTO>? ProductAmounts { get; private set; }
    }
}
