namespace LearningEnglishWeb.Areas.Training.Services.Dtos
{
    public class TrainingWordResultDto
    {
        public int UserWordId { get; set; }

        public bool IsRightAnswer { get; set; }

        public string UserAnswer { get; set; }
    }
}
