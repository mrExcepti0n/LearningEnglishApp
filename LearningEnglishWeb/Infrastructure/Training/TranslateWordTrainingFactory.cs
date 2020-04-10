﻿using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Models.Training.TranslateWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    internal class TranslateWordTrainingFactory : TrainingFactoryBase<TranslateWordTraining>
    {
        public TranslateWordTrainingFactory(ITrainingService trainingService, TrainingSettings trainingSettings) 
            : base(trainingService, TrainingTypeEnum.TranslateWord, trainingSettings)
        {

        }

        public override async Task<TranslateWordTraining> GetTraining()
        {
            UserWord[] words = await GetWords();

            return new TranslateWordTraining(GetQuestions(words).ToList(), _trainingSettings.IsReverseWay);
        }


        private IEnumerable<Question> GetQuestions(UserWord[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                yield return new Question(i + 1, words[i]);
            }
        }
    }
}