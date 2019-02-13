using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using RestSharp;

using FreshAir.Management;
using FreshAir.Models;

using UserDBModel = FreshAir.Management.DatabaseManagement.User;

namespace FreshAir.Management
{
    public class LoginManager : FreshAir.Models.Management
    {
        public LoginManager()
        {
            Initialize();
        }
        public LoginManager(User user)
        {
            UserAccount = user;
            Result = null;
            Initialize();
        }

        public void Login()
        {
            Client = new RestClient(UrlBase);
            Request = new RestRequest("api/login/", Method.POST);
            var userJson = JsonConvert.SerializeObject(UserAccount);
            Request.AddParameter("application/json; charset=utf-8", userJson, ParameterType.RequestBody);
            Request.RequestFormat = DataFormat.Json;

            Response = Client.Execute(Request);

            Result = JsonConvert.DeserializeObject<LoginResult>(Response.Content);
        }
        public void Logout(Token token)
        {
            Client = new RestClient(UrlBase);
            Request = new RestRequest("api/logout/", Method.POST);
            var userJson = JsonConvert.SerializeObject(UserAccount);
            Request.AddHeader("Authorization", $"token {token.AccessToken}");
            Request.AddParameter("application/json; charset=utf-8", userJson, ParameterType.RequestBody);
            Request.RequestFormat = DataFormat.Json;

            Response = Client.Execute(Request);

            ResultLogout = JsonConvert.DeserializeObject<LogoutResult>(Response.Content);
        }
        public void SaveCredentials()
        {
            var user = new UserDBModel
            {
                Id = Result.Id,
                Username = UserAccount.Username,
                Password = UserAccount.Password,
                SaveCredentials = true
            };
            DatabaseManagement freshAirDatabase = new DatabaseManagement();
            freshAirDatabase.SaveCredentials(user);
        }
        public void SaveToken()
        {
            var token = new Token
            {
                AccessToken = Result.AccessToken
            };
            DatabaseManagement freshAirDatabase = new DatabaseManagement();
            freshAirDatabase.SaveToken(token);
        }
        public void SaveSettings()
        {
            var setting = new Settings
            {
                DarkTheme = false
            };
            DatabaseManagement freshAirDatabase = new DatabaseManagement();
            freshAirDatabase.SaveSettings(setting);
        }

        public bool LoginSuccessful()
        {
            if (Result.Errors == null || Result.Errors.Count < 0)
            {
                return true;
            }

            return false;
        }

        public User UserAccount { set; get; }
        public LoginResult Result { set; get; }
        public LogoutResult ResultLogout { set; get; }

        public class User : BaseUser
        {
            [JsonProperty("password")] 
            public string Password { set; get; }
        }
    }
}
