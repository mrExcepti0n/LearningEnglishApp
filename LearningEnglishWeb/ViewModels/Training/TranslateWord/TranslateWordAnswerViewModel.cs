﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training.TranslateWord
{
    public class TranslateWordAnswerViewModel
    {
        public string Word { get; set; }


        public string UserTranslation { get; set; }

        public string RightTranslation { get; set; }


        public bool IsCorrectAnswer => UserTranslation.ToLower().Trim() == RightTranslation.ToLower().Trim();
    }
}