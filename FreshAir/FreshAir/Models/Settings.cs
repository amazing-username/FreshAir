using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using SQLite;

namespace FreshAir.Models
{
    [DataContract]
    [Table("Settings")]
    public class Settings
    {
        [PrimaryKey, Column("Id")]
        public int Id { set; get; }
        public bool DarkTheme { set; get; }
    }
}
