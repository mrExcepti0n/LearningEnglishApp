using System;
using System.Collections.Generic;
using Data.Core;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services.Training.Abstractions;

namespace VocabularyApi.Services.Training
{
    public class CollectWordTrainingService : TrainingServiceBase<QuestionDto>
    {
        public CollectWordTrainingService(VocabularyContext context) : base(context, TrainingTypeEnum.CollectWord)
        {
        }

        public override IEnumerable<QuestionDto> GetQuestions(IEnumerable<UserVocabularyWord> words)
        {
            throw new NotImplementedException();
        }
    }
}