using Data.Core;
using LearningEnglishMobile.Core.Models.Training;
using LearningEnglishMobile.Core.Services.Training.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.Training
{
    public interface ITrainingService
    {
        Task<List<UserWord>> GetRequiringStudyWords(TrainingTypeEnum trainingType, bool isReverseTraining);

        Task<List<QuestionWithOptionsDto>> GetChooseTranslateQuestions(bool isReverseTraining);
    }
}
