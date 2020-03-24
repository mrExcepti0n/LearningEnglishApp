using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Services.Dtos;

namespace LearningEnglishWeb.Services
{
    public interface IWordSetService
    {
        Task AddWordSet(WordSetSaveDto wordSet);

        Task<List<WordSetShortDto>> GetWordSets();

        Task<WordSetDto> GetWordSet(int id);

        Task AddWords(int[] wordSetItems);
    }
}
