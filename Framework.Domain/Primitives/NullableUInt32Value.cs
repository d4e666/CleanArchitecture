#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableUInt32Value : Primitive<uint?>
    {
        #region Constructors

        protected NullableUInt32Value(uint? value) : base(value)
        {
        }

        #endregion
    }
}