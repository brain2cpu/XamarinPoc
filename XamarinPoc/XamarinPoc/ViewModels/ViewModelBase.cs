using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinPoc.Interfaces;
using XamarinPoc.Models;
using XamarinPoc.Services;

namespace XamarinPoc.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {
        #region Implements INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // it should be resolved as singleton by DI
        protected static readonly Order CurrentOrder = new() {Id = Guid.NewGuid().ToString("N")};
        
        // should be injected !!!
        protected readonly IPizzaDelivery Delivery = new PizzaDelivery();

        // alerts should be handled by a notification service
        protected static Task ShowAlertAsync(Exception xcp) => ShowAlertAsync(xcp.Message);

        protected static async Task ShowAlertAsync(string message)
        {
            await ((App)Application.Current).MainPage.DisplayAlert("Pizza service", message, "OK");
        }
    }
}
