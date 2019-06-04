#region Usings

using System.Collections.Generic;
using System.Threading.Tasks;
using OrderPicking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace OrderPicking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        #region Fields

        private readonly Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        #endregion

        #region Constructors

        public MainPage()
        {
            this.InitializeComponent();

            this.MasterBehavior = MasterBehavior.Popover;

            this.MenuPages.Add((int) MenuItemType.Browse, (NavigationPage) this.Detail);
        }

        #endregion

        #region Methods

        public async Task NavigateFromMenu(int id)
        {
            if (!this.MenuPages.ContainsKey(id))
                switch (id)
                {
                    case (int) MenuItemType.Browse:
                        this.MenuPages.Add(id, new NavigationPage(new ItemsPage()));

                        break;
                    case (int) MenuItemType.About:
                        this.MenuPages.Add(id, new NavigationPage(new AboutPage()));

                        break;
                }

            var newPage = this.MenuPages[id];

            if (newPage != null && this.Detail != newPage)
            {
                this.Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                this.IsPresented = false;
            }
        }

        #endregion
    }
}