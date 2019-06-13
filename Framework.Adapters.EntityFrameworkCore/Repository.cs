#region Usings

using Framework.Adapters.EntityFrameworkCore.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Framework.Adapters.EntityFrameworkCore
{
    public abstract class Repository<T> : IEntityRepository<T>
        where T : class
    {
        #region Constructors

        protected Repository(DbSet<T> set)
        {
            this.Set = set;
        }

        #endregion

        #region Properties

        protected DbSet<T> Set { get; }

        #endregion

        /// <inheritdoc />
        public abstract Task<T> GetByIdAsync(long id);

        /// <inheritdoc />
        public abstract Task<IEnumerable<T>> ListAllAsync();

        /// <inheritdoc />
        public abstract Task<IEnumerable<T>> ListAsync(ISpecification<T> specification);

        /// <inheritdoc />
        public abstract Task<T> GetSingleBySpecificationAsync(ISpecification<T> specification);

        /// <inheritdoc />
        public abstract Task<bool> Exists(ISpecification<T> specification);

        /// <inheritdoc />
        public abstract Task<T> AddAsync(T entity);

        /// <inheritdoc />
        public abstract Task UpdateAsync(T entity);

        /// <inheritdoc />
        public abstract Task DeleteAsync(T entity);

        /// <inheritdoc />
        public abstract Task<long> CountAll();
    }
}