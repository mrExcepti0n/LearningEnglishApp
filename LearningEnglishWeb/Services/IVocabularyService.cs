using LearningEnglishWeb.Models;
using System.Collections.Generic;

namespace LearningEnglishWeb.Services
{
    public interface IVocabularyService
    {
        List<Word> GetWords(string mask = null);

        List<string> GetTranslations(string word);

        void AddWord(string name, string translation);

        void RemoveWord(string name, string translation);
    }
}