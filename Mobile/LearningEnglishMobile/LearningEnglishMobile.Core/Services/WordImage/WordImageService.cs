using LearningEnglishMobile.Core.Helpers;
using LearningEnglishMobile.Core.Services.RequestProvider;
using LearningEnglishMobile.Core.Services.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.WordImage
{
    public class WordImageService : IWordImageService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService _settingsService;

        private const string ApiUrlBase = "api/wordimage";

        public WordImageService(IRequestProvider requestProvider, ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }

        public async Task<Stream> GetWordImageStream(string word)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.VocabularyEndpoint, $"{ApiUrlBase}/{word}");
            var token = _settingsService.AuthAccessToken;
            return await _requestProvider.GetStreamAsync(uri, token);
        }

    }
}
