using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Infrastructure.Factories.Abstractions;
using LearningEnglishWeb.Areas.Training.Models;
using LearningEnglishWeb.Areas.Training.Models.Shared;
using LearningEnglishWeb.Areas.Training.Models.TranslateWord;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.Services.Dtos;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Factories
{
    internal class TranslateWordTrainingFactory : TrainingFactoryBase<TranslateWordTraining>
    {
        public TranslateWordTrainingFactory(ITrainingService trainingService, TrainingSettings trainingSettings) 
            : base(trainingService, TrainingTypeEnum.TranslateWord, trainingSettings)
        {

        }

        public override async Task<TranslateWordTraining> GetTraining()
        {
            IEnumerable<QuestionDto> questionsDto = await GetQuestions<QuestionDto>();

            var questions = questionsDto.Select(q => new Question(q.Number, q.ToUserWord()));

            return new TranslateWordTraining(questions, TrainingSettings.IsReverseWay);
        }

    }
}