using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public class ChooseTranslateTrainingFactory : TrainingFactoryBase<ChooseTranslateTraining>
    {
        private Random _random;

        public ChooseTranslateTrainingFactory(IVocabularyService vocabularyService, IWordImageService wordImageService, LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay)
            :base(vocabularyService, wordImageService, fromLanguage, toLanguage, reverseWay)
        {
          
            _random = new Random();
        }

        public override async Task<ChooseTranslateTraining> GetTraining()
        {
            Word[] words = (await _vocabularyService.GetRequiringStudyWords()).ToArray();

            return new ChooseTranslateTraining(_wordImageService, GetQuestions(words), _reverseWay);
        }




        private IEnumerable<ChooseTranslateQuestion> GetQuestions(Word[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                var answers = GetAnswers(words, i, 4);

                yield return GetQuestion(i, words[i], answers);
            }
        }
        
        private ChooseTranslateQuestion GetQuestion(int number, Word word, List<ChooseTranslateAnswer> answers)
        {
            return new ChooseTranslateQuestion
            {
                Number = number + 1,
                Word = _reverseWay ?  word.Translation : word.Name,
                TranslationAnswers = answers
            };
        }

        private ChooseTranslateAnswer GetAnswer(Word word, bool isRight)
        {
            return new ChooseTranslateAnswer
            {
                IsRight = isRight,
                Translation = _reverseWay ? word.Name : word.Translation
            };
        }


        private List<ChooseTranslateAnswer> GetAnswers(Word[] words, int currentWordIndex, int size)
        {            

            var otherWords = words.Where((w,i) => i != currentWordIndex).ToArray();
            ShuffleWords(otherWords);
            var answers = otherWords.Take(size).Select(ow => GetAnswer(ow, false)).ToList();
            answers.Add(GetAnswer(words[currentWordIndex], true));

            var res = answers.ToArray();
            ShuffleWords(res);

            return res.ToList();
        }

        private void ShuffleWords(Object[] words)
        {
            for (int i = words.Length - 1; i >= 1; i--)
            {
                int j = _random.Next(i + 1);
                var temp = words[j];
                words[j] = words[i];
                words[i] = temp;
            }
        }

    }
}
