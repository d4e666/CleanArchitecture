#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.EditTracking
{
    public interface ITrackCreatedAdapter
    {
        #region Methods

        void SetCreated(string createdBy, DateTimeOffset createdOn);

        #endregion
    }
}