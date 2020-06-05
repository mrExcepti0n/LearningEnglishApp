using System;
using System.Collections.Generic;
using VocabularyApi.IntegrationTests.Builders.Abstractions;
using VocabularyApi.Models;

namespace VocabularyApi.IntegrationTests.Builders
{
    public class UserVocabularyBuilder : EntityBuilder<UserVocabulary>
    {
        readonly UserVocabularyWordBuilder _wordBuilder = new UserVocabularyWordBuilder();

        private Guid _user = Guid.Empty;
        private List<UserVocabularyWord> _vocabularyWords = new List<UserVocabularyWord>();

        public override UserVocabulary Build()
        {
            var vocabulary = new UserVocabulary("Test Vocabulary", null, _user, true, _vocabularyWords.ToArray());
            return vocabulary;
        }

        public UserVocabularyBuilder WithStandardWords()
        {
            _vocabularyWords.Add(_wordBuilder.WithWord("dog", "собака"));
            _vocabularyWords.Add(_wordBuilder.WithWord("cat", "кошка"));
            _vocabularyWords.Add(_wordBuilder.WithWord("girl", "девочка"));

            return this;
        }

        public UserVocabularyBuilder WithUser(Guid userId)
        {
            _user = userId;
            return this;
        }
    }
}
