#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class BooleanValue : Primitive<bool>
    {
        #region Constructors

        protected BooleanValue(bool value) : base(value)
        {
        }

        #endregion
    }
}