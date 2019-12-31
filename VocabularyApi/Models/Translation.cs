namespace VocabularyApi.Models
{
    public class WordTranslation
    {
        public int  Id { get; set; }

        public int WordId { get; set; }

        public VocabularyWord Word { get; set; }

        public string Translation { get; set; }

        public LanguageEnum Language { get; set; }
    }
}

