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
        public void SwitchScheme(bool darkTheme)
        {
            if (darkTheme)
            {
                App.Current.Resources["NavigationPrimary"] = Color.FromHex("#2196F3");                App.Current.Resources["ContentBackground"] = Color.FromHex("ffffff");                App.Current.Resources["Button"] = Color.FromHex("#787878");                App.Current.Resources["ButtonText"] = Color.FromHex("#ffffff");                App.Current.Resources["TextColor"] = Color.FromHex("#999999");                App.Current.Resources["Rando"] = Color.FromHex("#ffffff");                App.Current.Resources["LabelFrontground"] = Color.FromHex("#999999");                App.Current.Resources["PickerBackgroundColor"] = Color.FromHex("#787878");                App.Current.Resources["PickerTextColor"] = Color.FromHex("#999999");                App.Current.Resources["SliderLineColor"] = Color.FromHex("#000000");                App.Current.Resources["SliderThumbColor"] = Color.FromHex("#f3f3f3");            }            else            {                App.Current.Resources["NavigationPrimary"] = Color.FromHex("#2d2d2d");                App.Current.Resources["ContentBackground"] = Color.FromHex("#202020");                App.Current.Resources["Button"] = Color.FromHex("#787878");                App.Current.Resources["ButtonText"] = Color.FromHex("#ffffff");                App.Current.Resources["TextColor"] = Color.FromHex("#999999");                App.Current.Resources["Rando"] = Color.FromHex("#ffffff");                App.Current.Resources["LabelFrontground"] = Color.FromHex("#999999");                App.Current.Resources["PickerBackgroundColor"] = Color.FromHex("#787878");                App.Current.Resources["PickerTextColor"] = Color.FromHex("#999999");                App.Current.Resources["SliderLineColor"] = Color.FromHex("#ffffff");                App.Current.Resources["SliderThumbColor"] = Color.FromHex("#8686ff");
            }
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
