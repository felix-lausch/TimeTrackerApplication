using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackerApplication.Models;
using TimeTrackerApplication.Views;
using Xamarin.Forms;

namespace TimeTrackerApplication.ViewModels
{
    public class TimeTrackerViewModel : BaseViewModel
    {
        private string _startTimeEntry = string.Empty;
        private string _endTimeEntry = string.Empty;
        private string _pauseEntry = string.Empty;

        public ObservableCollection<TimeEntry> Entries { get; }
        public Command LoadEntriesCommand { get; }
        public Command AddEntryCommand { get; }

        public Command<TimeEntry> UpdateEntryCommand { get; }

        public Command RefreshEntriesCommand { get; }

        public Command<TimeEntry> TimeEntryTapped { get; }

        public TimeTrackerViewModel()
        {
            Title = "Browse";
            Entries = new ObservableCollection<TimeEntry>();
            LoadEntriesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            TimeEntryTapped = new Command<TimeEntry>(OnTimeEntrySelected);

            AddEntryCommand = new Command(async () => await OnAddEntry());
            UpdateEntryCommand = new Command<TimeEntry>(async (x) => await OnUpdateEntry(x));
            RefreshEntriesCommand = new Command(async () => await OnRefreshEntries());
        }

        private async Task OnRefreshEntries()
        {
            //await ExecuteLoadItemsCommand(); //understand refresh view
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Entries.Clear();
                var entries = await DataStore.GetItemsAsync();

                foreach (var entry in entries)
                {
                    Entries.Add(entry);
                }
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

        public string StartTimeEntry {
            get => _startTimeEntry;
            set => SetProperty(ref _startTimeEntry, value);
        }

        public string EndTimeEntry {
            get => _endTimeEntry;
            set => SetProperty(ref _endTimeEntry, value);
        }

        public string PauseEntry {
            get => _pauseEntry;
            set => SetProperty(ref _pauseEntry, value);
        }

        private async Task OnAddEntry()
        {
            var (startHours, startMinutes) = ParseTimeEntry(StartTimeEntry);
            var (endHours, endMinutes) = ParseTimeEntry(EndTimeEntry);
            var pause = Convert.ToDouble(PauseEntry);

            var timeEntry = new TimeEntry
            {
                StartHours = startHours,
                StartMinutes = startMinutes,
                EndHours = endHours,
                EndMinutes = endMinutes,
                PauseHours = pause,
            };

            var entry = await DataStore.AddItemAsync(timeEntry);
            Entries.Add(entry);

            //reset inputs
            StartTimeEntry = string.Empty;
            EndTimeEntry = string.Empty;
            PauseEntry = string.Empty;
        }

        private async Task OnUpdateEntry(TimeEntry timeEntry)
        {
            var idk = await DataStore.UpdateItemAsync(timeEntry);
            //TODO: what doing here?
        }

        async void OnTimeEntrySelected(TimeEntry timeEntry)
        {
            if (timeEntry is null)
            {
                return;
            }

            var idkifthisshitworks = timeEntry;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private (int Hours, int Minutes) ParseTimeEntry(string timeEntry)
        {
            var strings = timeEntry.Split(':');

            var hours = Convert.ToInt32(strings[0]);
            var minutes = Convert.ToInt32(strings[1]);

            return (hours, minutes);
        }
    }
}