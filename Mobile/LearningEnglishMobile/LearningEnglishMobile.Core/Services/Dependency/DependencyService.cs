using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Services.Dependency
{
    public class DependencyService : IDependencyService
    {
        public T Get<T>() where T : class
        {
            return Xamarin.Forms.DependencyService.Get<T>();
        }
    }
}
