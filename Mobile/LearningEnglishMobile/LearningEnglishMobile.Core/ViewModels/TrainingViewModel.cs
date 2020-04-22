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
    public class TrainingViewModel : ExtendedBindableObject    
    {
        public ICommand GetCollectWordTrainingCommand => new Command(async () => await GetCollectWordTrainingAsync());

        public ICommand GetTranslateWordTrainingCommand => new Command(async () => await GetTranslateWordTrainingAsync());

        public ICommand GetChooseTranslateTrainigCommand => new Command(async () => await GetChooseTranslateTrainigAsync());

        public ICommand GetAuditionTrainigCommand => new Command(async () => await GetAuditionTrainigCommandAsync());


        public INavigation Navigation { get; internal set; }

        public TrainingViewModel()
        {

        }

        private async Task GetCollectWordTrainingAsync()
        {
            await Navigation.PushAsync(new CollectWordTrainingView());
        }

        private async Task GetTranslateWordTrainingAsync()
        {
            await Navigation.PushAsync(new TranslateWordTrainingView());
        }

        private async Task GetChooseTranslateTrainigAsync()
        {
            await Navigation.PushAsync(new ChooseTranslateTrainigView());
        }

        private async Task GetAuditionTrainigCommandAsync()
        {
            await Navigation.PushAsync(new AuditionTrainigView());
        }
    }
}
