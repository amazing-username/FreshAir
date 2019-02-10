using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using RestSharp;

using FreshAir.Management;
using FreshAir.Models;

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

        public class User : BaseUser
        {
            [JsonProperty("password")] 
            public string Password { set; get; }
        }
    }
}
