using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Adapters.Persistence.Database.NetStandard
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<long> CountAll();

    }
}