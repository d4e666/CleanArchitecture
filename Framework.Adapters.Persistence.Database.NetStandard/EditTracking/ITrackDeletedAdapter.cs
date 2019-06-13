#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.EditTracking
{
    public interface ITrackDeletedAdapter
    {
        #region Methods

        void SetDeleted(string deletedBy, DateTimeOffset deletedOn);

        #endregion
    }
}