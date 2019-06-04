#region Usings

using System;
using System.Windows.Input;
using Xamarin.Forms;

#endregion

namespace OrderPicking.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        #region Constructors

        public AboutViewModel()
        {
            this.Title = "About";

            this.OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        #endregion

        #region Properties

        public ICommand OpenWebCommand { get; }

        #endregion
    }
}