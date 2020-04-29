using IdentityModel.Client;
using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Services.Identity;
using LearningEnglishMobile.Core.Services.OpenUrl;
using LearningEnglishMobile.Core.Services.Settings;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool _isLogin;
        private string _authUrl;

        private IOpenUrlService _openUrlService;
        private IIdentityService _identityService;
        private ISettingsService _settingsService;

        public LoginViewModel(ISettingsService settingsService, IOpenUrlService openUrlService, IIdentityService identityService)
        {
            _openUrlService = openUrlService;
            _identityService = identityService;
            _settingsService = settingsService;        
        }

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
                RaisePropertyChanged(() => IsLogin);
            }
        }

        public string LoginUrl
        {
            get
            {
                return _authUrl;
            }
            set
            {
                _authUrl = value;
                RaisePropertyChanged(() => LoginUrl);
            }
        }


        public ICommand SignInCommand => new Command(async () => await SignInAsync());

        public ICommand RegistryCommand => new Command(async () => await RegistryAsync());
        public ICommand NavigateCommand => new Command<string>(async (url) => await NavigateAsync(url));

        public ICommand LogoutCommand => new Command(() => Logout());

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is LogoutParameter)
            {
                var logoutParameter = (LogoutParameter)navigationData;

                if (logoutParameter.Logout)
                {
                    Logout();
                }
            }

            return base.InitializeAsync(navigationData);
        }

        private async Task NavigateAsync(string url)
        {
            var unescapedUrl = System.Net.WebUtility.UrlDecode(url);

            if (unescapedUrl.Equals(GlobalSetting.Instance.LogoutCallback))
            {
                _settingsService.AuthAccessToken = string.Empty;
                _settingsService.AuthIdToken = string.Empty;
                IsLogin = false;
                LoginUrl = _identityService.CreateAuthorizationRequest();
            }
            else if (unescapedUrl.Contains(GlobalSetting.Instance.Callback))
            {
                var authResponse = new AuthorizeResponse(url);
                if (!string.IsNullOrWhiteSpace(authResponse.Code))
                {
                    var userToken = await _identityService.GetTokenAsync(authResponse.Code);
                    string accessToken = userToken.AccessToken;

                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        _settingsService.AuthAccessToken = accessToken;
                        _settingsService.AuthIdToken = authResponse.IdentityToken;
                        await NavigationService.NavigateToAsync<MainViewModel>();
                        //await NavigationService.RemoveLastFromBackStackAsync();
                    }
                }
            }
        }


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

        private async Task SignInAsync()
        {
            await Task.Delay(10);
            LoginUrl = _identityService.CreateAuthorizationRequest();
            IsLogin = true;
        }

        private async Task RegistryAsync()
        {
            await _openUrlService.OpenUrlAsync(GlobalSetting.Instance.RegisterWebsite);
        }


        private void Logout()
        {
            var authIdToken = _settingsService.AuthIdToken;
            var logoutRequest = _identityService.CreateLogoutRequest(authIdToken);

            if (!string.IsNullOrEmpty(logoutRequest))
            {
                // Logout
                LoginUrl = logoutRequest;
            }
        }

    }
}
