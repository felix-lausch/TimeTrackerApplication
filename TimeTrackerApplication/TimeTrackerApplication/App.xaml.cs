using System;
using TimeTrackerApplication.Services;
using TimeTrackerApplication.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTrackerApplication
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<TimeEntryDatastore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
