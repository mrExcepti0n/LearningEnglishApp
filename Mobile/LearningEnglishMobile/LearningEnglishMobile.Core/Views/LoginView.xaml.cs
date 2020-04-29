using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Services.Identity;
using LearningEnglishMobile.Core.Services.OpenUrl;
using LearningEnglishMobile.Core.Services.RequestProvider;
using LearningEnglishMobile.Core.ViewModels;
using LearningEnglishMobile.Core.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningEnglishMobile.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }
    }
}