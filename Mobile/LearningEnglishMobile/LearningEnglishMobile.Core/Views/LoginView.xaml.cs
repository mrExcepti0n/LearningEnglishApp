using LearningEnglishMobile.Core.Services.Identity;
using LearningEnglishMobile.Core.Services.OpenUrl;
using LearningEnglishMobile.Core.Services.RequestProvider;
using LearningEnglishMobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



            BindingContext = new LoginViewModel(new OpenUrlService(), new IdentityService(new RequestProvider())) { Navigation = Navigation };
        }
    }
}