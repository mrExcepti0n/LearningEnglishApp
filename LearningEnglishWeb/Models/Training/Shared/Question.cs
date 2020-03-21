namespace LearningEnglishWeb.Models.Training.Shared
{
    public class Question
    {
        public int Number { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }

        public string UserAnswer { get; set; }


        public virtual bool CheckAnswer(string userAnswer)
        {
            UserAnswer = userAnswer;
            return Translation == userAnswer;
        } 
    }
}
