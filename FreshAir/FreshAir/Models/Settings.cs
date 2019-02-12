using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using SQLite;

namespace FreshAir.Models
{
    [DataContract]
    public class Settings
    {
        public bool DarkTheme { set; get; }
    }
}
