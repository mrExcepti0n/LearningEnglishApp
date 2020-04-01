using Newtonsoft.Json;

namespace LearningEnglishWeb.Models.Training.Shared
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

        public int UserWordId { get; set; }

        public int Number { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }

        public string UserAnswer { get; set; }


        public bool CheckAnswer(string userAnswer)
        {
            UserAnswer = userAnswer;
            return IsRightAnswer;

        }


        public bool IsRightAnswer => Translation == UserAnswer;

    }
}
