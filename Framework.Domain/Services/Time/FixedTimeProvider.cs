#region Usings

using System;

#endregion

namespace Framework.Domain.Services.Time
{
    public sealed class FixedTimeProvider : TimeProvider
    {
        #region Fields

        private readonly DateTimeOffset _moment;

        #endregion

        #region Constructors

        public FixedTimeProvider(DateTime moment) : this(new DateTimeOffset(moment))
        {
        }

        public FixedTimeProvider(DateTimeOffset moment)
        {
            this._moment = moment;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override DateTime GetCurrentDate()
        {
            return this._moment.Date;
        }

        /// <inheritdoc />
        public override DateTime GetLocalDateAndTime()
        {
            return this._moment.LocalDateTime;
        }

        /// <inheritdoc />
        public override DateTime GetUtcDateAndTime()
        {
            return this._moment.UtcDateTime;
        }

        /// <inheritdoc />
        public override DateTimeOffset GetLocalDateAndTimeWithOffset()
        {
            return this._moment.ToLocalTime();
        }

        /// <inheritdoc />
        public override DateTimeOffset GetUtcDateAndTimeWithOffset()
        {
            return this._moment.ToUniversalTime();
        }

        /// <inheritdoc />
        public override long GetUnixTimeInSeconds()
        {
            return this._moment.ToUnixTimeSeconds();
        }

        /// <inheritdoc />
        public override long GetUnixTimeInMilliseconds()
        {
            return this._moment.ToUnixTimeMilliseconds();
        }

        #endregion
    }
}