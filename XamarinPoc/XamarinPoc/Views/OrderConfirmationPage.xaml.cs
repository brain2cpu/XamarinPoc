using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPoc.Models;

namespace XamarinPoc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderConfirmationPage
    {
        public OrderConfirmationPage(IEnumerable<PizzaOrder> pizzas)
        {
            InitializeComponent();

            decimal sum = 0m;
            var s = new StringBuilder();
            foreach (var orderItem in MainPage.CurrentOrder.Items)
            {
                var p = pizzas.Single(x => x.Id == orderItem.Id);
                s.AppendLine($"{p.Name} {orderItem.Quantity} x {orderItem.UnitPrice} RON");
                sum += orderItem.Quantity * orderItem.UnitPrice;
            }

            s.AppendLine();
            s.AppendLine($"Total: {sum} RON");

            OrderContent.Text = s.ToString();
        }

        private async void Order_OnClicked(object sender, EventArgs e)
        {
            var orderStatus = await MainPage.Delivery.OrderAsync(MainPage.CurrentOrder);

            await Application.Current.MainPage.Navigation.PushAsync(new OrderStatusPage(orderStatus), true);
        }
    }
}