using Data.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Dtos;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services;
using VocabularyApi.Services.Training;

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly VocabularyContext _vocabularyContext;
        private readonly IIdentityService _identityService;

        public TrainingController(VocabularyContext vocabularyContext, IIdentityService identityService)
        {
            _vocabularyContext = vocabularyContext;
            _identityService = identityService;
        }

        private Guid UserId => _identityService.GetUserIdentity();

        [HttpGet("TrainingQuestion")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetTrainingQuestions(TrainingTypeEnum trainingType, bool isReverseTraining, int count = 10)
        {
            var trainingService = TrainingFactory.GetTrainingService(_vocabularyContext, trainingType);
            var words = await trainingService.GetUserWordAsync(UserId, isReverseTraining);
            var result = trainingService.GetQuestions(words);
            return result.ToList();
        }

        [HttpGet("TrainingWordsRatio")]
        public async Task<ActionResult<List<TrainingWordRatioDto>>> GetTrainingWordsRatio([FromQuery] List<int> userWordIds)
        {
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>().Where(uv => uv.UserVocabulary.UserId == UserId
                                                                                           && userWordIds.Contains(uv.Id)).Include(uw => uw.TrainingStatistics)
                .ToListAsync();

            return userWords.Select(uw => new TrainingWordRatioDto { UserWordId = uw.Id, TrainingRatio = (byte)uw.GetKnowledgeRatio() }).ToList();
        }

        [HttpGet("AvailableTrainingWordsCount")]
        public async Task<ActionResult<List<TrainingAvailableWordsDto>>> GetAvailableTrainingWordsCount()
        {
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>().Where(uvw => uvw.UserVocabulary.UserId == UserId).Include(uw => uw.TrainingStatistics).ToListAsync();

            return GetAvailableTrainingWordsCount(userWords).ToList();
        }


        private IEnumerable<TrainingAvailableWordsDto> GetAvailableTrainingWordsCount(List<UserVocabularyWord> userWords)
        {
            var trainingTypes = Enum.GetValues(typeof(TrainingTypeEnum)).Cast<TrainingTypeEnum>();
            foreach (var trainingType in trainingTypes)
            {
                yield return GetAvailableTrainingWordsDto(userWords, trainingType, false);
                yield return GetAvailableTrainingWordsDto(userWords, trainingType, true);
            }
        }


        private TrainingAvailableWordsDto GetAvailableTrainingWordsDto(IEnumerable<UserVocabularyWord> userWords, TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            return new TrainingAvailableWordsDto
            {
                TrainingType = trainingType,
                IsReverseTraining = isReverseTraining,
                AvailableWordsCount = userWords.Count(uw => uw.NeedToRepeat(trainingType, isReverseTraining))
            };
        }
    }
}