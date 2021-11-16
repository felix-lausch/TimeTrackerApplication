using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TimeTrackerApplication.Models;
using TimeTrackerApplication.Views;
using Xamarin.Forms;

namespace TimeTrackerApplication.ViewModels
{
    public class TimeTrackerViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<TimeEntry> Entries { get; }
        public Command LoadEntriesCommand { get; }
        public Command AddTimeEntryCommand { get; }
        public Command StartEntryCompleted { get; }
        public Command RefreshEntriesCommand { get; }
        public Command<Item> ItemTapped { get; }

        private string startEntry = string.Empty;
        public string StartEntry {
            get { return startEntry; }
            set { SetProperty(ref startEntry, value); }
        }

        public TimeTrackerViewModel()
        {
            Title = "Browse";
            Entries = new ObservableCollection<TimeEntry>();
            LoadEntriesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //ItemTapped = new Command<Item>(OnItemSelected);

            AddTimeEntryCommand = new Command(async (x) => await OnAddTimeEntry(x));
            RefreshEntriesCommand = new Command(async () => await OnRefreshEntries());
        }

        private async Task OnRefreshEntries()
        {
            //await ExecuteLoadItemsCommand(); //understand refresh view
            Entries.Clear();
            var entries = await DataStore.GetItemsAsync(true);
            
            foreach (var entry in entries)
            {
                Entries.Add(entry);
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Entries.Clear();
                var entries = await DataStore.GetItemsAsync(true);

                foreach (var entry in entries)
                {
                    Entries.Add(entry);
                }
                //TODO: remove
                //var postResult = await DataStore.AddItemAsync(entries.First());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }

        //public Item SelectedItem
        //{
        //    get => _selectedItem;
        //    set
        //    {
        //        SetProperty(ref _selectedItem, value);
        //        OnItemSelected(value);
        //    }
        //}

        private async Task OnAddTimeEntry(object obj)
        {
            //TODO: build post entry
            var start = startEntry.Split(':');

            var timeEntry = new TimeEntry();

            if (int.TryParse(start[0], out int value))
            {
                timeEntry.StartHours = value;
            };

            var isSuccess = await DataStore.AddItemAsync(timeEntry);
            //TODO: send post request
        }

        //async void OnItemSelected(Item item)
        //{
        //    if (item == null)
        //        return;

        //    // This will push the ItemDetailPage onto the navigation stack
        //    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        //}
    }
}