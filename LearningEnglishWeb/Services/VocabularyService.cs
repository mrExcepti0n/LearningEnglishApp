using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Models;
using VocabularyApi.Controllers;

namespace LearningEnglishWeb.Services
{
    public class VocabularyService : IVocabularyService
    {

        private VocubalryController _vocubalryController;

        public VocabularyService(VocubalryController vocubalryController)
        {
            _vocubalryController = vocubalryController;
        }        

        public List<Word> GetWords(string mask = null)
        {
            var words = _vocubalryController.Get(mask).Value;
            return words.Select(w => new Word { Name = w.Name, Translation = w.Translation }).ToList();  
        }

        public List<string> GetTranslations(string word)
        {
            return _vocubalryController.GetTranslations(word).Value;       
        }

        public void AddWord(string name, string translation)
        {
            _vocubalryController.Post(name, translation);
        }


        public List<Word> GetRequiringStudyWords()
        {
            var words = _vocubalryController.GetRequiringStudyWords(10).Value;
            return words.Select(w => new Word { Name = w.Name, Translation = w.Translation }).ToList();
        }


   
        public void RemoveWord(string name, string translation)
        {
            _vocubalryController.Delete(name, translation);
        }


        public void LoadDictionary(byte[] array)
        {
            _vocubalryController.LoadDictionary(array);
        }

        private int userId => 0;

    }
}
