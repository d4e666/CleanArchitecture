#region Usings

using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Adapters.EntityFrameworkCore.Specifications;
using Framework.Adapters.Persistence.Database.NetStandard;

#endregion

namespace Framework.Adapters.EntityFrameworkCore
{
    public interface IEntityRepository<T> : IRepository<T>
    {
        #region Methods

        Task<IEnumerable<T>> ListAsync(ISpecification<T> specification);

        Task<T> GetSingleBySpecificationAsync(ISpecification<T> specification);

        Task<bool> Exists(ISpecification<T> specification);

        #endregion
    }
}