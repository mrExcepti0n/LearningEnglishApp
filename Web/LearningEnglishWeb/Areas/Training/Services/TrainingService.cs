using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Services.Dtos;
using LearningEnglishWeb.Areas.Training.Services.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Areas.Training.Services
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

        public async Task<IEnumerable<T>> GetTrainingQuestions<T>(TrainingTypeEnum trainingType, bool isReverseTraining, IEnumerable<int> selectedUserWords)
        {
            var requestUrl = Api.Training.GetTrainingQuestions(_baseUrl, trainingType, isReverseTraining, selectedUserWords);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);

            return JsonConvert.DeserializeObject<List<T>>(stringResult);
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

        public async Task<Dictionary<(TrainingTypeEnum, bool), int>> GetAvailableTrainingWordsCount()
        {
            var requestUrl = Api.Training.GetAvailableTrainingWordsCount(_baseUrl);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            var result = JsonConvert.DeserializeObject<List<TrainingAvailableWordsDto>>(stringResult);

            return result.ToDictionary(key => (key.TrainingType, key.IsReverseTraining), value => value.AvailableWordsCount);
        }

    }
}
