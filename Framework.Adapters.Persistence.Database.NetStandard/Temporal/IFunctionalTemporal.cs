#region Usings

using System;

#endregion

namespace Framework.Adapters.Persistence.Database.NetStandard.Temporal
{
    public interface IFunctionalTemporal
    {
        #region Properties

        DateTimeOffset FunctionalValidFrom { get; set; }

        DateTimeOffset FunctionalValidTo { get; set; }

        #endregion
    }
}