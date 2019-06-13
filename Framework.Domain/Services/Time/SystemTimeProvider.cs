#region Usings

using System;

#endregion

namespace Framework.Domain.Services.Time
{
    public sealed class SystemTimeProvider : TimeProvider
    {
        #region Methods

        /// <inheritdoc />
        public override DateTime GetCurrentDate()
        {
            return DateTime.Today;
        }

        /// <inheritdoc />
        public override DateTime GetLocalDateAndTime()
        {
            return DateTime.Now;
        }

        /// <inheritdoc />
        public override DateTime GetUtcDateAndTime()
        {
            return DateTime.UtcNow;
        }

        /// <inheritdoc />
        public override DateTimeOffset GetLocalDateAndTimeWithOffset()
        {
            return DateTimeOffset.Now;
        }

        /// <inheritdoc />
        public override DateTimeOffset GetUtcDateAndTimeWithOffset()
        {
            return DateTimeOffset.UtcNow;
        }

        /// <inheritdoc />
        public override long GetUnixTimeInSeconds()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        /// <inheritdoc />
        public override long GetUnixTimeInMilliseconds()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        #endregion
    }
}