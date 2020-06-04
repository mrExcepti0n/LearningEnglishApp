using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Abstractions
{
    public interface ITrainingService
    {
        Task<List<UserWord>> GetRequiringStudyWords(TrainingTypeEnum trainingType, bool isReverseTraining);
        Task SaveTrainingResult(TrainingResultDto training);
        Task<Dictionary<int, byte>> GetTrainingWordsRatio(List<int> userWordIds);

        Task<Dictionary<(TrainingTypeEnum, bool), int>> GetAvailibleTrainingWordsCount();
        Task<List<UserWord>> GetTrainingWords(IEnumerable<int> userSelectedWords);
    }
}
