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
            Vocabularies = new ObservableCollection<UserVocabulary>(vocabularyService.GetVocabularies());
            SelectVocabularyCommand = new Command<UserVocabulary>(async vocabulary => await SelectVocabulary(vocabulary));
        }

        private async Task SelectVocabulary(UserVocabulary vocabulary)
        {
            await NavigationService.NavigateToAsync<UserWordsViewModel>(vocabulary);
        }
    }
}
