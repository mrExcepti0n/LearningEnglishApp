using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Services.Identity;
using LearningEnglishMobile.Core.Services.OpenUrl;
using LearningEnglishMobile.Core.Services.RequestProvider;
using LearningEnglishMobile.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningEnglishMobile.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView(LogoutParameter logoutParameter)
        {
            InitializeComponent();

            var identityService = new IdentityService(new RequestProvider());
            BindingContext = new LoginViewModel(new OpenUrlService(), identityService, logoutParameter) {
                Navigation = Navigation
            };
        }
    }
}