#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OrderPicking.Models;
using OrderPicking.Services;
using Xamarin.Forms;

#endregion

namespace OrderPicking.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool isBusy;

        private string title = string.Empty;

        #endregion

        #region Properties

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        public bool IsBusy
        {
            get => this.isBusy;
            set => this.SetProperty(ref this.isBusy, value);
        }
        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        #endregion

        #region Methods

        protected bool SetProperty<T>(ref T backingStore, T value,
                                      [CallerMemberName] string propertyName = "",
                                      Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = this.PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Children

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}