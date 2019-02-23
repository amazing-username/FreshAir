using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using FreshAir.Models;

namespace FreshAir.Management
{
    public class SettingManager
    {
        public SettingManager()
        {
            Initialize();
        }
        public SettingManager(Settings setting)
        {
            UserSettings = setting;
            Initialize();
        }

        public void SwitchTheme()
        {
            Initialize();
            ConfigureSettings();
            LoadScheme();
        }
        public void LoadScheme()
        {
            if (!UserSettings.DarkTheme)
            {
                App.Current.Resources["NavigationPrimary"] = Color.FromHex("#159f64");
                App.Current.Resources["ContentBackground"] = Color.FromHex("f4f4f4");
                App.Current.Resources["Button"] = Color.FromHex("#098751");
                App.Current.Resources["ButtonText"] = Color.FromHex("#ffffff");
                App.Current.Resources["TextColor"] = Color.FromHex("#000000");
                App.Current.Resources["Rando"] = Color.FromHex("#ffffff");
                App.Current.Resources["LabelFrontground"] = Color.FromHex("#ffffff");
                App.Current.Resources["PickerBackgroundColor"] = Color.FromHex("#787878");
                App.Current.Resources["SwitchOn"] = Color.FromHex("#12a90d");
                App.Current.Resources["PickerTextColor"] = Color.FromHex("#999999");
                App.Current.Resources["SliderLineColor"] = Color.FromHex("#000000");
                App.Current.Resources["SliderThumbColor"] = Color.FromHex("#f3f3f3");
            }
            else
            {
                App.Current.Resources["NavigationPrimary"] = Color.FromHex("#2d2d2d");
                App.Current.Resources["ContentBackground"] = Color.FromHex("#202020");
                App.Current.Resources["Button"] = Color.FromHex("#09633c");
                App.Current.Resources["ButtonText"] = Color.FromHex("#e5e5e5");
                App.Current.Resources["TextColor"] = Color.FromHex("#e5e5e5");
                App.Current.Resources["Rando"] = Color.FromHex("#e5e5e5");
                App.Current.Resources["LabelFrontground"] = Color.FromHex("#ffffff");
                App.Current.Resources["PickerBackgroundColor"] = Color.FromHex("#787878");
                App.Current.Resources["SwitchOn"] = Color.FromHex("#12a90d");
                App.Current.Resources["PickerTextColor"] = Color.FromHex("#999999");
                App.Current.Resources["SliderLineColor"] = Color.FromHex("#ffffff");
                App.Current.Resources["SliderThumbColor"] = Color.FromHex("#8686ff");
            }
        }
        public void LoadDefault()
        {
                App.Current.Resources["NavigationPrimary"] = Color.FromHex("#159f64");
                App.Current.Resources["ContentBackground"] = Color.FromHex("f4f4f4");
                App.Current.Resources["Button"] = Color.FromHex("#098751");
                App.Current.Resources["ButtonText"] = Color.FromHex("#ffffff");
                App.Current.Resources["TextColor"] = Color.FromHex("#000000");
                App.Current.Resources["Rando"] = Color.FromHex("#ffffff");
                App.Current.Resources["LabelFrontground"] = Color.FromHex("#ffffff");
                App.Current.Resources["PickerBackgroundColor"] = Color.FromHex("#787878");
                App.Current.Resources["SwitchOn"] = Color.FromHex("#12a90d");
                App.Current.Resources["PickerTextColor"] = Color.FromHex("#999999");
                App.Current.Resources["SliderLineColor"] = Color.FromHex("#000000");
                App.Current.Resources["SliderThumbColor"] = Color.FromHex("#f3f3f3");
        }

        private void Initialize()
        {
        }
        public void ConfigureSettings()
        {
            DatabaseManagement db = new DatabaseManagement();
            UserSettings.Id = db.RetrieveUser().Id;
            db.SaveSettings(UserSettings);
        }

        private bool IsDarkTheme()
        {
            var result = false;
            DatabaseManagement db = new DatabaseManagement();

            result = db.RetrieveSettings().DarkTheme;

            return result;
        }


        private Settings UserSettings { set; get; }
    }
}
