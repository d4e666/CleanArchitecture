#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableInt64Value : Primitive<long?>
    {
        #region Constructors

        protected NullableInt64Value(long? value) : base(value)
        {
        }

        #endregion
    }
}