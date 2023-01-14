using System.Linq;

namespace Pharmacy.Domain.Interfaces
{
    public interface IPharmRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllASync();
        Task<T?> GetAsync(int id); 
        Task<bool> Create(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(int id);
    }
}
