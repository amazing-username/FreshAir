using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreshAir.Models
{
    public class LoginResult
    {
        [JsonProperty("id")]
        public int Id { set; get; }
        [JsonProperty("username")]
        public string Username { set; get; }
        [JsonProperty("token")]
        public string AccessToken { set; get; }
        [JsonProperty("first_login")]
        public bool IsFirstLogin { set; get; }
        [JsonProperty("non_field_errors")]
        public List<string> Errors { set; get; }
    }
}
