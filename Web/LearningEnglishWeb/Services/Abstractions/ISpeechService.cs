using Data.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Abstractions
{
    public interface ISpeechService
    {
        Task<string> GetAudio(string word, LanguageEnum language);

        Task<string> GetText(Stream stream);
    }
}
