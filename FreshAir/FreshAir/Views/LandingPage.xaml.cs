using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshAir.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : MasterDetailPage
    {
        public LandingPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            Init(new LandingPageMenuItem
            {
                Id = 0,
                Title = "Home"
            });
        }

        private void Init(LandingPageMenuItem item)
        {
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as LandingPageMenuItem;
            if (item == null)
                return;

            Init(item);
        }
    }
}