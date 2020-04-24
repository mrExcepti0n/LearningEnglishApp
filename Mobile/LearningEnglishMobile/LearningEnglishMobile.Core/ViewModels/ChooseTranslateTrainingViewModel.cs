using LearningEnglishMobile.Core.Extensions;
using LearningEnglishMobile.Core.Models.Training.ChooseTranslate;
using LearningEnglishMobile.Core.Models.Training.Results;
using LearningEnglishMobile.Core.Models.Training.Shared;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class ChooseTranslateTrainingViewModel  : ExtendedBindableObject
    {

        private QuestionWithOptions _currentQuestion;

        public QuestionWithOptions CurrentQuestion { 
            get => _currentQuestion; 
            set { _currentQuestion = value;
                RaisePropertyChanged(() => CurrentQuestion);
            } 
        }



        private bool _showRightAnswer;
        public bool ShowRightAnswer { get => _showRightAnswer;
            set {
                _showRightAnswer = value; 
                RaisePropertyChanged(() => ShowRightAnswer); 
            } 
        }

        private ChooseTranslateTraining _training;
        private INavigation _navigation;

        public ChooseTranslateTrainingViewModel(INavigation navigation)
        {
            _navigation = navigation;

            var questions = new List<QuestionWithOptions> {
                new QuestionWithOptions(1, new TrainingWord {Id = 1, Word = "Fox", Translation = "Лиса"}, new List<string>{ "Собака", "Заяц", "Лиса", "Кошка", "Волк"}),
                new QuestionWithOptions(1, new TrainingWord {Id = 2, Word = "Hare", Translation = "Заяц"}, new List<string>{ "Заяц", "Лиса", "Собака", "Кошка", "Волк"})

            };
            _training = new ChooseTranslateTraining(questions);

            CurrentQuestion = _training.GetCurrentQuestion();

            ShowRightAnswer = false;

            CheckAnswerCommand = new Command<string>((answer) => CheckAnswer(answer));
            SkipAnswerCommand = new Command(() => SkipAnswer());
            NextAnswerCommand = new Command(() => NextQuestion());
        }


        public ICommand CheckAnswerCommand { get; }
        public ICommand SkipAnswerCommand { get; set; }
        public ICommand NextAnswerCommand { get; set; }

        private void CheckAnswer(string answer)
        {
           var isRight = _training.CheckAnswer(answer);
            ShowRightAnswer = true;
        }

        private void SkipAnswer()
        {
            _training.CheckAnswer(null);
            ShowRightAnswer = true;
        }

        private void NextQuestion()
        {
            var question = _training.GetNextQuestion();

            if (question != null)
            {
                CurrentQuestion = question;
                ShowRightAnswer = false;
            }
            else
            {
                _navigation.PushAsync(new TrainingResultView(_training.GetSummarizing()));
                _navigation.RemovePrevPageFromStack();
            }
        }

    }
}
