using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public abstract class TrainingBase<TQ> where TQ: QuestionBase
    {
        public Guid Id { get; set; }

        public int QuestionsCount { get; set; }

        public int CurrentQuestionNumber { get; set; }

        public int RightAnsweredQuestions { get; set; }


        protected IEnumerable<TQ> Questions { get; set; }

        private IEnumerator<TQ> QuestionEnumerator { get; }


        public TrainingBase(IEnumerable<TQ> questions)
        {
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



        public void Reset()
        {
            CurrentQuestionNumber = 0;
            RightAnsweredQuestions = 0;
            QuestionEnumerator.Reset();
        }
    }
}
