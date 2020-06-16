using LearningEnglishWeb.Models;

namespace LearningEnglishWeb.Areas.Training.Services.Dtos
{
    public class QuestionDto
    {
        public int UserWordId { get; set; }

        public int Number { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }


        public UserWord ToUserWord()
        {
            return new UserWord
            {
                Id = UserWordId,
                Word = Word,
                Translation = Translation
            };
        }
    }
}
