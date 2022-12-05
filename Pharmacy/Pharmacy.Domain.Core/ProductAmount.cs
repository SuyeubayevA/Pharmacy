using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Core
{
    public class ProductAmount
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }

        public int Amount { get; set; }
        public float Discount { get; set; }

        //Nvigation Property
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
