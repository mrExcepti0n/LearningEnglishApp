using LearningEnglishWeb.Models.Training.Shared;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Models.Training.CollectWord
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

        public char[] AnswerLetters { get; set; }

        public char[] UserLetters => UserAnswer?.ToCharArray();

    
    }
}
