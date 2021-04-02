using Xamarin.Forms.Xaml;
using XamarinPoc.Models;

namespace XamarinPoc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPage
    {
        private readonly int _orderId;

        public OrderStatusPage(OrderStatus order)
        {
            InitializeComponent();

            _orderId = order.Id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            OrderId.Text = _orderId.ToString();
            OrderStatus.Text = await MainPage.Delivery.GetOrderStatusAsync(_orderId);
        }
    }
}