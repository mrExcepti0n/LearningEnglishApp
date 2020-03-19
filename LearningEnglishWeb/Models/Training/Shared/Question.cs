namespace LearningEnglishWeb.Models.Training.Shared
{
    public class Question
    {
        public int Number { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }


        public virtual bool CheckAnswer(string userAnswer)
        {
            return Translation == userAnswer;
        } 
    }
}
