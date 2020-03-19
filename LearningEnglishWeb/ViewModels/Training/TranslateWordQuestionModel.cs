﻿using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class TranslateWordQuestionModel
    {
        public TranslateWordQuestionModel()
        {

        }

        public TranslateWordQuestionModel(Guid trainingId, Question question)
        {
            TrainingId = trainingId;
            Word = question.Word;
            QuestionNumber = question.Number.ToString();
        }

        public Guid TrainingId { get; set; }

        public string Word { get; set; }

        public string QuestionNumber { get; set; }
      
    }
}
