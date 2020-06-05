using LearningEnglishWeb.Models;

namespace LearningEnglishWeb.Areas.Training.Services.Dtos
{
    public class QuestionDto
    {
        public int UserWordId { get; protected set; }

        public int Number { get; protected set; }

        public string Word { get; protected set; }

        public string Translation { get; protected set; }


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
