using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinPoc.Models;

namespace XamarinPoc.ViewModels
{
    class OrderStatusViewModel : ViewModelBase
    {
        private OrderStatus _status;

        public OrderStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }

        private Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ??= new Command(async () => await RefreshAsync());

        private async Task RefreshAsync()
        {
            try
            {
                Status = await Delivery.GetOrderStatusAsync(Status.Id);
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
        }
    }
}
