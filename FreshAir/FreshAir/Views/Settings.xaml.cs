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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {            if (ItemsListView.SelectedItem == null)                return;            ItemsListView.SelectedItem = null;        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Logout.IsEnabled = false;
            DatabaseManagement dbSql = new DatabaseManagement();
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
            ViewModel.SwitchScheme(val);
            DatabaseManagement dbsql = new DatabaseManagement();
            dbsql.SaveSettings(new Models.Settings
            {
                Id = dbsql.RetrieveCredentials().Id,
                DarkTheme = val
            });
            dbsql.CloseDB();
        }
    }
}