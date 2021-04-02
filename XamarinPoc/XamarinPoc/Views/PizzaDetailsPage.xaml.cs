using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPoc.Models;

namespace XamarinPoc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaDetailsPage
    {
        private readonly PizzaDetails _pizza;

        public PizzaDetailsPage(PizzaDetails pizza)
        {
            InitializeComponent();

            _pizza = pizza;

            PizzaImage.Source = new UriImageSource {Uri = new Uri(pizza.ImageUri)};
            PizzaName.Text = pizza.Name;
            PizzaDescription.Text = pizza.Description;
            PizzaPrice.Text = $"Price {pizza.Price} RON";
            QuantityModifier.Value = MainPage.CurrentOrder.Items.SingleOrDefault(x => x.Id == pizza.Id)?.Quantity ?? 0;
            Quantity.Text = QuantityModifier.Value.ToString();
        }

        private void QuantityModifier_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            Quantity.Text = QuantityModifier.Value.ToString();
        }

        private async void AddToCommand_OnClicked(object sender, EventArgs e)
        {
            MainPage.CurrentOrder.SetItem(new OrderItem
            {
                Id = _pizza.Id,
                Quantity = (int)QuantityModifier.Value,
                UnitPrice = _pizza.Price
            });

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}