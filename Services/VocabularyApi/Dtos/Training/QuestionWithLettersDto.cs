namespace VocabularyApi.Dtos.Training
{
    public class QuestionWithLettersDto : QuestionDto
    {
        public QuestionWithLettersDto(UserVocabularyWordDto userWord, int number, char[] answerLetters) : base(userWord, number)
        {
            AnswerLetters = answerLetters;
        }

        public char[] AnswerLetters { get; set; }
    }
}
