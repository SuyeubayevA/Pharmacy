using Pharmacy.Domain.Core;
using System.Collections.Generic;

namespace Pharmacy.Application.Services
{
    public interface IOrder
    {
        void MakeOrder(Product product);
        void MakeOrder(IEnumerable<Product> product);
    }
}