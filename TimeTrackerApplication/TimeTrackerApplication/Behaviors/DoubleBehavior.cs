namespace TimeTrackerApplication.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;

    internal class DoubleBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = double.TryParse(args.NewTextValue, out _);

            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
