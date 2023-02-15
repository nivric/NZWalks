using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface ITokenHandler
    {
        Task<string> GetTokenAsync(User user);
    }
}
