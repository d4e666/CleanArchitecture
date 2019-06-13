#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableInt16Value : Primitive<short?>
    {
        #region Constructors

        protected NullableInt16Value(short? value) : base(value)
        {
        }

        #endregion
    }
}