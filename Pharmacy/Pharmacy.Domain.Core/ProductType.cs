﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Core
{
    public class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }

        //Primary Key
        public int Id { get; set; }
        public string Name { get; set; }
        public string Properties { get; set; }

        //Navigation Property
        public virtual ICollection<Product> Products { get; private set; }
    }
}