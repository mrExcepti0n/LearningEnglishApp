using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.Shared
{
    public abstract class TrainingBase<TQ> where TQ: Question
    {

        public bool IsReverse { get; }

        public Guid Id { get; set; }

        public int QuestionsCount => Questions.Length;

        public int CurrentQuestionNumber { get; set; }

        public int RightAnsweredQuestions { get; set; }


        [JsonProperty]
        protected TQ[] Questions { get; set; }


        public TrainingBase(IEnumerable<TQ> questions, bool isReverse = false)
        {
            IsReverse = isReverse;

            Id = Guid.NewGuid();
            Questions = questions.ToArray();

            CurrentQuestionNumber = 0;
            RightAnsweredQuestions = 0;
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
