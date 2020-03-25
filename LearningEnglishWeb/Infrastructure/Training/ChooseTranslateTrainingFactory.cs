using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    public class ChooseTranslateTrainingFactory : TrainingFactoryBase<ChooseTranslateTraining>
    {

        public ChooseTranslateTrainingFactory(IVocabularyService vocabularyService,  LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay)
            :base(vocabularyService, fromLanguage, toLanguage, reverseWay)
        {
          
           
        }

        public override async Task<ChooseTranslateTraining> GetTraining()
        {
            UserWord[] words = await GetWords();
            return new ChooseTranslateTraining(GetQuestions(words), _reverseWay);
        }
        


        private IEnumerable<QuestionWithOptions> GetQuestions(UserWord[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                var answers = GetOptions(words, i, 4);
                yield return GetQuestion(i, words[i], answers);
            }
        }
        
        private QuestionWithOptions GetQuestion(int number, UserWord word, List<string> options)
        {
            return new QuestionWithOptions
            {
                Number = number + 1,
                Word = word.Name,
                Translation = word.Translation,
                Options = options
            };
        }
     

        private List<string> GetOptions(UserWord[] words, int currentWordIndex, int size)
        {            

            var otherWords = words.Where((w,i) => i != currentWordIndex).ToArray();
            ShuffleWords(otherWords);

            var answers = otherWords.Take(size).Select(ow => ow.Translation).ToList();
            answers.Add(words[currentWordIndex].Translation);
            var res = answers.ToArray();
            ShuffleWords(res);
            return res.ToList();
        }

      

    }
}
