using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class TrainingViewModel : ViewModelBase    
    {
        public ICommand GetCollectWordTrainingCommand => new Command(async () => await GetCollectWordTrainingAsync());

        public ICommand GetTranslateWordTrainingCommand => new Command(async () => await GetTranslateWordTrainingAsync());

        public ICommand GetChooseTranslateTrainigCommand => new Command(async () => await GetChooseTranslateTrainigAsync());

        public ICommand GetAuditionTrainigCommand => new Command(async () => await GetAuditionTrainigCommandAsync());


        public TrainingViewModel()
        {

        }

        private async Task GetCollectWordTrainingAsync()
        {
            await NavigationService.NavigateToAsync<CollectWordTrainingViewModel>();
        }

        private async Task GetTranslateWordTrainingAsync()
        {
            throw new NotImplementedException();
        }

        private async Task GetChooseTranslateTrainigAsync()
        {
            await NavigationService.NavigateToAsync<ChooseTranslateTrainingViewModel>();
        }

        private async Task GetAuditionTrainigCommandAsync()
        {
            throw new NotImplementedException();
        }
    }
}
