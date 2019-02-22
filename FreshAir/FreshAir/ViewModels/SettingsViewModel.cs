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
            Toggables = new ObservableCollection<Toggable>();
            AccountActions = new ObservableCollection<AccountAction>();

            PopulateToggables();
            PopulateAccountSettings();
        }

        public void PopulateToggables()
        {
            Toggables.Clear();
            var colorScheme = new Toggable();
            DatabaseManagement fa = new DatabaseManagement();

            colorScheme.Enabled = fa.RetrieveSettings().DarkTheme;
            colorScheme.Function = "Dark Theme";

            Toggables.Add(colorScheme);
            fa.CloseDB();
        }
        public void PopulateAccountSettings()
        {
            AccountActions.Clear();
            var changePassword = new AccountAction
            {
                AccountSetting = "ChangePassword",
                SettingText = "Change your password"
            };

            AccountActions.Add(changePassword);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Toggable> Toggables { set; get; }
        public ObservableCollection<AccountAction> AccountActions { set; get; }
    }
}
