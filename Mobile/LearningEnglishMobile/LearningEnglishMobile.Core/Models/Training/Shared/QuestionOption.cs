using LearningEnglishMobile.Core.Models.Training.ChooseTranslate;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.Models.Training.Shared
{
    public class QuestionOption : BindableObject
    {
        public string Option { get; set; }

        public AnswerStateEnum? _answerState { get; set; }
        public AnswerStateEnum? AnswerState
        {
            get { return _answerState; }
            set
            {
                _answerState = value;
                OnPropertyChanged("AnswerState");
            }
        }
    }
}
