using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshAir.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPageMaster : ContentPage
    {
        public ListView ListView;

        public LandingPageMaster()
        {
            InitializeComponent();

            BindingContext = new LandingPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class LandingPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<LandingPageMenuItem> MenuItems { get; set; }
            
            public LandingPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<LandingPageMenuItem>(new[]
                {
                    new LandingPageMenuItem { Id = 0, Title = "FreshAir" },
                    new LandingPageMenuItem { Id = 1, Title = "Settings" },
                    new LandingPageMenuItem { Id = 2, Title = "Page 3" },
                    new LandingPageMenuItem { Id = 3, Title = "Page 4" },
                    new LandingPageMenuItem { Id = 4, Title = "Page 5" },
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
}