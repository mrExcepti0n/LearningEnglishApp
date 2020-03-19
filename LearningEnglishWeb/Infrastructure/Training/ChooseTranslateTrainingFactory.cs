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
            Word[] words = await GetWords();
            return new ChooseTranslateTraining(GetQuestions(words), _reverseWay);
        }
        


        private IEnumerable<QuestionWithOptions> GetQuestions(Word[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                var answers = GetAnswers(words, i, 4);
                yield return GetQuestion(i, words[i], answers);
            }
        }
        
        private QuestionWithOptions GetQuestion(int number, Word word, List<Answer> answers)
        {
            return new QuestionWithOptions
            {
                Number = number + 1,
                Word = word.Name,
                Translation = word.Translation,
                Options = answers
            };
        }
     

        private List<Answer> GetAnswers(Word[] words, int currentWordIndex, int size)
        {            

            var otherWords = words.Where((w,i) => i != currentWordIndex).ToArray();
            ShuffleWords(otherWords);

            var answers = otherWords.Take(size).Select(ow => new Answer { Option = ow.Translation }).ToList();
            answers.Add(new Answer { Option = words[currentWordIndex].Translation });

            var res = answers.ToArray();
            ShuffleWords(res);

            return res.ToList();
        }

      

    }
}
