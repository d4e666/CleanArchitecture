#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class StringValue : Primitive<string>
    {
        #region Constructors

        protected StringValue(string value) : base(value)
        {
        }

        #endregion
    }
}