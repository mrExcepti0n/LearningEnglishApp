using VocabularyApi.IntegrationTests.Builders.Abstractions;
using VocabularyApi.Models;

namespace VocabularyApi.IntegrationTests.Builders
{
    public class UserVocabularyWordBuilder : EntityBuilder<UserVocabularyWord>
    {
        private string _word;
        private string _translation;

        public override UserVocabularyWord Build()
        {
            return new UserVocabularyWord
            {
                Word = _word,
                Translation = _translation
            };
        }



        public UserVocabularyWordBuilder WithWord(string word, string translation)
        {
            _word = word;
            _translation = translation;
            return this;
        }
    }
}
