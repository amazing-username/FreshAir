using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
