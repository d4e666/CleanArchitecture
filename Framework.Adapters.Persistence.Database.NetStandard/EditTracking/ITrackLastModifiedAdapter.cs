#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.EditTracking
{
    public interface ITrackLastModifiedAdapter
    {
        #region Methods

        void SetLastModified(string lastModifiedBy, DateTimeOffset lastModifiedOn);

        #endregion
    }
}