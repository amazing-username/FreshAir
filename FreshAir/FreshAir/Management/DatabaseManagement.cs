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

        public void CloseDB()
        {
            DB.Close();
        }
        public void DeleteTable<T>(T tablename)
        {
            DB.DropTable<T>();
        }
        public void DeleteRecords<T>()
        {
            DB.DeleteAll<T>();
        }
        public void SaveUser(User user)
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
            try
            {
                if (!TableExists("Settings"))
                {
                    DB.CreateTable<Settings>();
                    DB.Insert(settings);
                    return;
                }
                DB.Update(settings);
            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }
            return;
        }
        
        public User RetrieveUser()
        {
            return DB.Table<User>().FirstOrDefault() ?? new User {Username = " " , Password = " ",
                SaveCredentials = false };
        }
        public Token RetrieveToken()
        {
            return DB.Table<Token>().FirstOrDefault();
        }
        public Settings RetrieveSettings()
        {
            if (!TableExists("Settings"))
            {
                DB.CreateTable<Settings>();
                DB.Insert(new Settings
                {
                    DarkTheme = false
                });
            }

            return DB.Table<Settings>().FirstOrDefault() ?? new Settings
            {
                DarkTheme = false
            };
        }
        public Settings RetrieveSettings(int id)
        {
            try
            {
                if (!TableExists("Settings"))
                {
                    DB.CreateTable<Settings>();
                    DB.Insert(new Settings
                    {
                        DarkTheme = false
                    });
                }

                return DB.Get<Settings>(id);
            }
            catch (Exception e)
            {

            }
            finally
            {
                var sett = new Settings
                {
                    Id = id,
                    DarkTheme = false
                };

                SaveSettings(sett);
            }

            return new Settings
            {
                Id = id,
                DarkTheme = false
            };
        }

        public bool TableExists(string tablename)
        {
            var result = 0;
            try
            {
                result = DB.GetTableInfo(tablename).Count;
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
            public string Firstname { set; get; }
            public string Lastname { set; get; }
            public string Email { set; get; }
            public string Password { set; get; }
            public bool SaveCredentials { set; get; }
        }
    }
}
