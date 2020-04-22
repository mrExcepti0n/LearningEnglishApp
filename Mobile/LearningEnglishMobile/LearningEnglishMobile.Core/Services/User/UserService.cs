
using LearningEnglishMobile.Core.Models.User;
using LearningEnglishMobile.Core.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.User
{
    public class UserService : IUserService
    {

        private readonly IRequestProvider _requestProvider;

        public UserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<UserInfo> GetUserInfoAsync(string authToken)
        {
            var uri = GlobalSetting.Instance.UserInfoEndpoint;

            var userInfo = await _requestProvider.GetAsync<UserInfo>(uri, authToken);
            return userInfo;
        }
    }
}
