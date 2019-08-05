using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace PolytexWebApp.DataLayer
{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(ulong id);
        Task<bool> Create(T order);
        Task<bool> Update(T order);
        Task<bool> Delete(ulong id);
    }
}