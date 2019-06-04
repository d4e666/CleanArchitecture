#region Usings

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using OrderPicking.Models;
using OrderPicking.Views;
using Xamarin.Forms;

#endregion

namespace OrderPicking.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Constructors

        public ItemsViewModel()
        {
            this.Title = "Browse";
            this.Items = new ObservableCollection<Item>();
            this.LoadItemsCommand = new Command(async () => await this.ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(
                this, "AddItem", async (obj, item) =>
                                 {
                                     var newItem = item;
                                     this.Items.Add(newItem);
                                     await this.DataStore.AddItemAsync(newItem);
                                 });
        }

        #endregion

        #region Properties

        public ObservableCollection<Item> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        #endregion

        #region Methods

        private async Task ExecuteLoadItemsCommand()
        {
            if (this.IsBusy)
                return;

            this.IsBusy = true;

            try
            {
                this.Items.Clear();
                var items = await this.DataStore.GetItemsAsync(true);
                foreach (var item in items)
                    this.Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        #endregion
    }
}