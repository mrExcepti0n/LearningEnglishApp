using LearningEnglishMobile.Core.Models.Vocabulary;
using LearningEnglishMobile.Core.Services.Vocabulary;
using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class UserWodsViewModel : ExtendedBindableObject
    {
        public UserVocabulary SelectedVocabulary { get; }

        public ObservableCollection<UserWord> UserWords { get; set; }


        public UserWodsViewModel(UserVocabulary selectedVocabulary, IVocabularyService vocabularyService)
        {
            SelectedVocabulary = selectedVocabulary;
            UserWords = new ObservableCollection<UserWord>(vocabularyService.GetUserWords(selectedVocabulary.Id));
        }


        public ICommand SelectWordCommand => new Command<UserWord>(uw => SelectWord(uw));

        public ICommand DeleteWordCommand => new Command<UserWord>(uw => DeleteWord(uw));

        private void DeleteWord(UserWord uw)
        {
            UserWords.Remove(uw);
        }

        private void SelectWord(UserWord uw)
        {
            uw.IsSelected = !uw.IsSelected;
        }

    }
}
