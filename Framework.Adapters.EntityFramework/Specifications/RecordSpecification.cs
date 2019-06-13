using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Adapters.EntityFramework.Specifications
{
    public abstract class RecordSpecification<T> : ISpecification<T>
    {
        protected RecordSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            this.IncludeStrings.Add(includeString);
        }
    }
}