using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Services.Settings;
using LearningEnglishMobile.Core.Services.User;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class MasterMainViewModel : ViewModelBase
    {
        public ObservableCollection<MasterMenuItem> MenuItems { get; set; }

        private IUserService _userService { get; set; }
        private ISettingsService _settingsService { get; set; }

        private UserInfo _userInfo;
        public UserInfo UserInfo { 
            get => _userInfo; 
            set { 
                _userInfo = value;
                RaisePropertyChanged(() => UserInfo); 
            } 
        }

        public MasterMainViewModel(IUserService userService, ISettingsService settingsService)
        {
            _userService = userService;
            _settingsService = settingsService;

            MenuItems = new ObservableCollection<MasterMenuItem>(new[]
            {
                new MasterMenuItem { Id = 0, Title = "Settings", TargetType = typeof(SettingsView) }
            });
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var token = _settingsService.AuthAccessToken;
            UserInfo = await _userService.GetUserInfoAsync(token);
        }


        public ICommand LogoutCommand => new Command(async () => await LogoutAsync());

        private async Task LogoutAsync()
        {
            await NavigationService.NavigateToAsync<LoginViewModel>(new LogoutParameter { Logout = true });
            await NavigationService.RemoveBackStackAsync();
        }
    }
}
