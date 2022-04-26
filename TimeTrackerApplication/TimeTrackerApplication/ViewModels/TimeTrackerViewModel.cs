using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimeTrackerApplication.Models;
using TimeTrackerApplication.Views;
using Xamarin.Forms;

namespace TimeTrackerApplication.ViewModels
{
    public class TimeTrackerViewModel : BaseViewModel
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private Task _infoTask = Task.CompletedTask;
        private string _infoText = string.Empty;
        private string _infoTextColor = "ForestGreen";
        private string _startTimeEntry = string.Empty;
        private string _endTimeEntry = string.Empty;
        private string _pauseEntry = string.Empty;

        public ObservableCollection<TimeEntry> Entries { get; }
        public Command LoadEntriesCommand { get; }
        public Command AddEntryCommand { get; }

        public Command<TimeEntry> UpdateEntryCommand { get; }

        public Command RefreshEntriesCommand { get; }

        public Command<TimeEntry> TimeEntryTapped { get; }

        private string startEntry = string.Empty;
        public string StartEntry {
            get { return startEntry; }
            set { SetProperty(ref startEntry, value); }
        }

        public TimeTrackerViewModel()
        {
            Title = "Time Tracker";
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
            Entries.Clear();
            var entries = await DataStore.GetItemsAsync(true);
            
            foreach (var entry in entries)
            {
                Entries.Add(entry);
            }
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

        public string InfoText {
            get => _infoText;
            set => SetProperty(ref _infoText, value);
        }

        public string InfoTextColor {
            get => _infoTextColor;
            set => SetProperty(ref _infoTextColor, value);
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
            var timeEntryBefore = timeEntry;

            // assign hours & minutes from new user input
            var (startHours, startMinutes) = ParseTimeEntry(timeEntry.DisplayStartTime);
            var (endHours, endMinutes) = ParseTimeEntry(timeEntry.DisplayEndTime);

            timeEntry.StartHours= startHours;
            timeEntry.StartMinutes = startMinutes;
            timeEntry.EndHours = endHours;
            timeEntry.EndMinutes = endMinutes;

            var result = await DataStore.UpdateItemAsync(timeEntry);

            result.Switch (
                async entry => await HandleInfoText("Entry updated"),
                async error => await HandleInfoText(error, success: false));
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

        private async Task HandleInfoText(string infoText, bool success = true)
        {
            if (_infoTask.Status == TaskStatus.Running)
            {
                tokenSource.Cancel();
            }

            InfoTextColor = success ? "ForestGreen" : "PaleVioletRed";
            InfoText = infoText;
            
            _infoTask = Task.Delay(2000, tokenSource.Token);
            await _infoTask;

            InfoText = string.Empty;
        }
    }
}