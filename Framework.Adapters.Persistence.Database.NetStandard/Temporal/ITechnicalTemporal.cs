#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.Temporal
{
    public interface ITechnicalTemporal
    {
        #region Properties

        string TechnicalValidBy { get; }

        DateTimeOffset TechnicalValidFrom { get; }

        string TechnicalVoidBy { get; }

        DateTimeOffset TechnicalValidTo { get; }

        #endregion
    }
}