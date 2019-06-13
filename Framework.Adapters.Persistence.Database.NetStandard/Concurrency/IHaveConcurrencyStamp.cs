#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.Concurrency
{
    public interface IHaveConcurrencyStamp
    {
        #region Properties

        Guid ConcurrencyStamp { get; }

        #endregion
    }
}