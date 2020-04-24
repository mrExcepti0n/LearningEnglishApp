using LearningEnglishMobile.Core.Models.Training.Results;
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
    public partial class TrainingResultView : ContentPage
    {
        public TrainingResultView(TrainingSummarizing summarizing)
        {
            InitializeComponent();
            BindingContext = new TrainingResultViewModel(summarizing);
        }
    }
}