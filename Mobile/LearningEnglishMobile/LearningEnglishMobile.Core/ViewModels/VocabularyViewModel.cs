using LearningEnglishMobile.Core.Extensions;
using LearningEnglishMobile.Core.Models.Vocabulary;
using LearningEnglishMobile.Core.Services.Vocabulary;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class VocabularyViewModel : ViewModelBase
    {

        private IVocabularyService _vocabularyService { get; set; }

        private ObservableCollection<UserVocabulary> _vocabularies;

        public ObservableCollection<UserVocabulary> Vocabularies { 
            get => _vocabularies; 
            set {
                _vocabularies = value;
                RaisePropertyChanged(() => Vocabularies);
            } 
        }

        public ICommand SelectVocabularyCommand { get; }


       
        public VocabularyViewModel(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
            SelectVocabularyCommand = new Command<UserVocabulary>(async vocabulary => await SelectVocabulary(vocabulary));
        }

        public override async Task InitializeAsync(object navigationData)
        {
            Vocabularies = (await _vocabularyService.GetVocabularies()).ToObservableCollection();
            await base.InitializeAsync(navigationData);
        }

        private async Task SelectVocabulary(UserVocabulary vocabulary)
        {
            await NavigationService.NavigateToAsync<UserWordsViewModel>(vocabulary);
        }
    }
}
