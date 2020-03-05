﻿using AngleSharp;
using LearningEnglishWeb.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class ChooseTranslateQuestionModel
    {
        public Guid TrainingId { get; set; }
        public string Word { get; set; }

        public List<string> Translations { get; set; }

        public string QuestionNumber { get; set; }

        public string ImageSrc { get; set; }

        public bool IsReverse { get; set; }

        public ChooseTranslateQuestionModel()
        {

        }

        public ChooseTranslateQuestionModel(ChooseTranslateTraining training, ChooseTranslateQuestion question, string image)
        {
            TrainingId = training.Id;
            IsReverse = training.IsReverse;
            Word = question.Word;
            Translations = question.TranslationAnswers.Select(ta => ta.Translation).ToList();
            QuestionNumber = question.Number.ToString();

            ImageSrc = image;
        }



    }
}
