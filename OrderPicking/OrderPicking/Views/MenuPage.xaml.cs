#region Usings

using System.Collections.Generic;
using OrderPicking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace OrderPicking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        #region Fields

        private readonly List<HomeMenuItem> menuItems;

        #endregion

        #region Constructors

        public MenuPage()
        {
            this.InitializeComponent();

            this.menuItems = new List<HomeMenuItem>
                             {
                                 new HomeMenuItem
                                 {
                                     Id = MenuItemType.Browse,
                                     Title = "Browse"
                                 },
                                 new HomeMenuItem
                                 {
                                     Id = MenuItemType.About,
                                     Title = "About"
                                 }
                             };

            this.ListViewMenu.ItemsSource = this.menuItems;

            this.ListViewMenu.SelectedItem = this.menuItems[0];
            this.ListViewMenu.ItemSelected += async (sender, e) =>
                                              {
                                                  if (e.SelectedItem == null)
                                                      return;

                                                  var id = (int) ((HomeMenuItem) e.SelectedItem).Id;
                                                  await this.RootPage.NavigateFromMenu(id);
                                              };
        }

        #endregion

        #region Properties

        private MainPage RootPage => Application.Current.MainPage as MainPage;

        #endregion
    }
}