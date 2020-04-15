using LearningEnglishMobile.Core.Services.Dependency;
using LearningEnglishMobile.Core.Services.Settings;
using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _identityEndpoint;

        private readonly ISettingsService _settingsService;
        private readonly IDependencyService _dependencyService;

        public SettingsViewModel(ISettingsService settingsService, IDependencyService dependencyService)
        {
            _settingsService = settingsService;
            _dependencyService = dependencyService;

            _identityEndpoint = _settingsService.IdentityEndpointBase;
        }

        public string IdentityEndpoint
        {
            get => _identityEndpoint;
            set
            {
                _identityEndpoint = value;
                if (!string.IsNullOrEmpty(_identityEndpoint))
                {
                    UpdateIdentityEndpoint();
                }
                RaisePropertyChanged(() => IdentityEndpoint);
            }
        }

        public bool UserIsLogged => !string.IsNullOrEmpty(_settingsService.AuthAccessToken);

       
        private void UpdateIdentityEndpoint()
        {
            // Update remote endpoint (save to local storage)
            GlobalSetting.Instance.BaseIdentityEndpoint = _settingsService.IdentityEndpointBase = _identityEndpoint;
        }


    }
}
