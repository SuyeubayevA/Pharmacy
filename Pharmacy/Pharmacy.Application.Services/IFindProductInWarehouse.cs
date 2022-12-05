using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services.Interfaces
{
    public interface IFindProductInWarehouse
    {
        IEnumerable<ProductAmount> GetAvailableProductsInWarehouseByName(string productName);
        IEnumerable<ProductAmount> GetAvailableProductsInWarehouseByProdType(string productTypeName);
    }
}
