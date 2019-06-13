#region Usings

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Framework.Domain.Entities
{
    /// <summary>
    ///     TODO: Struct HashCode to be replaced by .NET standard 2.1 hashcode struct.
    /// </summary>
    public struct HashCode
    {
        private ICollection<object> _Components;

        public void Add<T>(T value)
        {
            this.InitComponentsList();
            this._Components.Add(value);
        }

        public void Add<T>(T value, IEqualityComparer<T> comparer)
        {
            this.InitComponentsList();
            this._Components.Add(value);
        }

        private void InitComponentsList()
        {
            if (this._Components == null)
                this._Components = new List<object>();
        }

        public int ToHashCode()
        {
            var hash = 17;

            unchecked
            {
                hash = this._Components.Aggregate(hash, (current, componentValue) => current + 23 * (componentValue?.GetHashCode() ?? 0));
            }

            return hash;
        }
    }
}