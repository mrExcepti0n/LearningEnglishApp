using LearningEnglishWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public interface IVocabularyService
    {
        Task<List<Word>> GetWords(string mask = null);

        Task<List<string>> GetTranslations(string word);

        Task AddWord(string name, string translation);

        Task RemoveWord(string name, string translation);


        Task<List<Word>> GetRequiringStudyWords();


        Task LoadDictionary(byte[] array);
    }
}