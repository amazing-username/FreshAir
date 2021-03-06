﻿using System;
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
                db.CloseDB();
                return;
            }

            var usr = db.RetrieveUser();
            if (usr.Username == null)
            {
                usr.Username = " ";
            }
            if (usr.Password == null)
            {
                usr.Password = " ";
            }
            if (usr.SaveCredentials == null)
            {
                usr.SaveCredentials = false;
            }

            UserName.Text = usr.Username ?? " ";
            Password.Text = usr.Password ?? " ";
            db.CloseDB();
            WillSaveCredentials.Checked = usr.SaveCredentials;
        }
        private void InitializeFreshAirForUser(LoginManager lMgr)
        {
            DatabaseManagement dbMgr = new DatabaseManagement();
            dbMgr.SaveSettings(new Models.Settings
            {
                Id = lMgr.Result.Id,
                DarkTheme = false
            });
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
                if (lm.Result.IsFirstLogin)
                {
                    InitializeFreshAirForUser(lm);
                }
                DatabaseManagement settingsData = new DatabaseManagement();
                var res = settingsData.RetrieveSettings(lm.Result.Id);
                SettingManager settingMgr = new SettingManager(res);
                settingMgr.LoadScheme();

                settingsData.SaveToken(new Models.Token
                {
                    AccessToken = lm.Result.AccessToken
                });
                if (WillSaveCredentials.Checked)
                {
                    settingsData.SaveUser(new UserDBModel
                    {
                        Id = lm.Result.Id,
                        Firstname = lm.Result.Firstname,
                        Lastname = lm.Result.Lastname,
                        Email = lm.Result.Email,
                        Username = lm.Result.Username,
                        Password = Password.Text.ToString(),
                        SaveCredentials = WillSaveCredentials.Checked
                    });
                }
                settingsData.CloseDB();
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
