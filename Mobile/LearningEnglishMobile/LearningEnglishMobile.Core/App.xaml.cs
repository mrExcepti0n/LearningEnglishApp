using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Services.Navigation;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }
        }



        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }

            base.OnResume();
        }
    }
    
}
