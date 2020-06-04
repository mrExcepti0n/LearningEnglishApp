using VocabularyApi.Models;

namespace VocabularyApi.Dtos.Training
{
    public class QuestionDto
    {
        public QuestionDto(UserVocabularyWord userWord, int number)
        {
            UserWordId = userWord.Id;
            Word = userWord.Word;
            Translation = userWord.Translation;

            Number = number;
        }

        public int UserWordId { get; set; }

        public int Number { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }
    }
}
