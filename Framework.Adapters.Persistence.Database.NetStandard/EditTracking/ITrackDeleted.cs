#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.EditTracking
{
    public interface ITrackDeleted
    {
        #region Properties

        string DeletedBy { get; }

        DateTimeOffset? DeletedOn { get; }

        #endregion
    }
}