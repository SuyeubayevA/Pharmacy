using Pharmacy.Application.Services;
using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Business
{
    public class Order : IOrder
    {
        void IOrder.MakeOrder(Product product)
        {
            throw new NotImplementedException();
        }

        void IOrder.MakeOrder(IEnumerable<Product> product)
        {
            throw new NotImplementedException();
        }
    }
}
