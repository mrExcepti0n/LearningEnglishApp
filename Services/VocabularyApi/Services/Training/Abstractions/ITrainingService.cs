using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VocabularyApi.Models;

namespace VocabularyApi.Services.Training.Abstractions
{
    public interface ITrainingService<out QuestionDto>
    {
        Task<IEnumerable<UserVocabularyWord>> GetUserWordAsync(Guid userId, bool isReverseTraining, int wordsCount = 10);

        IEnumerable<QuestionDto> GetQuestions(IEnumerable<UserVocabularyWord> words);
    }
}
