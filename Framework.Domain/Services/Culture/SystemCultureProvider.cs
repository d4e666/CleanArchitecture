#region Usings

using System.Globalization;
using System.Threading;

#endregion

namespace Framework.Domain.Services.Culture
{
    public sealed class SystemCultureProvider : CultureProvider
    {
        #region Methods

        /// <inheritdoc />
        public override CultureInfo GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }

        /// <inheritdoc />
        public override CultureInfo GetCurrentUiCulture()
        {
            return Thread.CurrentThread.CurrentUICulture;
        }

        #endregion
    }
}