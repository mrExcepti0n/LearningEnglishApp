using System.Security.Principal;

namespace LearningEnglishWeb.Services.Abstractions
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
