using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Views;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView(new LogoutParameter { Logout = false});           
        }
    }
    
}
