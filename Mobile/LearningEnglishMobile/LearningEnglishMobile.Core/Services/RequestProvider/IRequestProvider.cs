using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task DeleteAsync(string uri, string token = "");

        Task<Stream> GetStreamAsync(string requestUrl, string token = "");
    }
}
