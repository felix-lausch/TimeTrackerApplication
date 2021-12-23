namespace TimeTrackerApplication.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Xamarin.Forms;

    internal class TimeEntryBehavior : Behavior<Entry>
    {
        const string timeRegex = "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        public void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var IsValid = (Regex.IsMatch(e.NewTextValue, timeRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
