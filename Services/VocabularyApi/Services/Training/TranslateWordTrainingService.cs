using System;
using System.Collections.Generic;
using Data.Core;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services.Training.Abstractions;

namespace VocabularyApi.Services.Training
{
    public class TranslateWordTrainingService : TrainingServiceBase<QuestionWithOptionsDto>
    {
        public TranslateWordTrainingService(VocabularyContext context) : base(context, TrainingTypeEnum.TranslateWord)
        {
        }

        public override IEnumerable<QuestionWithOptionsDto> GetQuestions(IEnumerable<UserVocabularyWord> words)
        {
            throw new NotImplementedException();
        }
    }
}
