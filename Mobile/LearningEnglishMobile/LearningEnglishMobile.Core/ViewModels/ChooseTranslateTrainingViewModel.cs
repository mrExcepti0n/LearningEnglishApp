using Data.Core;
using LearningEnglishMobile.Core.Extensions;
using LearningEnglishMobile.Core.Models.Training.ChooseTranslate;
using LearningEnglishMobile.Core.Models.Training.Results;
using LearningEnglishMobile.Core.Models.Training.Shared;
using LearningEnglishMobile.Core.Services.Training;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class ChooseTranslateTrainingViewModel  : ViewModelBase
    {

        private QuestionWithOptions _currentQuestion;
        private ITrainingService _trainingService;

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

        public ChooseTranslateTrainingViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        

            CheckAnswerCommand = new Command<string>((answer) => CheckAnswer(answer));
            SkipAnswerCommand = new Command(() => SkipAnswer());
            NextAnswerCommand = new Command(async () => await NextQuestion());
        }

        public override async Task InitializeAsync(object navigationData)
        {
            ShowRightAnswer = false;

            var questionsDto = await _trainingService.GetChooseTranslateQuestions(false);

            var questions = questionsDto.Select(q => new QuestionWithOptions(q)).ToList();
            _training = new ChooseTranslateTraining(questions);

            CurrentQuestion = _training.GetCurrentQuestion();

   
            await base.InitializeAsync(navigationData);
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

        private async Task NextQuestion()
        {
            var question = _training.GetNextQuestion();

            if (question != null)
            {
                CurrentQuestion = question;
                ShowRightAnswer = false;
            }
            else
            {
                await NavigationService.NavigateToAsync<TrainingResultViewModel>(_training.GetSummarizing());
                await NavigationService.RemoveLastFromBackStackAsync();
            }
        }

    }
}
