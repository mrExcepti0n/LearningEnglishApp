using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services
{
    public interface IWordImageService
    {
        Task<byte[]> GetImage(string word);

        Task<string> GetImageSrc(string word);
    }
}
