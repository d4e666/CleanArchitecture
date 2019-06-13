#region Usings

using System;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class CaseInsensitiveStringValue : StringValue
    {
        #region Constructors

        /// <inheritdoc />
        protected CaseInsensitiveStringValue(string value) : base(value)
        {
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override bool EqualValue(string thisValue, string otherValue)
        {
            return string.Equals(thisValue, otherValue, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}