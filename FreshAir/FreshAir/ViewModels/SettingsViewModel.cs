using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

using FreshAir.Management;
using FreshAir.Models;
using FreshAir.Views;

namespace FreshAir.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
            Toggables = new ObservableCollection<Toggables>();

            PopulateToggables();
        }

        public void PopulateToggables()
        {
            Toggables.Clear();
            var colorScheme = new Toggables();
            DatabaseManagement fa = new DatabaseManagement();

            colorScheme.Enabled = fa.RetrieveSettings().DarkTheme;
            colorScheme.Function = "Dark Theme";

            Toggables.Add(colorScheme);
            fa.CloseDB();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Toggables> Toggables { set; get; }
    }
}
