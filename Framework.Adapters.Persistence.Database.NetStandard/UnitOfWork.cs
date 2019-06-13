#region Usings

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        #region Constructors

        ~UnitOfWork()
        {
            this.Dispose(false);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void ReleaseManagedResources()
        {
        }

        protected virtual void ReleaseUnmanagedResources()
        {
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                this.ReleaseManagedResources();
            this.ReleaseUnmanagedResources();
        }

        #endregion

        /// <inheritdoc />
        public abstract void Commit();

        /// <inheritdoc />
        public abstract Task CommitAsync();

        /// <inheritdoc />
        public abstract Task CommitAsync(CancellationToken cancellationToken);
    }
}