using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.Services.OpenUrl
{
    public class OpenUrlService : IOpenUrlService
    {
        public async Task OpenUrlAsync(string url)
        {
            //  Device.OpenUri(new Uri(url));
            await Launcher.OpenAsync(new Uri(url));
          
        }
    }
}
