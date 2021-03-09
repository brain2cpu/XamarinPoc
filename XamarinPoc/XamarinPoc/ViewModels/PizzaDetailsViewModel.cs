using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinPoc.Models;

namespace XamarinPoc.ViewModels
{
    class PizzaDetailsViewModel : ViewModelBase
    {
        private PizzaDetails _selectedPizza;
        public PizzaDetails SelectedPizza
        {
            get => _selectedPizza;
            set
            {
                _selectedPizza = value;
                NotifyPropertyChanged();

                Quantity = CurrentOrder.Items.SingleOrDefault(x => string.Equals(x.Id, _selectedPizza?.Id))?.Quantity ?? 1;
            }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                NotifyPropertyChanged();
            }
        }

        private Command _addCommand;
        public Command AddCommand => _addCommand ??= new Command(async () => await AddAsync());

        private async Task AddAsync()
        {
            try
            {
                CurrentOrder.SetItem(new OrderItem
                {
                    Id = SelectedPizza.Id, 
                    Quantity = Quantity, 
                    UnitPrice = SelectedPizza.Price
                });

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
        }
    }
}
