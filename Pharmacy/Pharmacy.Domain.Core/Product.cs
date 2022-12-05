using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Core
{
    public class Product
    {
        public Product()
        {
            this.ProductAmounts = new HashSet<ProductAmount>();
        }
        //Primary Key
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        //Foreign Key
        public int ProductTypeId { get; set; }
        public int SalesInfoId { get; set; }

        //Nvigation Property
        public virtual ProductType ProductType { get; set; }
        public virtual SalesInfo SalesInfo { get; set; }
        public virtual ICollection<ProductAmount> ProductAmounts { get; private set; }
    }
}
