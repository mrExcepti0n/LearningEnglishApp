using LearningEnglishMobile.Core.Models.Training;
using LearningEnglishMobile.Core.Models.Training.Results;
using System;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Models.Training.Shared
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

        bool CheckAnswer(string answer);

        TrainingResults GetResults();
    }
}
