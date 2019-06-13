#region Usings

using System.Globalization;

#endregion

namespace Framework.Domain.Services.Culture
{
    public interface ICultureProvider
    {
        #region Methods

        CultureInfo GetCurrentCulture();

        CultureInfo GetCurrentUiCulture();

        #endregion
    }
}