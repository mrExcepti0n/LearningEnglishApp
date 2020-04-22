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
    public class LoginViewModel : ExtendedBindableObject
    {
        private string _userName;
        private string _password;

        private bool _isValid;
        private bool _isLogin;
        private string _authUrl;

        private IOpenUrlService _openUrlService;
        private IIdentityService _identityService;
        public INavigation Navigation;

        public LoginViewModel(IOpenUrlService openUrlService, IIdentityService identityService)
        {
            _openUrlService = openUrlService;
            _identityService = identityService;

            UserName = "admin";
            Password = "123";
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }


        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
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

        public ICommand RegisterCommand => new Command(async () => await RegisterAsync());     



        private async Task SignInAsync()
        {
            Application.Current.MainPage = new NavigationPage(new MainView());

            //await Task.Delay(10);
            //LoginUrl = _identityService.CreateAuthorizationRequest();
            //IsValid = true;
            //IsLogin = true;
        }

        private async Task RegisterAsync()
        {
           await  _openUrlService.OpenUrlAsync(GlobalSetting.Instance.RegisterWebsite);
        }

    }
}
