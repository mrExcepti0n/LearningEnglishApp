using Data.Core.Extensions;
using LearningEnglishMobile.Core.Models.Vocabulary;
using LearningEnglishMobile.Core.Services.Vocabulary;
using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class UserWodsViewModel : ExtendedBindableObject
    {
        public UserVocabulary SelectedVocabulary { get; }

        public ObservableCollection<UserWord> UserWords { get; set; }

        private List<UserWord> _userWords { get; set; }

        public UserWodsViewModel(UserVocabulary selectedVocabulary, IVocabularyService vocabularyService)
        {
            SelectedVocabulary = selectedVocabulary;
            _userWords = vocabularyService.GetUserWords(selectedVocabulary.Id).ToList();
            UserWords = new ObservableCollection<UserWord>(_userWords);
        }


        public ICommand SelectWordCommand => new Command<UserWord>(uw => SelectWord(uw));

        public ICommand DeleteWordCommand => new Command<UserWord>(uw => DeleteWord(uw));

        public ICommand FilterWordsCommand => new Command<string>(s => FilterWords(s));

        private void FilterWords(string mask)
        {
            var words = mask == null ? _userWords : _userWords.Where(uw => uw.Word.Contains(mask, StringComparison.OrdinalIgnoreCase)).ToList();
            
            UserWords = new ObservableCollection<UserWord>(words);
            RaisePropertyChanged(() => UserWords);
        }



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
