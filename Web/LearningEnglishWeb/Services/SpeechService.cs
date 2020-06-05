using Data.Core;
using LearningEnglishWeb.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public class SpeechService : ISpeechService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public SpeechService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("SpeechUrl").Value;
        }


        public async Task<string> GetAudio(string word, LanguageEnum language)
        {
            var result = await _httpClient.GetAsync($"{_baseUrl}/{word}?language={(int)language}");
            byte[] file;

            await using (Stream stream = await result.Content.ReadAsStreamAsync())
            await using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                file = ms.ToArray();
            }

            var fileStr = Convert.ToBase64String(file);
            return $"data:audio/wav;base64,{fileStr}";
        }


        public async Task<string> GetText(Stream stream)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(stream), "file", "sound.wav");
            var result = await _httpClient.PostAsync(_baseUrl, content);
            result.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
        }
    }
}