using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VocabularyApi.Dtos;
using VocabularyApi.Models;

namespace VocabularyApi.Services.Training.Abstractions
{
    public interface ITrainingService<out QuestionDto>
    {
        Task<IEnumerable<UserVocabularyWordDto>> GetUserWordAsync(Guid userId, bool isReverseTraining, int wordsCount = 10);

        Task<IEnumerable<UserVocabularyWordDto>> GetUserWordAsync(Guid userId, IEnumerable<int> wordsId, bool isReverseTraining);

        IEnumerable<QuestionDto> GetQuestions(IEnumerable<UserVocabularyWordDto> words);
    }
}
