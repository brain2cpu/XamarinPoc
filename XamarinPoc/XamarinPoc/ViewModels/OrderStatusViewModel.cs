using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinPoc.Models;

namespace XamarinPoc.ViewModels
{
    class OrderStatusViewModel : ViewModelBase
    {
        private OrderStatus _order;
        public OrderStatus Order
        {
            get => _order;
            set
            {
                _order = value;
                NotifyPropertyChanged();

                // start a fire-and-forget without raising warnings, not the best approach though!
                _ = GetStatusAsync();
            }
        }

        private string _status;

        public string Status
        {
            get => _status;
            private set
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }

        private Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ??= new Command(async () => await GetStatusAsync());

        private async Task GetStatusAsync()
        {
            try
            {
                Status = await Delivery.GetOrderStatusAsync(Order.Id);
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
        }
    }
}
