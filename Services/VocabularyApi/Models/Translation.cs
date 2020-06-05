using Data.Core;

namespace VocabularyApi.Models
{
    public class WordTranslation
    {
        public WordTranslation()
        {

        }

        public WordTranslation(string translation, LanguageEnum language, VocabularyWord word)
        {
            Translation = translation;
            Language = language;
            Word = word;
        }

        public int  Id { get; set; }

        public int WordId { get; set; }

        public VocabularyWord Word { get; set; }

        public string Translation { get; set; }

        public LanguageEnum Language { get; set; }
    }
}

