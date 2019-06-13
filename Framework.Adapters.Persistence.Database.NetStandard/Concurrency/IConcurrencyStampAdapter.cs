#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.Concurrency
{
    public interface IConcurrencyStampAdapter
    {
        #region Methods

        void SetConcurrencyStamp(Guid token);

        #endregion
    }
}