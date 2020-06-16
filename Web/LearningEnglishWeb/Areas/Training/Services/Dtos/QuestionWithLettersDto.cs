namespace LearningEnglishWeb.Areas.Training.Services.Dtos
{
    public class QuestionWithLettersDto : QuestionDto
    {
        public char[] AnswerLetters { get; set; }
    }
}
