using Microsoft.AspNetCore.Http;
using System;

namespace VocabularyApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetUserIdentity()
        {
            return Guid.Parse(_context.HttpContext.User.FindFirst("sub").Value);
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}
