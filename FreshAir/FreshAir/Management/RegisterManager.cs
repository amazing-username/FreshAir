using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using RestSharp;

using FreshAir.Models;

namespace FreshAir.Management 
{
    public class RegisterManager : FreshAir.Models.Management
    {
        public RegisterManager(User user)
        {
            UserAccount = user;
            Initialize();
        }

        public void RegisterAccount()
        {
            Client = new RestClient(UrlBase);
            Request = new RestRequest("api/user/", Method.POST);
            var userJson = JsonConvert.SerializeObject(UserAccount);
            Request.AddParameter("application/json; charset=utf-8", userJson, ParameterType.RequestBody);
            Request.RequestFormat = DataFormat.Json;

            Response = Client.Execute(Request);
        }


        public User UserAccount { set; get; }
        private RestClient Client { set; get; }
        private RestRequest Request { set; get; }
        private IRestResponse Response { set; get; }
    }

    public class User : BaseUser
    {
        [JsonProperty("first_name")]
        public string Firstname { set; get; }
        [JsonProperty("last_name")]
        public string Lastname { set; get; }
        [JsonProperty("email")]
        public string Email { set; get; }
        [JsonProperty("password")]
        public string Password { set; get; }
        [JsonIgnore]
        public string PasswordConfirm { set; get; }
    }
}
