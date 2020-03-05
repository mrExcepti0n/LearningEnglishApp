using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public abstract class TrainingFactoryBase<T>
    {

        protected IVocabularyService _vocabularyService;
        protected readonly LanguageEnum _fromLanguage;
        protected readonly LanguageEnum _toLanguage;
        protected readonly bool _reverseWay;
        protected IWordImageService _wordImageService;

        protected TrainingFactoryBase(IVocabularyService vocabularyService, IWordImageService wordImageService, LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay)
        {
            _vocabularyService = vocabularyService;
            _wordImageService = wordImageService;
            _fromLanguage = fromLanguage;
            _toLanguage = toLanguage;
            _reverseWay = reverseWay;
        }

        public abstract Task<T> GetTraining();
    }
}
