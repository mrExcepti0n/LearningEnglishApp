using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;
using LearningEnglishWeb.Services.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Services
{
    public class VocabularyService : IVocabularyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public VocabularyService(HttpClient httpClient, IConfiguration configuration )
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("VocabularyUrl").Value;
        }

        public async Task<List<UserVocabulary>> GetVocabularies()
        {
            var requestUrl = Api.Vocabulary.GetVocabularies(_baseUrl);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<List<UserVocabulary>>(stringResult);
        }

        public async Task<UserVocabulary> GetVocabulary(int id)
        {
            var requestUrl = Api.Vocabulary.GetVocabulary(_baseUrl, id);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<UserVocabulary>(stringResult);
        }


        public async Task<List<UserWord>> GetWords(string mask = null, int? vocabularyId = null)
        {
            var requestUrl = Api.Vocabulary.GetWords(_baseUrl, mask, vocabularyId);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<List<UserWord>>(stringResult);  
        }

        public async Task<List<string>> GetTranslations(string word)
        {
            var requestUrl = Api.Vocabulary.GetTranslation(_baseUrl, word);
            var stringResult = await _httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<List<string>>(stringResult);
        }

        public async Task AddWord(string name, string translation, int? vocabularyId = null)
        {
            var requestUrl = Api.Vocabulary.AddWord(_baseUrl);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(new { Word = name, Translation = translation, UserVocabularyId = vocabularyId }), Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(requestUrl, content);
            result.EnsureSuccessStatusCode();
        }


        public async Task RemoveWord(int wordId)
        {
            var requestUrl = Api.Vocabulary.RemoveWord(_baseUrl, wordId);
            var result = await _httpClient.DeleteAsync(requestUrl);
            result.EnsureSuccessStatusCode();
        }


        public async Task LoadDictionary(byte[] array)
        {
            var requestUrl = Api.Vocabulary.LoadDictionary(_baseUrl);
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(new MemoryStream(array)), "fromFile", "vocabulary.xdxf");
            var result = await _httpClient.PostAsync(requestUrl, content);
            result.EnsureSuccessStatusCode();          
        }    

    }
}
