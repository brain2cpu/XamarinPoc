using System;
using Xamarin.Forms;
using XamarinPoc.Views;

namespace XamarinPoc
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // add some kind of logging here to trace unhandled exceptions
        }
    }
}
