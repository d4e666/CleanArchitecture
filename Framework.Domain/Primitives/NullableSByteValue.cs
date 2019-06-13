#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableSByteValue : Primitive<sbyte?>
    {
        #region Constructors

        protected NullableSByteValue(sbyte? value) : base(value)
        {
        }

        #endregion
    }
}