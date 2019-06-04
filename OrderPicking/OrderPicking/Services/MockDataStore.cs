#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderPicking.Models;

#endregion

namespace OrderPicking.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        #region Fields

        private readonly List<Item> items;

        #endregion

        #region Constructors

        public MockDataStore()
        {
            this.items = new List<Item>();
            var mockItems = new List<Item>
                            {
                                new Item
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Text = "First item",
                                    Description = "This is an item description."
                                },
                                new Item
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Text = "Second item",
                                    Description = "This is an item description."
                                },
                                new Item
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Text = "Third item",
                                    Description = "This is an item description."
                                },
                                new Item
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Text = "Fourth item",
                                    Description = "This is an item description."
                                },
                                new Item
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Text = "Fifth item",
                                    Description = "This is an item description."
                                },
                                new Item
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Text = "Sixth item",
                                    Description = "This is an item description."
                                }
                            };

            foreach (var item in mockItems)
                this.items.Add(item);
        }

        #endregion

        #region Methods

        public async Task<bool> AddItemAsync(Item item)
        {
            this.items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = this.items.Where(arg => arg.Id == item.Id).FirstOrDefault();
            this.items.Remove(oldItem);
            this.items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = this.items.Where(arg => arg.Id == id).FirstOrDefault();
            this.items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(this.items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(this.items);
        }

        #endregion
    }
}