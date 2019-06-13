#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableInt32Value : Primitive<int?>
    {
        #region Constructors

        protected NullableInt32Value(int? value) : base(value)
        {
        }

        #endregion
    }
}