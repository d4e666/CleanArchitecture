#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableDecimalValue : Primitive<decimal?>
    {
        #region Constructors

        protected NullableDecimalValue(decimal? value) : base(value)
        {
        }

        #endregion
    }
}