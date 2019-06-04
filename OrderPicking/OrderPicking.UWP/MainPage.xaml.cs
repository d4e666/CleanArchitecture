namespace OrderPicking.UWP
{
    public sealed partial class MainPage
    {
        #region Constructors

        public MainPage()
        {
            this.InitializeComponent();

            this.LoadApplication(new OrderPicking.App());
        }

        #endregion
    }
}