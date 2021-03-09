using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinPoc.Models;

namespace XamarinPoc.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private IEnumerable<Pizza> _availablePizzas = null;

        public ObservableCollection<PizzaOrder> Pizzas { get; } = new();

        private PizzaOrder _selectedPizza;
        public PizzaOrder SelectedPizza
        {
            get => _selectedPizza;
            set
            {
                _selectedPizza = value;
                NotifyPropertyChanged();
                ShowProductDetailsCommand.ChangeCanExecute();
                OrderCommand.ChangeCanExecute();
            }
        }

        private Command<PizzaOrder> _showProductDetailsCommand;
        public Command<PizzaOrder> ShowProductDetailsCommand =>
            _showProductDetailsCommand ??= new Command<PizzaOrder>(async p => await ShowProductDetailsAsync(p));

        private async Task ShowProductDetailsAsync(PizzaOrder pizza)
        {
            try
            {
                var details = await Delivery.GetDetailsAsync(pizza.Id);

                // ideally we would have a class handling all the navigation
                await Application.Current.MainPage.Navigation.PushAsync(new Views.PizzaDetailsPage(details), true);
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
        }

        private Command _orderCommand;
        public Command OrderCommand => _orderCommand ??= new Command(async () => await OrderAsync(), () => CurrentOrder.IsValid);

        private async Task OrderAsync()
        {
            try
            {
                if (!CurrentOrder.IsValid)
                    return;

                var orderStatus = await Delivery.OrderAsync(CurrentOrder);

                // see above
                await Application.Current.MainPage.Navigation.PushAsync(new Views.OrderStatusPage(orderStatus), true);
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
        }

        private Command _visitCommand;
        public Command VisitCommand => _visitCommand ??= new Command(async () => await VisitAsync());

        // the real location would come from an API call, the UX is questionable too
        private async Task VisitAsync()
        {
            var placeMark = new Placemark
            {
                CountryName = "Romania",
                Thoroughfare = "Bulevardul Vasile Parvan 2",
                Locality = "Timisoara"
            };
            var options = new MapLaunchOptions { Name = "A well-known place" };

            try
            {
                await Map.OpenAsync(placeMark, options);
            }
            catch
            {
                await ShowAlertAsync("No map application available to open or place-mark can not be located");
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                _availablePizzas ??= await Delivery.GetVariationsAsync();

                Pizzas.Clear();

                foreach (var p in _availablePizzas)
                {
                    Pizzas.Add(new PizzaOrder
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageUri = p.ImageUri,
                        Quantity = CurrentOrder.Items.SingleOrDefault(x => string.Equals(x.Id, p.Id))?.Quantity ?? 0

                    });
                }
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
            finally
            {
                OrderCommand.ChangeCanExecute();
            }
        }
    }
}
