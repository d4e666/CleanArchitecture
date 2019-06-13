#region Usings

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methods

        void Commit();

        Task CommitAsync();

        Task CommitAsync(CancellationToken cancellationToken);

        #endregion
    }
}