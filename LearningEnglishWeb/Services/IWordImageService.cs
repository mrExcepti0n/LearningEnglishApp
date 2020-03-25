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

        Task<string> GetThumbnailSrc(string word);

        Task<byte[]> GetThumbnail(string word);
    }
}
