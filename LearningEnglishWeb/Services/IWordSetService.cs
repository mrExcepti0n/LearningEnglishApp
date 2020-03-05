using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Services.Dtos;

namespace LearningEnglishWeb.Services
{
    public interface IWordSetService
    {
        void AddWordSet(WordSetSaveDto wordSet);

        List<WordSetShortDto> GetWordSets();

        WordSetDto GetWordSet(int id);

        void AddWords(int[] wordSetItems);
    }
}
