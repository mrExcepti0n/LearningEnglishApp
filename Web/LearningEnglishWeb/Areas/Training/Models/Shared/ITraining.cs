using System;
using System.Threading.Tasks;
using LearningEnglishWeb.Areas.Training.Services.Dtos;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;

namespace LearningEnglishWeb.Areas.Training.Models.Shared
{
    public interface ITraining<out TQ> where TQ: Question
    {

        bool IsReverse { get; }

        Guid Id { get; }

        int QuestionsCount { get; }

        int CurrentQuestionNumber { get; }

        int RightAnsweredQuestions { get; }


        TQ GetNextQuestion();

        TQ GetCurrentQuestion();

        Task<string> GetCurrentWordImageSrc(IWordImageService wordImageService);

        bool CheckAnswer(string answer);

        TrainingResultDto GetResults();
    }
}
