#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableSingleValue : Primitive<float?>
    {
        #region Constructors

        protected NullableSingleValue(float? value) : base(value)
        {
        }

        #endregion
    }
}