using LearningEnglishMobile.Core.Services.Settings;
using LearningEnglishMobile.Core.ViewModels;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly ISettingsService _settingsService;

        public NavigationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public Task InitializeAsync()
        {
            //if (string.IsNullOrEmpty(_settingsService.AuthAccessToken))
                return NavigateToAsync<LoginViewModel>();
            //else
            //    return NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = GetCurrentPage();

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = GetCurrentPage();

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

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            Page currentPage = GetCurrentPage();


            if (page is LoginView)
            {
                Application.Current.MainPage = page;
            } else if (currentPage is NavigationPage navigationPage)
            {
                await navigationPage.PushAsync(page);
            }  else 
            {
                Application.Current.MainPage = page;
            }
  

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Page GetCurrentPage()
        {
            return Application.Current.MainPage is MasterDetailPage masterDetailPage ? masterDetailPage.Detail : Application.Current.MainPage;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
