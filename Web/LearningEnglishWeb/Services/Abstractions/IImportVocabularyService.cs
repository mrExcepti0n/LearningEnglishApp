using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Abstractions
{
    public interface IImportVocabularyService
    {
        Task LoadDictionary(byte[] array);
    }
}
