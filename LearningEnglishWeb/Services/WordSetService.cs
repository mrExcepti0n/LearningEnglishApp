using System;
using System.Collections.Generic;
using System.Net.Http;
using LearningEnglishWeb.Services.Dtos;
using Microsoft.Extensions.Configuration;

namespace LearningEnglishWeb.Services
{
    public class WordSetService : IWordSetService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public WordSetService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("VocabularyUrl").Value;
        }

  

        public void AddWordSet(WordSetSaveDto wordSet)
        {
            throw new NotImplementedException();
          //  _wordSetController.Add(wordSet);
        }


        public List<WordSetShortDto> GetWordSets()
        {
            throw new NotImplementedException();
            //return _wordSetController.Get().Value;
        }


        public WordSetDto GetWordSet(int id)
        {
            throw new NotImplementedException();
            //return _wordSetController.Get(id).Value;
        }

        public void AddWords(int[] wordSetItems)
        {
            throw new NotImplementedException();
            // _wordSetController.AddWords(wordSetItems);
        }
    }
}
