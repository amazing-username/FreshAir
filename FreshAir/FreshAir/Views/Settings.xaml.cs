using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FreshAir.Management;
using FreshAir.Models;
using FreshAir.ViewModels;

using UserLogin = FreshAir.Management.LoginManager.User;
using UserDB = FreshAir.Management.DatabaseManagement.User;

namespace FreshAir.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();

            ViewModel = new SettingsViewModel();

            BindingContext = ViewModel;
		}

        public SettingsViewModel ViewModel { set; get; }

        public bool FirstTime { set; get; }


        private void Logout_Clicked(object sender, EventArgs e)
        {
            Logout.IsEnabled = false;
            DatabaseManagement dbSql = new DatabaseManagement();
            SettingManager settingMgr = new SettingManager();
            settingMgr.LoadDefault();
            var token = dbSql.RetrieveToken();
            UserDB usr = dbSql.RetrieveCredentials();
            LoginManager lm = new LoginManager(new UserLogin
            {
                Username = usr.Username,
                Password = usr.Password
            });
            lm.Logout(token);
            Logout.IsEnabled = true;
            dbSql.CloseDB();
            App.Current.MainPage = new LoginPage();
        }


        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var val = e.Value;
            if (FirstTime)
            {
                FirstTime = false;
                return;
            }
            SettingManager settingMgr = new SettingManager(new Models.Settings
            {
                DarkTheme = val
            });
            settingMgr.SwitchTheme();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (ItemsListView.SelectedItem == null)
                return;
            ItemsListView.SelectedItem = null;
        }
        private void SettingsAccountView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SettingsAccountView.SelectedItem == null)
                return;
            var accountSetting = (AccountAction)SettingsAccountView.SelectedItem;

            if (accountSetting.AccountSetting.Equals("ChangePassword"))
            {
                Navigation.PushModalAsync(new ChangePasswordPage());
            }

            SettingsAccountView.SelectedItem = null;
        }
    }
}