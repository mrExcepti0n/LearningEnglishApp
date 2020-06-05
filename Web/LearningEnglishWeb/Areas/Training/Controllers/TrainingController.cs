using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Areas.Training.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;
        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        public async Task<IActionResult> Index(IEnumerable<int> userSelectedWords = null)
        {
            var viewModel = await GetTrainingViewModel(userSelectedWords);
            return View(viewModel);
        }


        private async Task<ChooseTrainingViewModel> GetTrainingViewModel(IEnumerable<int> userSelectedWords = null)
        {
            if (userSelectedWords != null && userSelectedWords.Any())
            {
                return new ChooseTrainingViewModel(userSelectedWords);
            }

            var availableTrainingWords = await _trainingService.GetAvailableTrainingWordsCount();
            return new ChooseTrainingViewModel(availableTrainingWords);
        }

    }
}
