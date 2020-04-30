using LearningEnglishMobile.Core.Extensions;
using LearningEnglishMobile.Core.Services.Training.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Models.Training.Shared
{
    public class QuestionWithOptions : Question
    {
        [JsonConstructor]
        protected QuestionWithOptions()
        {
        }

        public QuestionWithOptions(int number, TrainingWord word, IEnumerable<string> options) : base(number, word)
        {
            Options = options.Select(opt => new QuestionOption { Option = opt }).ToObservableCollection();
        }

        public QuestionWithOptions(QuestionWithOptionsDto question) : base(question)
        {
            Options = question.Options.Select(opt => new QuestionOption { Option = opt }).ToObservableCollection();
        }

        public ObservableCollection<QuestionOption> Options { get; set; }

        public override bool CheckAnswer(string userAnswer)
        {
            var isRight = base.CheckAnswer(userAnswer);

            if (isRight)
            {
                RightOption.AnswerState = ChooseTranslate.AnswerStateEnum.Right;
            } else if (userAnswer == null)
            {
                RightOption.AnswerState = ChooseTranslate.AnswerStateEnum.RightSkipped;
            } else
            {
                RightOption.AnswerState = ChooseTranslate.AnswerStateEnum.Right;
                Options.Single(opt => opt.Option == userAnswer).AnswerState = ChooseTranslate.AnswerStateEnum.Wrong;
            }
            return isRight;
        }


        private QuestionOption RightOption => Options.Single(opt => opt.Option == Translation);

    }
}
