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
        public void ChangePassword(UserModel user)
        {
            Client = new RestClient(UrlBase);
            DatabaseManagement dbMgr = new DatabaseManagement();
            var userId = dbMgr.RetrieveUser().Id;
            Request = new RestRequest($"api/user/{userId}/", Method.PUT);
            var userJson = JsonConvert.SerializeObject(user);
            Request.AddParameter("application/json; charset=utf-8", userJson, ParameterType.RequestBody);
            Request.RequestFormat = DataFormat.Json;

            Response = Client.Execute(Request);

            var respStr = Response.Content.ToString();
        }

        public bool LoginSuccessful()
        {
            if (Result.Errors == null || Result.Errors.Count < 0)
            {
                return true;
            }

            InitializeSettings();

            return false;
        }

        public UserModel UserObject { set; get; }
        public User UserAccount { set; get; }
        public LoginResult Result { set; get; }
        public LogoutResult ResultLogout { set; get; }

        public class User : BaseUser
        {
            [JsonProperty("password")] 
            public string Password { set; get; }
        }
        public class UserModel : User
        {
            [JsonProperty("first_name")]
            public string FirstName { set; get; }
            [JsonProperty("last_name")]
            public string Lastname { set; get; }
            [JsonProperty("email")]
            public string Email { set; get; }
        }

        private void InitializeSettings()
        {
            DatabaseManagement db = new DatabaseManagement();
            db.SaveSettings(new Settings
            {
                Id = Result.Id,
                DarkTheme = false
            });
        }
    }
}
