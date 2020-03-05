using Data.Core;
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
    public class SpeachService : ISpeachService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public SpeachService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("SpeachUrl").Value;
        }


        public async Task<string> GetAudio(string word, LanguageEnum language)
        {
            var result =  await _httpClient.GetAsync($"{_baseUrl}/{word}?language={(int)language}");
            byte[] file;
            using (Stream stream = await result.Content.ReadAsStreamAsync())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    file =  ms.ToArray();
                }
            }
        

            var fileStr = Convert.ToBase64String(file);

            return string.Format("data:audio/wav;base64,{0}", fileStr);
        }


        public async Task<string> GetText(Stream stream)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(stream), "file", "sound.wav");
            var result = await _httpClient.PostAsync(_baseUrl, content);
            result.EnsureSuccessStatusCode();
            return  JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
        }
    }
}
