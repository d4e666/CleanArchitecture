#region Usings

using System;
using OrderPicking.Models;
using OrderPicking.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace OrderPicking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        #region Fields

        private readonly ItemsViewModel viewModel;

        #endregion

        #region Constructors

        public ItemsPage()
        {
            this.InitializeComponent();

            this.BindingContext = this.viewModel = new ItemsViewModel();
        }

        #endregion

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.viewModel.Items.Count == 0)
                this.viewModel.LoadItemsCommand.Execute(null);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;

            if (item == null)
                return;

            await this.Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            this.ItemsListView.SelectedItem = null;
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        #endregion
    }
}