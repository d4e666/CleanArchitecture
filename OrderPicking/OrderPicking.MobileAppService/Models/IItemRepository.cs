#region Usings

using System.Collections.Generic;

#endregion

namespace OrderPicking.Models
{
    public interface IItemRepository
    {
        #region Methods

        void Add(Item item);

        void Update(Item item);

        Item Remove(string key);

        Item Get(string id);

        IEnumerable<Item> GetAll();

        #endregion
    }
}