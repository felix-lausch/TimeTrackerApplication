using System;
using System.Collections.Generic;
using System.ComponentModel;
using TimeTrackerApplication.Models;
using TimeTrackerApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTrackerApplication.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}