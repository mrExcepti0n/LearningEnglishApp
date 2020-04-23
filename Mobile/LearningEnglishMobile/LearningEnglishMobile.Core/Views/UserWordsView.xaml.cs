using LearningEnglishMobile.Core.Models.Vocabulary;
using LearningEnglishMobile.Core.Services.Vocabulary;
using LearningEnglishMobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningEnglishMobile.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserWordsView : ContentPage
    {
        public UserWordsView(UserVocabulary userVocabulary)
        {
            InitializeComponent();

            BindingContext = new UserWodsViewModel(userVocabulary, new MockedVocabularyService(userVocabulary.WordsCount));
        }
    }
}