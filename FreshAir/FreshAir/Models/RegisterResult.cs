using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreshAir.Models
{
    public class RegisterResult
    {
        [JsonProperty("detail")]
        public string Detail { set; get; }
        [JsonProperty("registered")]
        public bool Registered { set; get; }
    }
}
