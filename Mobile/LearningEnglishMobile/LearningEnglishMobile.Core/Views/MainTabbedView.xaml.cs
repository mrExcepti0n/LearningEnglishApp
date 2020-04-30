using LearningEnglishMobile.Core.ViewModels;
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
    public partial class MainTabbedView : TabbedPage
    {
        public MainTabbedView()
        {
            InitializeComponent();
        }

        private async Task InitializeCurrentViewModel()
        {
            await ((ViewModelBase)CurrentPage.BindingContext).InitializeAsync(null);
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            await InitializeCurrentViewModel();
        }
    }
}