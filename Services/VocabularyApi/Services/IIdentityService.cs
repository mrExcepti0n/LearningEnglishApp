using System;

namespace VocabularyApi.Services
{
    public interface IIdentityService
    {
        Guid GetUserIdentity();

        string GetUserName();
    }
}
