using LearningEnglishWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public abstract class TrainingBase<TQ> where TQ: QuestionBase
    {

        public bool IsReverse { get; }
        private IWordImageService _wordImageService;

        public Guid Id { get; set; }

        public int QuestionsCount { get; set; }

        public int CurrentQuestionNumber { get; set; }

        public int RightAnsweredQuestions { get; set; }


        protected IEnumerable<TQ> Questions { get; set; }

        private IEnumerator<TQ> QuestionEnumerator { get; }


        public TrainingBase(IWordImageService wordImageService, IEnumerable<TQ> questions, bool isReverse = false)
        {
            IsReverse = isReverse;
            _wordImageService = wordImageService;

            Id = Guid.NewGuid();
            QuestionsCount = questions.Count();
            Questions = questions;

            CurrentQuestionNumber = 0;
            RightAnsweredQuestions = 0;

            QuestionEnumerator = Questions.GetEnumerator();
        }


        public TQ GetNextQuestion()
        {
            if (QuestionEnumerator.MoveNext())
            {
                CurrentQuestionNumber++;
                return GetCurrentQuestion();          
            }

            return null;
        }


        public TQ GetCurrentQuestion()
        {
            return QuestionEnumerator.Current;
        }

        public async Task<string> GetCurrentWordImageSrc()
        {
            var question = GetCurrentQuestion();

            var word = IsReverse ? question.Translation : question.Word;
            return await _wordImageService.GetImageSrc(word);
        }   
    }
}
