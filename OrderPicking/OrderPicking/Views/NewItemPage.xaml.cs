#region Usings

using System;
using OrderPicking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace OrderPicking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        #region Constructors

        public NewItemPage()
        {
            this.InitializeComponent();

            this.Item = new Item
                        {
                            Text = "Item name",
                            Description = "This is an item description."
                        };

            this.BindingContext = this;
        }

        #endregion

        #region Properties

        public Item Item { get; set; }

        #endregion

        #region Methods

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", this.Item);
            await this.Navigation.PopModalAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        #endregion
    }
}