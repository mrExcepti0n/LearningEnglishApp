﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class CollectWordQuestionResult
    {
        public List<CollectWordAnswerResult> CollectWordAnswerResults { get; set; }

        public string RigtAnswer { get; set; }

        public bool IsCorrectAnswer => CollectWordAnswerResults.All(cwa => cwa.IsRight);
    }
}
