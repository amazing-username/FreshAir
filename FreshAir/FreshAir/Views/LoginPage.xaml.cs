using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messier16.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FreshAir.Management;

using User = FreshAir.Management.LoginManager.User;
using UserDBModel = FreshAir.Management.DatabaseManagement.User;

namespace FreshAir.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            PopulateCredentials();
        }


        private void PopulateCredentials()
        {
            DatabaseManagement db = new DatabaseManagement();
            if (!db.TableExists("User"))
            {
                return;
            }

            var usr = db.RetrieveCredentials();
            UserName.Text = usr.Username;
            Password.Text = usr.Password;
            WillSaveCredentials.Checked = usr.SaveCredentials;
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Login.IsEnabled = false;
            LoginManager lm = new LoginManager(new User{
                Username = UserName.Text.ToString(),
                Password = Password.Text.ToString()
            });
            lm.Login();
            Login.IsEnabled = true;
            if (lm.LoginSuccessful())
            {
                lm.SaveSettings();
                lm.SaveToken();
                if (WillSaveCredentials.Checked)
                    lm.SaveCredentials();
                App.Current.MainPage = new LandingPage();
            }
        }

        private void ForgotPassword_Clicked(object sender, EventArgs e)
        {

        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Register.IsEnabled = false;
            Navigation.PushModalAsync(new RegisterPage());
            Register.IsEnabled = true;
        }
    }
}
