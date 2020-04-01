using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    public class CollectWordTrainingFactory : TrainingFactoryBase<CollectWordTraining>
    {

        public CollectWordTrainingFactory(ITrainingService trainingService,  LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay) 
            : base(trainingService, TrainingTypeEnum.CollectWord, fromLanguage, toLanguage, reverseWay)
        {
            
        }

        public override async Task<CollectWordTraining> GetTraining()
        {
            UserWord[] words = await GetWords();

            return new CollectWordTraining(GetQuestions(words), _reverseWay);
        }  


        private IEnumerable<CollectWordQuestion> GetQuestions(UserWord[] words)
        {
            for (var i= 0; i< words.Length; i++)
            {
                var translation = words[i].Translation.ToLower();
                yield return new CollectWordQuestion(i + 1, words[i], ShuffleWords(translation.ToCharArray()));
            }
        }
    }
}
