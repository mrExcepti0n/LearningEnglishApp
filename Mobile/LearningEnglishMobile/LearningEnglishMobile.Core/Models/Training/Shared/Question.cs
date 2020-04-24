using Newtonsoft.Json;

namespace LearningEnglishMobile.Core.Models.Training.Shared
{
    public class Question
    {
        [JsonConstructor]
        protected Question()
        {
        }

        public Question(int number, TrainingWord userWord)
        {
            Number = number;
            UserWordId = userWord.Id;
            Word = userWord.Word;
            Translation = userWord.Translation;
        }

        public int UserWordId { get; set; }

        public int Number { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }

        public string UserAnswer { get; set; }


        public virtual bool CheckAnswer(string userAnswer)
        {
            UserAnswer = userAnswer;
            return IsRightAnswer;

        }


        public bool IsRightAnswer => Translation == UserAnswer;

    }
}
