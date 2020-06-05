using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Services.Dtos;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Areas.Training.Models.Shared
{
    public abstract class TrainingBase<TQ> : ITraining<TQ> where TQ: Question
    {
        protected TrainingBase(IEnumerable<TQ> questions, TrainingTypeEnum trainingType, bool isReverse = false)
        {
            TrainingType = trainingType;
            IsReverse = isReverse;

            Id = Guid.NewGuid();
            Questions = questions.ToArray();

            CurrentQuestionNumber = 0;
            RightAnsweredQuestions = 0;
        }


        public bool IsReverse { get; }

        public Guid Id { get;  }

        public int QuestionsCount => Questions.Length;

        public int CurrentQuestionNumber { get; private set; }

        public int RightAnsweredQuestions { get; private set; }

        protected TrainingTypeEnum TrainingType { get; }


        [JsonProperty]
        protected TQ[] Questions { get; }


   

        public TrainingResultDto GetResults()
        {
            return new TrainingResultDto
            {
                TrainingType = TrainingType,
                IsReverseTraining = IsReverse,
                TrainingWordResults = Questions.Select(q => new TrainingWordResultDto { UserWordId = q.UserWordId, IsRightAnswer = q.IsRightAnswer, UserAnswer = q.UserAnswer }).ToList()
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

        public async Task<string> GetCurrentWordImageSrc(IWordImageService wordImageService)
        {
            var question = GetCurrentQuestion();

            var word = IsReverse ? question.Translation : question.Word;
            return await wordImageService.GetImageSrc(word);
        }


        public bool CheckAnswer(string answer)
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
