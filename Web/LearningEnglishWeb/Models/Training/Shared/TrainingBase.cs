using Data.Core;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.Shared
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
