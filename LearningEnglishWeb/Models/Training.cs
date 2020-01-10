using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class Training
    {
        public Guid Id { get; set; }

        public int QuestionsCount { get; set; }

        public int CurrentQuestionNumber { get; set; }

        public int RightAnsweredQuestions { get; set; }


        public List<Question> Questions { get; set; }


        public Training(List<Question> questions)
        {
            Id = Guid.NewGuid();
            QuestionsCount = questions.Count;
            Questions = questions;

            CurrentQuestionNumber = 0;
            RightAnsweredQuestions = 0;
        }


        public Question GetNextQuestion()
        {
            if (CurrentQuestionNumber +1  < QuestionsCount)
            {
                return Questions[++CurrentQuestionNumber];
            }

            return null;
        }

        public QuestionResult CheckAnswer(string answer)
        {
            var question = Questions[CurrentQuestionNumber];

            var questionResult = new QuestionResult
            {
                Word = question.Word
            };

            if (question.RightAnswer == answer)
            {
                RightAnsweredQuestions++;

                questionResult.Answers = new List<AnswerResult>
                {
                    new AnswerResult {Word = question.RightAnswer, Right = true, UserSelect = true}
                };


            }
            else
            {
                questionResult.Answers = new List<AnswerResult>
                {
                    new AnswerResult {Word = question.RightAnswer, Right = true, UserSelect = false},
                    new AnswerResult {Word = answer, Right = false, UserSelect = true}
                };
            }
            return questionResult;

        }


        public void Reset()
        {
            CurrentQuestionNumber = 0;
            RightAnsweredQuestions = 0;
        }
    }
}
