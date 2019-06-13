#region Usings

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace Framework.Adapters.EntityFramework.Specifications
{
    public interface ISpecification<T>
    {
        #region Properties

        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        List<string> IncludeStrings { get; }

        #endregion
    }
}