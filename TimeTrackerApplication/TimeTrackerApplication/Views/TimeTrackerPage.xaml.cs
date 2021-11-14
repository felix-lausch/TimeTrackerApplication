using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackerApplication.Models;
using TimeTrackerApplication.ViewModels;
using TimeTrackerApplication.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTrackerApplication.Views
{
    public partial class TimeTrackerPage : ContentPage
    {
        TimeTrackerViewModel _viewModel;

        public TimeTrackerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TimeTrackerViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}