using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FreshAir.Management;
using FreshAir.Models;

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
		}

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
            App.Current.MainPage = new LoginPage();
        }
    }
}