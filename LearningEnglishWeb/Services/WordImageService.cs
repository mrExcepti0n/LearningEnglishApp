﻿using LearningEnglishWeb.Services.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public class WordImageService : IWordImageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public WordImageService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("WordImageUrl").Value;
        }
       

        public async Task<byte[]> GetImage(string word)
        {
            var requestUrl = Api.WordImage.GetWordImage(_baseUrl, word);

            using (HttpResponseMessage response = await _httpClient.GetAsync(requestUrl))
            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
          
        }

        public async Task<string> GetImageSrc(string word)
        {
            var image = await GetImage(word);
            var base64 = Convert.ToBase64String(image);
            return string.Format("data:image/gif;base64,{0}", base64);
        }
    }
}