using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Services.Dtos;
using LearningEnglishWeb.Models;

namespace LearningEnglishWeb.Areas.Training.Services
{
    public interface ITrainingService
    {
        //Task<List<UserWord>> GetRequiringStudyWords(TrainingTypeEnum trainingType, bool isReverseTraining);
        Task SaveTrainingResult(TrainingResultDto training);
        Task<Dictionary<int, byte>> GetTrainingWordsRatio(List<int> userWordIds);

        Task<Dictionary<(TrainingTypeEnum, bool), int>> GetAvailableTrainingWordsCount();

        Task<IEnumerable<T>> GetTrainingQuestions<T>(TrainingTypeEnum trainingType, bool isReverseTraining, IEnumerable<int> selectedUserWords);
    }
}
