using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Services.Dependency
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}
