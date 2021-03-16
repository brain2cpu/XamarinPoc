using Xamarin.Forms.Xaml;
using XamarinPoc.Models;
using XamarinPoc.ViewModels;

namespace XamarinPoc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPage
    {
        public OrderStatusPage(OrderStatus order)
        {
            InitializeComponent();

            ((OrderStatusViewModel) BindingContext).Order = order;
        }
    }
}