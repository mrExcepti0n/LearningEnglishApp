using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.OpenUrl
{
    public interface IOpenUrlService
    {
        Task OpenUrlAsync(string url);
    }
}
