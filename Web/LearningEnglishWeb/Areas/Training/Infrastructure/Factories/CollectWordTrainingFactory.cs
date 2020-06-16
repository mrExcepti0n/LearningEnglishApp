using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Infrastructure.Factories.Abstractions;
using LearningEnglishWeb.Areas.Training.Models;
using LearningEnglishWeb.Areas.Training.Models.CollectWord;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.Services.Dtos;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Factories
{
    public class CollectWordTrainingFactory : TrainingFactoryBase<CollectWordTraining>
    {

        public CollectWordTrainingFactory(ITrainingService trainingService,  TrainingSettings trainingSettings) 
            : base(trainingService, TrainingTypeEnum.CollectWord, trainingSettings)
        {
            
        }

        public override async Task<CollectWordTraining> GetTraining()
        {
            IEnumerable<QuestionWithLettersDto> questionsDto = await GetQuestions<QuestionWithLettersDto>();

            var questions = questionsDto.Select(q => new CollectWordQuestion(q.Number, q.ToUserWord(), q.AnswerLetters));
            return new CollectWordTraining(questions, TrainingSettings.IsReverseWay);
        }  
    }
}
