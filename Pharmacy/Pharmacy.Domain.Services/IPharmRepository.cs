using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces
{
    public interface IPharmRepository<T> where T : class
    {
        Task<T[]> GetAllASync();
        Task<T> GetAsync(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
