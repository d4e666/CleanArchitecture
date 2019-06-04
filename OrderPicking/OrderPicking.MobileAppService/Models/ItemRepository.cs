#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#endregion

namespace OrderPicking.Models
{
    public class ItemRepository : IItemRepository
    {
        #region Fields

        private static readonly ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        #endregion

        #region Constructors

        public ItemRepository()
        {
            this.Add(
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Item 1",
                    Description = "This is an item description."
                });
            this.Add(
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Item 2",
                    Description = "This is an item description."
                });
            this.Add(
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Item 3",
                    Description = "This is an item description."
                });
        }

        #endregion

        #region Methods

        public Item Get(string id)
        {
            return items[id];
        }

        public IEnumerable<Item> GetAll()
        {
            return items.Values;
        }

        public void Add(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public Item Find(string id)
        {
            Item item;
            items.TryGetValue(id, out item);

            return item;
        }

        public Item Remove(string id)
        {
            Item item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(Item item)
        {
            items[item.Id] = item;
        }

        #endregion
    }
}