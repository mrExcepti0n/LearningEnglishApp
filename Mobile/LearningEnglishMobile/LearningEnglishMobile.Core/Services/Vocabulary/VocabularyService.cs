using LearningEnglishMobile.Core.Helpers;
using LearningEnglishMobile.Core.Models.Vocabulary;
using LearningEnglishMobile.Core.Services.RequestProvider;
using LearningEnglishMobile.Core.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.Vocabulary
{
    public class VocabularyService : IVocabularyService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService _settingsService;

        private const string ApiUrlBase = "api/vocabulary";

        public VocabularyService(IRequestProvider requestProvider,  ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }


        public async Task<IEnumerable<UserWord>> GetUserWords(int id)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.VocabularyEndpoint, $"{ApiUrlBase}/words?vocabularyid={id}");
            var token = _settingsService.AuthAccessToken;
            List<UserWord> result = await _requestProvider.GetAsync<List<UserWord>>(uri, token);

            return result;
        }

        public async Task<IEnumerable<UserVocabulary>> GetVocabularies()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.VocabularyEndpoint, ApiUrlBase);
            var token = _settingsService.AuthAccessToken;
            List<UserVocabulary> result  = await _requestProvider.GetAsync<List<UserVocabulary>>(uri, token);

            return result;
        }
    }
}
