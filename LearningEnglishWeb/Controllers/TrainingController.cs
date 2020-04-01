using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Training;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers
{
    public class TrainingController : Controller
    {
        private ITrainingService _trainingService;
        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        public async Task<IActionResult> Index()
        {
            var availibleTrainingWords = await _trainingService.GetAvailibleTrainingWordsCount();
            var viewModel = new ChooseTrainingViewModel(availibleTrainingWords);
            return View(viewModel);
        }

    }
}
