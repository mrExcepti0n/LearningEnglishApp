using LearningEnglishWeb.Models.Training.Shared;

namespace LearningEnglishWeb.Models.Training.CollectWord
{
    public class CollectWordQuestion : Question
    {
        public char[] AnswerLetters { get; set; }

        public char[] UserLetters => UserAnswer?.ToCharArray();

        public override bool CheckAnswer(string userAnswer)
        {
            return base.CheckAnswer(userAnswer);
        }
    }
}
