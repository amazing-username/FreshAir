using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FreshAir.Management;

namespace FreshAir.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangePasswordPage : ContentPage
	{
        private DatabaseManagement DBMgr { set; get; }
        private LoginManager UserMgr { set; get; }


		public ChangePasswordPage ()
		{
            DBMgr = new DatabaseManagement();
			InitializeComponent ();
		}


        private LoginManager.UserModel UserWithNewPassword(string newPassword)
        {
            var userFromDB = DBMgr.RetrieveUser();

            return new LoginManager.UserModel
            {
                FirstName = "d",
                Lastname = "df",
                Email = "dfdfd@iii.com",
                Username = userFromDB.Username,
                Password = newPassword,
            };
        }
        private DatabaseManagement.User UpdatedUser(string password)
        {
            var user = DBMgr.RetrieveUser();
            user.Password = password;

            return user;
        }

        private bool CurrentPasswordIsValid(string currentPassword)
        {
            var username = DBMgr.RetrieveUser().Username;
            UserMgr = new LoginManager(new LoginManager.User
            {
                Username = username,
                Password = currentPassword
            });
            UserMgr.Login();
            var result = UserMgr.Result;

            if (result == null)
                return false;

            DBMgr.SaveToken(new Models.Token
            {
                AccessToken = result.AccessToken
            });

            return true;
        }

        private void ChangePassword_Clicked(object sender, EventArgs e)
        {
            ChangePassword.IsEnabled = false;

            var currentPassword = CurrentPassword.Text.ToString();
            var newPassword = NewPassword.Text.ToString();
            var newPasswordConfirm = NewPasswordConfirm.Text.ToString();

            if (!CurrentPasswordIsValid(currentPassword))
            {
                ChangePassword.IsEnabled = true;
                return;
            }
            var user = UserWithNewPassword(newPassword);
            var updatedUser = UpdatedUser(newPassword);

            UserMgr.ChangePassword(user);
            DBMgr.SaveUser(updatedUser);

            ChangePassword.IsEnabled = true;
        }
    }
}