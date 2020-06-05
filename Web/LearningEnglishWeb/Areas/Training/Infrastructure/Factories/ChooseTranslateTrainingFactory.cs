using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Infrastructure.Factories.Abstractions;
using LearningEnglishWeb.Areas.Training.Models;
using LearningEnglishWeb.Areas.Training.Models.ChooseTranslate;
using LearningEnglishWeb.Areas.Training.Models.Shared;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.Services.Dtos;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Factories
{
    public class ChooseTranslateTrainingFactory : TrainingFactoryBase<ChooseTranslateTraining>
    {

        public ChooseTranslateTrainingFactory(ITrainingService trainingService, TrainingSettings trainingSettings)
            :base(trainingService, TrainingTypeEnum.ChooseTranslate, trainingSettings)
        {
        }

        public override async Task<ChooseTranslateTraining> GetTraining()
        {
            IEnumerable<QuestionWithOptionsDto> questionsDto = await GetQuestions<QuestionWithOptionsDto>();

            var questions = questionsDto.Select(q => new QuestionWithOptions(q.Number, q.ToUserWord(), q.Options));
            return new ChooseTranslateTraining(questions, TrainingSettings.IsReverseWay);
        }
    }
}
