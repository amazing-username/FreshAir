using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg.Plugins.Popup.Pages;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FreshAir.Management;
using FreshAir.Models;

using User = FreshAir.Management.RegisterManager.User;

namespace FreshAir.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async Task<User> PopulateUser()
        {
            return new User
            {
                Firstname = Firstname.Text.ToString(),
                Lastname = Lastname.Text.ToString(),
                Email = Email.Text.ToString(),
                Username = Username.Text.ToString(),
                Password = Password.Text.ToString(),
                PasswordConfirm = PasswordConfirm.Text.ToString()
            };
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            Register.IsEnabled = false;

            var user = await PopulateUser();

            RegisterManager regUser = new RegisterManager(user);
            regUser.RegisterAccount();

            Register.IsEnabled = true;
        }
    }

}