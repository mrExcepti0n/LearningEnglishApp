using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Dtos;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services;

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private VocabularyContext _vocabularyContext;
        private IIdentityService _identityService;

        public TrainingController(VocabularyContext vocabularyContext, IIdentityService identityService)
        {
            _vocabularyContext = vocabularyContext;
            _identityService = identityService;
        }


        private Guid userId => _identityService.GetUserIdentity();


        [HttpGet("RequiringStudyWords")]
        public async Task<ActionResult<List<UserVocabularyWordDto>>> GetRequiringStudyWords(TrainingTypeEnum trainingType, bool isReverseTraining, int count = 10)
        {
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>().Where(uv => uv.UserVocabulary.UserId == userId).Include(uw => uw.TrainingStatistics).ToListAsync();

            var requiringStudyWords = userWords.Where(uv => uv.NeedToRepeat(trainingType, isReverseTraining))
                                                .OrderByDescending(uv => uv.GetKnowledgeRatio(trainingType, isReverseTraining))
                                                .Take(count)
                                                .ToList();

            return requiringStudyWords.Select(rsw => new UserVocabularyWordDto(rsw)).ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SaveTrainingResult(TrainingResultDto training)
        {
            var userWordIdList = training.TrainingWordResults.Select(tw => tw.UserWordId).ToList();
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>().Where(uv => uv.UserVocabulary.UserId == userId && userWordIdList.Contains(uv.Id)).Include(uvw => uvw.TrainingStatistics).ToListAsync();

            foreach (var userWord in userWords)
            {
                var wordResult = training.TrainingWordResults.Single(trw => trw.UserWordId == userWord.Id);

                userWord.SetTrainingResult(training.TrainingType, training.IsReverseTraining, wordResult.IsRightAnswer, wordResult.UserAnswer);
            }

            await _vocabularyContext.SaveChangesAsync();
            return CreatedAtAction(nameof(SaveTrainingResult), training);
        }


        [HttpGet("TrainingWordsRatio")]
        public async Task<ActionResult<List<TrainingWordRatioDto>>> GetTrainingWordsRatio([FromQuery] List<int> userWordIds)
        {
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>().Where(uv => uv.UserVocabulary.UserId == userId && userWordIds.Contains(uv.Id)).Include(uw => uw.TrainingStatistics).ToListAsync();

            return userWords.Select(uw => new TrainingWordRatioDto { UserWordId = uw.Id, TrainingRatio = (byte)uw.GetKnowledgeRatio() }).ToList();
        }

        [HttpGet("AvailibleTrainingWordsCount")]
        public async Task<ActionResult<List<TrainingAvailableWordsDto>>> GetAvailibleTrainingWordsCount()
        {
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>().Where(uvw => uvw.UserVocabulary.UserId == userId).Include(uw => uw.TrainingStatistics).ToListAsync();

            var trainingTypes = Enum.GetValues(typeof(TrainingTypeEnum)).Cast<TrainingTypeEnum>();

            var result = new List<TrainingAvailableWordsDto>();
            foreach (var trainingType in trainingTypes)
            {
                var trainingWordsCountDto = GetAvailibleTrainingWordsDto(userWords, trainingType, false);
                result.Add(trainingWordsCountDto);

                var inverseTrainingWordsCountDto = GetAvailibleTrainingWordsDto(userWords, trainingType, true);
                result.Add(inverseTrainingWordsCountDto);
            }

            return result;
        }

        private TrainingAvailableWordsDto GetAvailibleTrainingWordsDto(IEnumerable<UserVocabularyWord> userWords, TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            return new TrainingAvailableWordsDto
            {
                TrainingType = trainingType,
                IsReverseTraining = isReverseTraining,
                AvailableWordsCount = GetAvailibleTrainingWordsCount(userWords, trainingType, isReverseTraining)
            };
        }

        private int GetAvailibleTrainingWordsCount(IEnumerable<UserVocabularyWord> userWords, TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            return userWords.Count(uw => uw.NeedToRepeat(trainingType, isReverseTraining));
        }
    }
}