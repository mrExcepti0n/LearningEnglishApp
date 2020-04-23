using LearningEnglishMobile.Core.Models.Vocabulary;
using LearningEnglishMobile.Core.Services.Vocabulary;
using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class VocabularyViewModel : ExtendedBindableObject
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


        private INavigation _navigation;


        public VocabularyViewModel(IVocabularyService vocabularyService, INavigation navigation)
        {
            _navigation = navigation;
            Vocabularies = new ObservableCollection<UserVocabulary>(vocabularyService.GetVocabularies());
            SelectVocabularyCommand = new Command<UserVocabulary>(vocabulary => SelectVocabulary(vocabulary));
        }

        private void SelectVocabulary(UserVocabulary vocabulary)
        {
            _navigation.PushAsync(new UserWordsView(vocabulary));
        }
    }
}
