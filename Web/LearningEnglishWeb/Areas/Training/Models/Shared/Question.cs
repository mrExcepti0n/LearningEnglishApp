using LearningEnglishWeb.Models;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Areas.Training.Models.Shared
{
    public class Question
    {
        [JsonConstructor]
        protected Question()
        {
        }

        public Question(int number, UserWord userWord)
        {
            Number = number;
            UserWordId = userWord.Id;
            Word = userWord.Word.ToLower();
            Translation = userWord.Translation.ToLower();
        }
        [JsonProperty]
        public int UserWordId { get; protected set; }
        [JsonProperty]
        public int Number { get; protected set; }
        [JsonProperty]
        public string Word { get; protected set; }
        [JsonProperty]
        public string Translation { get; protected set; }
        [JsonProperty]
        public string UserAnswer { get; protected set; }


        public bool CheckAnswer(string userAnswer)
        {
            UserAnswer = userAnswer;
            return IsRightAnswer;

        }


        public bool IsRightAnswer => Translation == UserAnswer;

    }
}
