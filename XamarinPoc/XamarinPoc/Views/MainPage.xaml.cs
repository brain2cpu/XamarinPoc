using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinPoc.Interfaces;
using XamarinPoc.Models;
using XamarinPoc.Services;

namespace XamarinPoc.Views
{
    public partial class MainPage
    {
        public static readonly IPizzaDelivery Delivery = new FakePizzaDelivery();
        public static readonly Order CurrentOrder = new();

        private readonly ObservableCollection<PizzaOrder> _list = new ObservableCollection<PizzaOrder>();

        public MainPage()
        {
            InitializeComponent();
            Pizzas.ItemsSource = _list;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _list.Clear();
            foreach(var p in await Delivery.GetVariationsAsync())
                _list.Add(new PizzaOrder
                {
                    Id = p.Id,
                    ImageUri = p.ImageUri,
                    Name = p.Name,
                    Quantity = CurrentOrder.Items.SingleOrDefault(x => Equals(x.Id, p.Id))?.Quantity ?? 0
                });
        }

        private async void Pizzas_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is not PizzaOrder p)
                return;

            var details = await Delivery.GetDetailsAsync(p.Id);
            details.Id = p.Id;

            await Application.Current.MainPage.Navigation.PushAsync(new PizzaDetailsPage(details), true);
        }

        private async void Order_OnClicked(object sender, EventArgs e)
        {
            var orderStatus = await Delivery.OrderAsync(CurrentOrder);

            await Application.Current.MainPage.Navigation.PushAsync(new OrderStatusPage(orderStatus), true);
        }

        private async void  Visit_OnClicked(object sender, EventArgs e)
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
                await ((App) Application.Current).MainPage.DisplayAlert("Pizza service",
                    "No map application available to open or place-mark can not be located", "OK");
            }
        }
    }
}
