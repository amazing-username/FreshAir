using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FreshAir.Views;

namespace FreshAir.ViewModels
{
    public class LandingPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LandingPageMenuItem> MenuItems { get; set; }
        
        public LandingPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<LandingPageMenuItem>(new[]
            {
                new LandingPageMenuItem { Id = 0, Title = "Home" },
                new LandingPageMenuItem(1) { Id = 1, Title = "Settings" }
            });
        }
            
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
