using System.Collections.Generic;
using LearningEnglishWeb.Models;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Areas.Training.Models.Shared
{
    public class QuestionWithOptions : Question
    {
        [JsonConstructor]
        protected QuestionWithOptions()
        {
        }

        public QuestionWithOptions(int number, UserWord userWord, IEnumerable<string> options) : base(number, userWord)
        {
            Options = options;
        }

        [JsonProperty]
        public IEnumerable<string> Options { get; protected set; }

    }
}
