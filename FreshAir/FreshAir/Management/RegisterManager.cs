﻿using System;
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
            Result = null;
            Initialize();
        }

        public void RegisterAccount()
        {
            Client = new RestClient(UrlBase);
            Request = new RestRequest("api/register/user/", Method.POST);
            var userJson = JsonConvert.SerializeObject(UserAccount);
            Request.AddParameter("application/json; charset=utf-8", userJson, ParameterType.RequestBody);
            Request.RequestFormat = DataFormat.Json;

            Response = Client.Execute(Request);

            Result = JsonConvert.DeserializeObject<RegisterResult>(Response.Content);
        }

        public bool RegistrationSuccessful()
        {
            if (Result == null || !Result.Registered)
                return false;

            return true;
        }

        public RegisterResult Result { set; get; }
        public User UserAccount { set; get; }
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

}
