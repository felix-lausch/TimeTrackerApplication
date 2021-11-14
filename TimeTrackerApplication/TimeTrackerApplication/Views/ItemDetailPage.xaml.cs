using System.ComponentModel;
using TimeTrackerApplication.ViewModels;
using Xamarin.Forms;

namespace TimeTrackerApplication.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}