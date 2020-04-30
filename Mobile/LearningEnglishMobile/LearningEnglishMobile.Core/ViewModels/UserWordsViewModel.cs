using Data.Core.Extensions;
using LearningEnglishMobile.Core.Extensions;
using LearningEnglishMobile.Core.Models.Vocabulary;
using LearningEnglishMobile.Core.Services.Vocabulary;
using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class UserWordsViewModel : ViewModelBase
    {
        private IVocabularyService _vocabularyService { get; set; }

        private UserVocabulary _selectedVocabulary;
        private List<UserWord> _allWords;
        private ObservableCollection<UserWord> _userWords;

        public UserVocabulary SelectedVocabulary { get => _selectedVocabulary; private set { _selectedVocabulary = value; RaisePropertyChanged(() => SelectedVocabulary); } }

        public ObservableCollection<UserWord> UserWords { get => _userWords; set { _userWords = value; RaisePropertyChanged(() => UserWords); } }


        public UserWordsViewModel(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;

        }

        public override async Task InitializeAsync(object navigationData)
        {
            SelectedVocabulary = navigationData as UserVocabulary;
            _allWords = (await _vocabularyService.GetUserWords(SelectedVocabulary.Id)).ToList();
            UserWords = _allWords.ToObservableCollection();
            await base.InitializeAsync(navigationData);
        }


        public ICommand SelectWordCommand => new Command<UserWord>(uw => SelectWord(uw));

        public ICommand DeleteWordCommand => new Command<UserWord>(uw => DeleteWord(uw));

        public ICommand FilterWordsCommand => new Command<string>(s => FilterWords(s));

        private void FilterWords(string mask)
        {
            var words = mask == null ? _allWords : _allWords.Where(uw => uw.Word.Contains(mask, StringComparison.OrdinalIgnoreCase)).ToList();            
            UserWords = words.ToObservableCollection();
        }



        private void DeleteWord(UserWord uw)
        {
            _allWords.Remove(uw);
            UserWords.Remove(uw);
        }

        private void SelectWord(UserWord uw)
        {
            uw.IsSelected = !uw.IsSelected;
        }

    }
}
