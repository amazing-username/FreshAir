﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using SQLite;

using FreshAir.Models;

namespace FreshAir.Management
{
    public class DatabaseManagement
    {
        public DatabaseManagement()
        {
            Initialize();
        }

        public void DeleteTable<T>(T tablename)
        {
            DB.DropTable<T>();
        }
        public void SaveCredentials(User user)
        {
            if (!TableExists("User"))
                DB.CreateTable<User>();
            DB.DeleteAll<User>();

            DB.Insert(user);
        }
        
        public User RetrieveCredentials()
        {
            return DB.Table<User>().FirstOrDefault();
        }

        public bool TableExists(string tablename)
        {
            var result = 0;
            try
            {
                result = DB.Table<User>().Count();
            }
            catch (SQLiteException se)
            {
                var seMsg = se.Message;
                return false;
            }

            if (result == 0)
                return false;

            return true;
        }

        private void Initialize()
        {
            AppName = "FreshAir";
            DBPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.Personal), AppName);

            DB = new SQLiteConnection(DBPath);
        }


        private SQLiteConnection DB { set; get; }
        private string AppName { set; get; }
        private string DBPath { set; get; }

        [DataContract]
        [Table("User")]
        public class User : BaseUser
        {
            [PrimaryKey, AutoIncrement, Column("Id")]
            public int Id { set; get; }
            public string Password { set; get; }
            public bool SaveCredentials { set; get; }
        }
    }
}
