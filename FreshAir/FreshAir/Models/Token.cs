using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using SQLite;

namespace FreshAir.Models
{
    [DataContract]
    [Table("Token")]
    public class Token
    {
        public string AccessToken { set; get; }
    }
}
