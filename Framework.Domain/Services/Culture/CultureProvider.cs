#region Usings

using System;
using System.Globalization;

#endregion

namespace Framework.Domain.Services.Culture
{
    public abstract class CultureProvider : ICultureProvider
    {
        #region Fields

        private static readonly object SyncRoot = new object();

        #endregion

        #region Properties

        public static ICultureProvider Current { get; private set; }

        #endregion

        #region Methods

        public static void SetCurrent(ICultureProvider value)
        {
            lock (SyncRoot)
            {
                Current = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <inheritdoc />
        public abstract CultureInfo GetCurrentCulture();

        /// <inheritdoc />
        public abstract CultureInfo GetCurrentUiCulture();

        #endregion
    }
}