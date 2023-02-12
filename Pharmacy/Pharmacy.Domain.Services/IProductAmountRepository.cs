using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces
{
    public interface IProductAmountRepository : IPharmRepository<ProductAmount>
    {
        Task<ProductAmount?> GetAsync(int warehouseId, int ProductId);
    }
}
