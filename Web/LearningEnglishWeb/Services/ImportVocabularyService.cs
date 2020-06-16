using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public class ImportVocabularyService : IImportVocabularyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ImportVocabularyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("ImportVocabularyUrl").Value;
        }

        public async Task LoadDictionary(byte[] array)
        {
            var requestUrl = Api.ImportVocabulary.LoadDictionary(_baseUrl);
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(new MemoryStream(array)), "fromFile", "vocabulary.xdxf");
            var result = await _httpClient.PostAsync(requestUrl, content);
            result.EnsureSuccessStatusCode();
        }
    }
}
