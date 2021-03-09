using Xamarin.Forms.Xaml;
using XamarinPoc.Models;
using XamarinPoc.ViewModels;

namespace XamarinPoc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaDetailsPage
    {
        public PizzaDetailsPage(PizzaDetails pizza)
        {
            InitializeComponent();

            ((PizzaDetailsViewModel) BindingContext).SelectedPizza = pizza;
        }
    }
}