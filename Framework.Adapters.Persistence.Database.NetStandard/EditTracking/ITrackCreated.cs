#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.EditTracking
{
    public interface ITrackCreated
    {
        #region Properties

        string CreatedBy { get; }

        DateTimeOffset CreatedOn { get; }

        #endregion
    }
}