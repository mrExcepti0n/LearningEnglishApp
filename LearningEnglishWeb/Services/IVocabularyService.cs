using LearningEnglishWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public interface IVocabularyService
    {
        Task<List<UserVocabulary>> GetVocabularies();

        Task<List<UserWord>> GetWords(string mask = null);

        Task<List<string>> GetTranslations(string word);

        Task AddWord(string name, string translation);

        Task RemoveWord(string name, string translation);


        Task<List<UserWord>> GetRequiringStudyWords();


        Task LoadDictionary(byte[] array);
    }
}