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

        public int UserWordId { get; protected set; }

        public int Number { get; protected set; }

        public string Word { get; protected set; }

        public string Translation { get; protected set; }

        public string UserAnswer { get; protected set; }


        public bool CheckAnswer(string userAnswer)
        {
            UserAnswer = userAnswer;
            return IsRightAnswer;

        }


        public bool IsRightAnswer => Translation == UserAnswer;

    }
}
