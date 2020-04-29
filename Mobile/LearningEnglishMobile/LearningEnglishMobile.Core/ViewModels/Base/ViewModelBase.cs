using LearningEnglishMobile.Core.Services.Navigation;
using LearningEnglishMobile.Core.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected INavigationService NavigationService { get; }

        public ViewModelBase()
        {
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
