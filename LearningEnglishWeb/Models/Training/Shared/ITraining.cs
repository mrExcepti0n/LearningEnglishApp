using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.Shared
{
    public interface ITraining<out TQ> where TQ: Question
    {

        bool IsReverse { get; }

        Guid Id { get; set; }

        int QuestionsCount { get; }

        int CurrentQuestionNumber { get; set; }

        int RightAnsweredQuestions { get; set; }


        TQ GetNextQuestion();

        TQ GetCurrentQuestion();

        Task<string> GetCurrentWordImageSrc(IWordImageService wordImageService);

        bool CheckAnswer(string answer);

        TrainingResultDto GetResults();
    }
}
