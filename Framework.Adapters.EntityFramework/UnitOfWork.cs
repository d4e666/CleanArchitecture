#region Usings

using System.Data.Entity;
using Framework.Adapters.Persistence.Database.NetStandard;

#endregion

namespace Framework.Adapters.EntityFramework
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