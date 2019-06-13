#region Usings

using Framework.Domain.Entities;
using Framework.Domain.Gateways;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.Gateways
{
    public abstract class EntityGateway<TUnitOfWork, TEntity> : IEntityGateway<TEntity>
        where TUnitOfWork : IUnitOfWork
        where TEntity : AggregateRoot
    {
        #region Constructors

        protected EntityGateway(TUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        #endregion

        #region Properties

        protected TUnitOfWork UnitOfWork { get; }

        #endregion
    }
}