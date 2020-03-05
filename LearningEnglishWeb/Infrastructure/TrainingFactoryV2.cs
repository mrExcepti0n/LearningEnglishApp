using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public class TrainingFactoryV2
    {
        private IVocabularyService _vocabularyService;
        private IWordImageService _wordImageService;
        public TrainingFactoryV2(IVocabularyService vocabularyService, IWordImageService wordImageService)
        {
            _vocabularyService = vocabularyService;
            _wordImageService = wordImageService;
        }

        public async Task<CollectWordTraining> GetCollectWordTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var factory = new CollectWordTrainingFactory(_vocabularyService, _wordImageService, fromLanguage, toLanguage, isReverseWay);
            return await factory.GetTraining();
        }


        public async Task<ChooseTranslateTraining> GetChooseTranslateTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var factory = new ChooseTranslateTrainingFactory(_vocabularyService, _wordImageService, fromLanguage, toLanguage, isReverseWay);
            return await factory.GetTraining();
        }

        internal async Task<TranslateWordTraining> GetTranslateTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            return await new TranslateWordTrainingFactory(_vocabularyService, _wordImageService, fromLanguage, toLanguage, isReverseWay).GetTraining();
        }
    }
}
