using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreshAir.Models
{
    public class LogoutResult
    {
        [JsonProperty("detail")]
        public string Detail { set; get; }
    }
}
