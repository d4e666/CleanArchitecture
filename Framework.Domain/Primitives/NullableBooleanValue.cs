#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableBooleanValue : Primitive<bool?>
    {
        #region Constructors

        protected NullableBooleanValue(bool? value) : base(value)
        {
        }

        #endregion
    }
}