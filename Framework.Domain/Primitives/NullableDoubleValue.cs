#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableDoubleValue : Primitive<double?>
    {
        #region Constructors

        protected NullableDoubleValue(double? value) : base(value)
        {
        }

        #endregion
    }
}