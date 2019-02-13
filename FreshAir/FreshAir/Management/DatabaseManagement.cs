using System;
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
        public void SaveToken(Token token)
        {
            if (!TableExists("Token"))
                DB.CreateTable<Token>();
            DB.DeleteAll<Token>();

            DB.Insert(token);
        }
        public void SaveSettings(Settings settings)
        {
            if (!TableExists("Settings"))
            {
                DB.CreateTable<Settings>();
                DB.Insert(settings);
            }
        }
        
        public User RetrieveCredentials()
        {
            return DB.Table<User>().FirstOrDefault();
        }
        public Token RetrieveToken()
        {
            return DB.Table<Token>().FirstOrDefault();
        }
        public Settings RetrieveSettings()
        {
            return DB.Table<Settings>().FirstOrDefault();
        }

        public bool TableExists(string tablename)
        {
            var result = 0;
            try
            {
                result = DB.ExecuteScalar<int>($"select * from {tablename};");
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
            [PrimaryKey, Column("Id")]
            public int Id { set; get; }
            public string Password { set; get; }
            public bool SaveCredentials { set; get; }
        }
    }
}
