﻿using LearningEnglishWeb.Areas.Training.Models.Shared;
using LearningEnglishWeb.Models;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Areas.Training.Models.CollectWord
{
    public class CollectWordQuestion : Question
    {

        [JsonConstructor]
        protected CollectWordQuestion()
        {
        }
        public CollectWordQuestion(int number, UserWord userWord, char[] answerLetters) : base(number, userWord)
        {
            AnswerLetters = answerLetters;
        }

        [JsonProperty]
        public char[] AnswerLetters { get; protected set; }

        public char[] UserLetters => UserAnswer?.ToCharArray();

    
    }
}
