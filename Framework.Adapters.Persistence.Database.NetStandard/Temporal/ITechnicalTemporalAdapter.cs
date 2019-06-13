#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.Temporal
{
    public interface ITechnicalTemporalAdapter : ICloneable
    {
        #region Methods

        void SetTechnicalValidBy(string user);
        void SetTechnicalValidFrom(DateTimeOffset moment);

        void SetTechnicalVoidBy(string user);
        void SetTechnicalValidTo(DateTimeOffset moment);

        #endregion
    }
}