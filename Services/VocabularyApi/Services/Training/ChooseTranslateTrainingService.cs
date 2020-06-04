using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Models;

namespace VocabularyApi.Services.Training
{
    public class ChooseTranslateTrainingService : TrainingServiceBase<QuestionWithOptionsDto>
    {
        public IEnumerable<QuestionWithOptionsDto> GetQuestions(UserVocabularyWord[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                var answers = GetOptions(words, i, 4);
                yield return GetQuestion(i, words[i], answers);
            }
        }

        private QuestionWithOptionsDto GetQuestion(int number, UserVocabularyWord word, List<string> options)
        {
            return new QuestionWithOptionsDto(word, number + 1,  options);
        }


        private List<string> GetOptions(UserVocabularyWord[] words, int currentWordIndex, int size)
        {

            var otherWords = words.Where((w, i) => i != currentWordIndex).ToArray();
            ShuffleWords(otherWords);

            var answers = otherWords.Take(size).Select(ow => ow.Translation).ToList();
            answers.Add(words[currentWordIndex].Translation);
            var res = answers.ToArray();
            ShuffleWords(res);
            return res.ToList();
        }
    }
}
