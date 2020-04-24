using Data.Core;
using LearningEnglishMobile.Core.Models.Training.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningEnglishMobile.Core.Models.Training.Shared
{
    public abstract class TrainingBase<TQ> : ITraining<TQ> where TQ: Question
    {

        public bool IsReverse { get; }

        public Guid Id { get; set; }

        public int QuestionsCount => Questions.Length;

        public int CurrentQuestionNumber { get; set; }

        public int RightAnsweredQuestions { get; set; }

        protected TrainingTypeEnum TrainingType { get; private set; }


        [JsonProperty]
        protected TQ[] Questions { get; set; }


        public TrainingBase(IEnumerable<TQ> questions, TrainingTypeEnum trainingType, bool isReverse = false)
        {
            TrainingType = trainingType;
            IsReverse = isReverse;

            Id = Guid.NewGuid();
            Questions = questions.ToArray();

            CurrentQuestionNumber = 0;
            RightAnsweredQuestions = 0;
        }

        public TrainingResults GetResults()
        {
            return new TrainingResults
            {
                TrainingType = TrainingType,
                IsReverseTraining = IsReverse,
                TrainingWordResults = Questions.Select(q => new TrainingWordResult { UserWordId = q.UserWordId, IsRightAnswer = q.IsRightAnswer, UserAnswer = q.UserAnswer }).ToList()
            };
        }

        internal TrainingSummarizing GetSummarizing()
        {
            return new TrainingSummarizing
            {
                RightQuestions = RightAnsweredQuestions,
                TotalQuestions = QuestionsCount
            };
        }


        public TQ GetNextQuestion()
        {
            CurrentQuestionNumber++;
            return GetCurrentQuestion();
            
        }


        public TQ GetCurrentQuestion()
        {
            return CurrentQuestionNumber < QuestionsCount ? Questions[CurrentQuestionNumber] : null;
        }      


        public virtual bool CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();
            var isRight = question.CheckAnswer(answer);
            if (isRight)
            {
                RightAnsweredQuestions++;
            }
            return isRight;
        }
    }
}
