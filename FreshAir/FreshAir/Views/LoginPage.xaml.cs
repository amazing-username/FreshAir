using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FreshAir.Management;

using User = FreshAir.Management.LoginManager.User;

namespace FreshAir.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
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
