using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class SettingsViewModel
    {
        //public ICommand LogoutCommand => new Command(async () => await LogoutAsync());

        //private async Task LogoutAsync()
        //{
        //    // Logout
        //    Application.Current.MainPage = new LoginView(new LogoutParameter { Logout = true });
        //    await RemoveBackStackAsync();
        //}

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

    }
}
