using System.Collections.Generic;
using System.Linq;
using Data.Core;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services.Training.Abstractions;

namespace VocabularyApi.Services.Training
{
    public class ChooseTranslateTrainingService : TrainingServiceBase<QuestionWithOptionsDto>
    {
        public ChooseTranslateTrainingService(VocabularyContext context) : base(context, TrainingTypeEnum.ChooseTranslate)
        {
        }

        public override IEnumerable<QuestionWithOptionsDto> GetQuestions(IEnumerable<UserVocabularyWord> words)
        {
            var userVocabularyWords = words.ToList();

            for (var i = 0; i < userVocabularyWords.Count; i++)
            {
                var answers = GetOptions(userVocabularyWords, i, 4);
                yield return GetQuestion(i, userVocabularyWords[i], answers);
            }
        }

        private QuestionWithOptionsDto GetQuestion(int number, UserVocabularyWord word, List<string> options)
        {
            return new QuestionWithOptionsDto(word, number + 1,  options);
        }


        private List<string> GetOptions(IEnumerable<UserVocabularyWord> words, int currentWordIndex, int size)
        {
            var userVocabularyWords = words.ToList();

            var otherWords = userVocabularyWords.Where((w, i) => i != currentWordIndex).ToArray();
            ShuffleWords(otherWords);

            var answers = otherWords.Take(size).Select(ow => ow.Translation).ToList();
            answers.Add(userVocabularyWords[currentWordIndex].Translation);
            var res = answers.ToArray();
            ShuffleWords(res);
            return res.ToList();
        }

     
    }
}
