using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected INavigation Navigation { get; }
    }
}
