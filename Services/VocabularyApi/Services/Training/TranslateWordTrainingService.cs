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
    public class TranslateWordTrainingService : TrainingServiceBase<QuestionDto>
    {
        public TranslateWordTrainingService(VocabularyContext context) : base(context, TrainingTypeEnum.TranslateWord)
        {
        }



        public override IEnumerable<QuestionDto> GetQuestions(IEnumerable<UserVocabularyWordDto> words)
        {
            return words.Select((vw, ind) => new QuestionDto(vw, ind + 1)).ToArray();
        }
    }
}
