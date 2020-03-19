using LearningEnglishWeb.Models.Training.Shared;

namespace LearningEnglishWeb.Models.Training.CollectWord
{
    public class CollectWordQuestion : Question
    {
        public char[] AnswerLetters { get; set; }
        public char[] UserLetters { get; set; }
        
        public override bool CheckAnswer(string userAnswer)
        {
            UserLetters = userAnswer.ToCharArray();
            return base.CheckAnswer(userAnswer);
        }
    }
}
