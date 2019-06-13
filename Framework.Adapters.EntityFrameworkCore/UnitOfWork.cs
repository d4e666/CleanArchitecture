#region Usings

using Framework.Adapters.Persistence.Database.NetStandard;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Framework.Adapters.EntityFrameworkCore
{
    public abstract class UnitOfWork<TContext> : UnitOfWork
        where TContext : DbContext
    {
        #region Constructors

        protected UnitOfWork(TContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Properties

        protected TContext Context { get; }

        #endregion
    }
}