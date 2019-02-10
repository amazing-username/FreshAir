using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreshAir.Models
{
    public class Status
    {
        [JsonProperty("message")]
        public string Message { set; get; }
        [JsonProperty("state")]
        public string State { set; get; }
        [JsonProperty("date")]
        public DateTime Date { set; get; }
        [JsonProperty("owner")]
        public string Owner { set; get; }
    }
}
