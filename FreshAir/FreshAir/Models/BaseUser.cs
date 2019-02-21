using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using RestSharp;

namespace FreshAir.Models
{
    public class BaseUser
    {
        [JsonProperty("username")]
        public string Username { set; get; }
    }
}
