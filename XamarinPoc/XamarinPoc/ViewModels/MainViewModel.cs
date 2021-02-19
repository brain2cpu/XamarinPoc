﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinPoc.Interfaces;
using XamarinPoc.Models;
using XamarinPoc.Services;

namespace XamarinPoc.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        //should be injected
        private readonly IPizzaDelivery _delivery = new PizzaDelivery();

        public ObservableCollection<Pizza> PizzaTypes { get; } = new();

        private Pizza _selectedPizza;
        public Pizza SelectedPizza
        {
            get => _selectedPizza;
            set
            {
                _selectedPizza = value;
                NotifyPropertyChanged();
                OrderCommand.ChangeCanExecute();
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
                OrderCommand.ChangeCanExecute();
            }
        }

        private Command _orderCommand;
        public Command OrderCommand =>
            _orderCommand ??= new Command(async () => await Order(), () => SelectedPizza != null && Quantity > 0 && Quantity < 200);

        private async Task Order()
        {
            try
            {
                var isSuccess = await _delivery.OrderAsync(new Order {PizzaName = SelectedPizza.Name, Quantity = Quantity});

                await ShowAlertAsync(isSuccess ? "Buon appetito" : "Order failed");

                SelectedPizza = null;
                Quantity = 1;
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                var pizzas = await _delivery.GetVariationsAsync();

                foreach (var pizza in pizzas)
                {
                    PizzaTypes.Add(pizza);
                }
            }
            catch (Exception xcp)
            {
                await ShowAlertAsync(xcp);
            }
        }

        private static Task ShowAlertAsync(Exception xcp) => ShowAlertAsync(xcp.Message);

        private static async Task ShowAlertAsync(string message)
        {
            await ((App)Application.Current).MainPage.DisplayAlert("Pizza service", message, "OK");
        }
    }
}
