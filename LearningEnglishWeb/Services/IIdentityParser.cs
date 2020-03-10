using System.Security.Principal;

namespace LearningEnglishWeb.Services
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
