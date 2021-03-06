﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using Microsoft.EntityFrameworkCore;
using VocabularyApi.Dtos;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;

namespace VocabularyApi.Services.Training.Abstractions
{
    public abstract class TrainingServiceBase<T> : ITrainingService<T> 
    {
        private readonly Random _random;
        private readonly TrainingTypeEnum _trainingType;
        private readonly VocabularyContext _vocabularyContext;

        protected TrainingServiceBase(VocabularyContext context, TrainingTypeEnum trainingType)
        {
            _vocabularyContext = context;
            _trainingType = trainingType;
            _random = new Random();
        }

        protected W[] ShuffleWords<W>(W[] words)
        {
            for (int i = words.Length - 1; i >= 1; i--)
            {
                int j = _random.Next(i + 1);
                var temp = words[j];
                words[j] = words[i];
                words[i] = temp;
            }

            return words;
        }


        public async Task<IEnumerable<UserVocabularyWordDto>> GetUserWordAsync(Guid userId, bool isReverseTraining, int wordsCount = 10)
        {
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>().Where(uv => uv.UserVocabulary.UserId == userId)
                                                                        .Include(uw => uw.TrainingStatistics)
                                                                        .ToListAsync();
            //todo order/take on sever
            userWords = userWords.Where(uv => uv.NeedToRepeat(_trainingType, isReverseTraining))
                .OrderByDescending(uv => uv.GetKnowledgeRatio(_trainingType, isReverseTraining))
                .Take(wordsCount)
                .ToList();

            return GetUserVocabularyWords(userWords, isReverseTraining).ToList();
        }

        public async Task<IEnumerable<UserVocabularyWordDto>> GetUserWordAsync(Guid userId, IEnumerable<int> userWordIds, bool isReverseTraining)
        {
            var userWords = await _vocabularyContext.Set<UserVocabularyWord>()
                .Where(uv => uv.UserVocabulary.UserId == userId && userWordIds.Contains(uv.Id))
                .ToListAsync();
            return GetUserVocabularyWords(userWords, isReverseTraining).ToList();
        }


        private IEnumerable<UserVocabularyWordDto> GetUserVocabularyWords(IEnumerable<UserVocabularyWord> userVocabularyWords, bool isReverseTraining)
        {
            foreach (var userVocabularyWord in userVocabularyWords)
            {
                yield return new UserVocabularyWordDto(userVocabularyWord, isReverseTraining);
            }
        }

        public abstract IEnumerable<T> GetQuestions(IEnumerable<UserVocabularyWordDto> words);


       
    }
}
