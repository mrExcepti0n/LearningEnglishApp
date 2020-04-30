using Data.Core;
using LearningEnglishMobile.Core.Helpers;
using LearningEnglishMobile.Core.Models.Training;
using LearningEnglishMobile.Core.Services.RequestProvider;
using LearningEnglishMobile.Core.Services.Settings;
using LearningEnglishMobile.Core.Services.Training.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.Training
{
    public class TrainingService : ITrainingService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService _settingsService;

        private const string ApiUrlBase = "api/training";

        public TrainingService(IRequestProvider requestProvider, ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }

        public async Task<List<UserWord>> GetRequiringStudyWords(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.VocabularyEndpoint, $"{ApiUrlBase}/RequiringStudyWords?trainingType={trainingType}&isReverseTraining={isReverseTraining}");
            var token = _settingsService.AuthAccessToken;
            List<UserWord> result = await _requestProvider.GetAsync<List<UserWord>>(uri, token);

            return result;
        }


        public async Task<List<QuestionWithOptionsDto>> GetChooseTranslateQuestions(bool isReverseTraining)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.VocabularyEndpoint, $"{ApiUrlBase}/RequiringStudyWords/ChooseTranslate?isReverseTraining={isReverseTraining}");
            var token = _settingsService.AuthAccessToken;
            List<QuestionWithOptionsDto> result = await _requestProvider.GetAsync<List<QuestionWithOptionsDto>>(uri, token);

            return result;
        }
    }
}
