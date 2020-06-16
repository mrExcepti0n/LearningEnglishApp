using LearningEnglishWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Abstractions
{
    public interface IVocabularyService
    {
        Task<List<UserVocabulary>> GetVocabularies();

        Task<UserVocabulary> GetVocabulary(int id);

        Task<List<UserWord>> GetWords(string mask = null, int? vocabularyId = null);

        Task<List<string>> GetTranslations(string word);

        Task AddWord(string name, string translation, int? vocabularyId = null);

        Task RemoveWord(int wordId);

    }
}