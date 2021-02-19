using Xamarin.Forms;
using XamarinPoc.ViewModels;

namespace XamarinPoc.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((MainViewModel)BindingContext).InitializeAsync();
        }
    }
}
