using Microsoft.AspNetCore.Http;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using System.Diagnostics.CodeAnalysis;

namespace DeckOfCards.Services
{
    [ExcludeFromCodeCoverage]
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetSessionId() =>
            _httpContextAccessor.HttpContext.Features.Get<IAnonymousIdFeature>().AnonymousId;
    }
}
