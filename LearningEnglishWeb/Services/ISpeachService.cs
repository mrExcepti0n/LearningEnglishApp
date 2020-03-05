using Data.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public interface ISpeachService
    {
        Task<string> GetAudio(string word, LanguageEnum language);

        Task<string> GetText(Stream stream);
    }
}
