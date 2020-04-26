using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningEnglishMobile.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
        public MainView()
        {
            InitializeComponent();
            MasterView.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;         

            await NavigateToPage(page);
            IsPresented = false;

            MasterView.ListView.SelectedItem = null;
        }

        private async Task NavigateToPage(Page page)
        {
            if (Detail.Navigation.NavigationStack.LastOrDefault().GetType() != page.GetType())
            {
                await Detail.Navigation.PushAsync(page);
            }
        }
    }
}