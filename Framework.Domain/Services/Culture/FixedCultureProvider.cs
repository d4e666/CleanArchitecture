#region Usings

using System;
using System.Globalization;

#endregion

namespace Framework.Domain.Services.Culture
{
    public sealed class FixedCultureProvider : CultureProvider
    {
        #region Fields

        private readonly CultureInfo _culture;
        private readonly CultureInfo _uiCulture;

        #endregion

        #region Constructors

        public FixedCultureProvider(CultureInfo culture, CultureInfo uiCulture)
        {
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
            this._uiCulture = uiCulture ?? throw new ArgumentNullException(nameof(uiCulture));
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override CultureInfo GetCurrentCulture()
        {
            return this._culture;
        }

        /// <inheritdoc />
        public override CultureInfo GetCurrentUiCulture()
        {
            return this._uiCulture;
        }

        #endregion
    }
}