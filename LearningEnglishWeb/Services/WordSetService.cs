using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;
using LearningEnglishWeb.Services.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Services
{
    public class WordSetService : IWordSetService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public WordSetService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("WordSetUrl").Value;
        }

  

        public async Task AddWordSet(WordSetSaveDto wordSet)
        {
            string requestUrl = Api.WordSet.AddWordSet(_baseUrl);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(wordSet), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(requestUrl, content);
            result.EnsureSuccessStatusCode();
        }


        public async Task<List<WordSetShortDto>> GetWordSets()
        {
            string requestUrl = Api.WordSet.GetWordSets(_baseUrl);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<List<WordSetShortDto>>(stringResult);
        }

        public async Task<WordSetDto> GetWordSet(int id)
        {
            var requestUrl = Api.WordSet.GetWordSet(_baseUrl, id);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<WordSetDto>(stringResult);
        }

        public async Task<int> AddWords(int wordSetId, ICollection<int> wordSetItemIds)
        {
            string requestUrl = Api.WordSet.AddWords(_baseUrl);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(new UserWordSetSaveDto { WordSetId = wordSetId, WordSetItemIds = wordSetItemIds }), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(requestUrl, content);
            result.EnsureSuccessStatusCode();

            var jsonResponse = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<int>(jsonResponse);
        }      
    }
}
