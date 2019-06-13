#region Usings

using System;

#endregion

namespace Framework.Domain.Services.Time
{
    public abstract class TimeProvider : ITimeProvider
    {
        #region Fields

        private static readonly object SyncRoot = new object();

        #endregion

        #region Properties

        public static ITimeProvider Current { get; private set; }

        public DateTimeOffset UnixEpoch { get; } = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        public DateTime MinDateTime { get; } = new DateTime(1900, 01, 01, 0, 0, 0);

        public DateTime MaxDateTime { get; } = new DateTime(9999, 12, 31, 0, 0, 0);

        public DateTimeOffset MinDateTimeWithOffset { get; } = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);

        public DateTimeOffset MaxDateTimeWithOffset { get; } = new DateTimeOffset(9999, 12, 31, 0, 0, 0, TimeSpan.Zero);

        #endregion

        #region Methods

        public static void SetCurrent(ITimeProvider value)
        {
            lock (SyncRoot)
            {
                Current = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <inheritdoc />
        public abstract DateTime GetCurrentDate();

        /// <inheritdoc />
        public abstract DateTime GetLocalDateAndTime();

        /// <inheritdoc />
        public abstract DateTime GetUtcDateAndTime();

        /// <inheritdoc />
        public abstract DateTimeOffset GetLocalDateAndTimeWithOffset();

        /// <inheritdoc />
        public abstract DateTimeOffset GetUtcDateAndTimeWithOffset();

        /// <inheritdoc />
        public abstract long GetUnixTimeInSeconds();

        /// <inheritdoc />
        public abstract long GetUnixTimeInMilliseconds();

        #endregion
    }
}