using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;
using LearningEnglishWeb.Services.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TrainingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("TrainingUrl").Value;
        }

        public async Task<List<UserWord>> GetRequiringStudyWords(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            var requestUrl = Api.Training.GetRequiringStudyWords(_baseUrl, trainingType, isReverseTraining);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<List<UserWord>>(stringResult);
        }

        public async Task SaveTrainingResult(TrainingResultDto training)
        {
            string requestUrl = Api.Training.SaveTrainingResult(_baseUrl);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(training), Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(requestUrl, content);
            result.EnsureSuccessStatusCode();
        }

        public async Task< Dictionary<int, byte>> GetTrainingWordsRatio(List<int> userWordIds)
        {
            var requestUrl = Api.Training.GetTrainingWordsRatio(_baseUrl, userWordIds);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            var result = JsonConvert.DeserializeObject<List<TrainingWordRatioDto>>(stringResult);

            return result.ToDictionary(key => key.UserWordId, value => value.TrainingRatio);
        }

        public async Task<Dictionary<(TrainingTypeEnum, bool), int>> GetAvailibleTrainingWordsCount()
        {
            var requestUrl = Api.Training.GetAvailibleTrainingWordsCount(_baseUrl);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            var result = JsonConvert.DeserializeObject<List<TrainingAvailableWordsDto>>(stringResult);

            return result.ToDictionary(key => (key.TrainingType, key.IsReverseTraining), value => value.AvailableWordsCount);
        }
    }
}
