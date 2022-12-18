using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Data.DTO
{
    public class ProductAmountDTO
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }

        public int Amount { get; set; }
        public float Discount { get; set; }

        //Navigation Property
        public string? ProductName { get; set; }
        public string? WarehouseName { get; set; }
    }
}
