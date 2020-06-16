using System;
using System.Collections.Generic;
using System.Linq;
using Data.Core;
using VocabularyApi.Dtos;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services.Training.Abstractions;

namespace VocabularyApi.Services.Training
{
    public class CollectWordTrainingService : TrainingServiceBase<QuestionWithLettersDto>
    {
        public CollectWordTrainingService(VocabularyContext context) : base(context, TrainingTypeEnum.CollectWord)
        {
        }

        public override IEnumerable<QuestionWithLettersDto> GetQuestions(IEnumerable<UserVocabularyWordDto> words)
        {
            var userVocabularyWords = words.Select((uv, ind) =>
                    new QuestionWithLettersDto(uv, ind + 1, ShuffleWords(uv.Translation.ToCharArray())))
                .ToList();
            return userVocabularyWords;
        }
    }
}