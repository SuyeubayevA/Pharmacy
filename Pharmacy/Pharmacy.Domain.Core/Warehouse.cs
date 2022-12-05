using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Core
{
    public class Warehouse
    {
        public Warehouse()
        {
            this.ProductAmounts = new HashSet<ProductAmount>();
        }

        //Primary Key
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        //Navigation Property
        public virtual ICollection<ProductAmount> ProductAmounts { get; private set; }
    }
}
