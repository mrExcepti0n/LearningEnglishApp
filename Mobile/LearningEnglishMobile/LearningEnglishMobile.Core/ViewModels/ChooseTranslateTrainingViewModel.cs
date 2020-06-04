using Data.Core;
using LearningEnglishMobile.Core.Extensions;
using LearningEnglishMobile.Core.Models.Training.ChooseTranslate;
using LearningEnglishMobile.Core.Models.Training.Results;
using LearningEnglishMobile.Core.Models.Training.Shared;
using LearningEnglishMobile.Core.Services.Training;
using LearningEnglishMobile.Core.Services.WordImage;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IWordImageService _wordImageService;

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


        private ImageSource _myImageSource;
        public ImageSource MyImageSource { get => _myImageSource; set { _myImageSource = value; RaisePropertyChanged(() => MyImageSource); } }

        private ChooseTranslateTraining _training;

        public ChooseTranslateTrainingViewModel(ITrainingService trainingService, IWordImageService wordImageService)
        {
            _trainingService = trainingService;
            _wordImageService = wordImageService;

            CheckAnswerCommand = new Command<string>((answer) => CheckAnswer(answer));
            SkipAnswerCommand = new Command(() => SkipAnswer());
            NextAnswerCommand = new Command(async () => await NextQuestion());
            PlayWordCommand = new Command(async () => await PlayWord());
        }

        private async Task PlayWord()
        {
            await CrossTextToSpeech.Current.Speak(_currentQuestion.Word);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            ShowRightAnswer = false;

            var questionsDto = await _trainingService.GetChooseTranslateQuestions(false);

            var questions = questionsDto.Select(q => new QuestionWithOptions(q)).ToList();
            _training = new ChooseTranslateTraining(questions);

            CurrentQuestion = _training.GetCurrentQuestion();

            await LoadWordImage();
            await PlayWord();
            await base.InitializeAsync(navigationData);
        }


        private async Task LoadWordImage()
        {
            var stream = await _wordImageService.GetWordImageStream(CurrentQuestion.Word);
            MyImageSource = ImageSource.FromStream(() => stream);
        }

        public ICommand CheckAnswerCommand { get; }
        public ICommand SkipAnswerCommand { get; set; }
        public ICommand NextAnswerCommand { get; set; }
        public ICommand PlayWordCommand { get; set; }


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

                await LoadWordImage();

                await PlayWord();
            }
            else
            {
                await NavigationService.NavigateToAsync<TrainingResultViewModel>(_training.GetSummarizing());
                await NavigationService.RemoveLastFromBackStackAsync();
            }
        }

    }
}
