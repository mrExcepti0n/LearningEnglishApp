using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.WordImage
{
    public interface IWordImageService
    {
        Task<Stream> GetWordImageStream(string word);
    }
}
