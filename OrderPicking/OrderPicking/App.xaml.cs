#region Usings

using OrderPicking.Services;
using OrderPicking.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace OrderPicking
{
    public partial class App : Application
    {
        #region Fields

        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        #endregion

        #region Constructors

        public App()
        {
            this.InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            this.MainPage = new MainPage();
        }

        #endregion

        #region Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion
    }
}