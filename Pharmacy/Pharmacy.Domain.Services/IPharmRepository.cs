using Pharmacy.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces
{
    public interface IPharmRepository<T,K,P> where T : class where K: class where P : class
    {
        Task<P[]> GetAllASync();
        Task<K> GetAsync(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
