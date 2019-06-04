#region Usings

using OrderPicking.Models;
using OrderPicking.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace OrderPicking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        #region Fields

        private readonly ItemDetailViewModel viewModel;

        #endregion

        #region Constructors

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            this.InitializeComponent();

            this.BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            this.InitializeComponent();

            var item = new Item
                       {
                           Text = "Item 1",
                           Description = "This is an item description."
                       };

            this.viewModel = new ItemDetailViewModel(item);
            this.BindingContext = this.viewModel;
        }

        #endregion
    }
}