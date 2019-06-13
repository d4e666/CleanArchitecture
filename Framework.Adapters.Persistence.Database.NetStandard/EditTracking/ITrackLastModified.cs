#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.EditTracking
{
    public interface ITrackLastModified
    {
        #region Properties

        string LastModifiedBy { get; }

        DateTimeOffset LastModifiedOn { get; }

        #endregion
    }
}