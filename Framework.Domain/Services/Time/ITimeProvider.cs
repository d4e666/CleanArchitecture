#region Usings

using System;

#endregion

namespace Framework.Domain.Services.Time
{
    public interface ITimeProvider
    {
        #region Properties

        DateTimeOffset UnixEpoch { get; }

        DateTime MinDateTime { get; }

        DateTime MaxDateTime { get; }

        DateTimeOffset MinDateTimeWithOffset { get; }

        DateTimeOffset MaxDateTimeWithOffset { get; }

        #endregion

        #region Methods

        DateTime GetCurrentDate();

        DateTime GetLocalDateAndTime();

        DateTime GetUtcDateAndTime();

        DateTimeOffset GetLocalDateAndTimeWithOffset();

        DateTimeOffset GetUtcDateAndTimeWithOffset();

        long GetUnixTimeInSeconds();

        long GetUnixTimeInMilliseconds();

        #endregion
    }
}