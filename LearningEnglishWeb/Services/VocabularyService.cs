using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Models;

namespace LearningEnglishWeb.Services
{
    public class VocabularyService : IVocabularyService
    {

        private List<Word> _words = new List<Word>
        {
                new Word {Name ="Dog", Translation ="Собака", Transcription = "[Dog]"},
                new Word {Name ="Fox", Translation ="Лиса", Transcription = "[Fox]"}
         };


        public List<Word> GetWords(string mask = null)
        {
            var dict = _words.ToDictionary(key => key.Name.ToLower());

            if (mask == null)
            {
                return dict.Select(d => d.Value).ToList();
            }

            return dict.Where(d => d.Key.Contains(mask.ToLower())).Select(d => d.Value).ToList();
        }
        public List<string> GetTranslations(string word)
        {

            return new List<string>
            {
                "Слово 1",
                "Слово 2",
                "Слово 3"
            };
        }


        public void AddWord(string name, string translation)
        {
            _words.Add(new Word { Name = name, Translation = translation });
        }


    }
}
