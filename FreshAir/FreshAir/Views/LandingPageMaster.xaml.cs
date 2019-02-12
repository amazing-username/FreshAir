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

using FreshAir.ViewModels;

namespace FreshAir.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPageMaster : ContentPage
    {
        public LandingPageMasterViewModel ViewModel { set; get; }
        public ListView ListView;

        public LandingPageMaster()
        {
            InitializeComponent();

            BindingContext = ViewModel = new LandingPageMasterViewModel();
            ListView = MenuItemsListView;
        }

    }
}