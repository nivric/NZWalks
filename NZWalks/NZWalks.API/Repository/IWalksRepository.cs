using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IWalksRepository
    {
        Task<List<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid id);
        Task<Walk> AddWalksAsync(Walk walk);
        Task<Walk> DeleteWalksAsync(Guid id);
        Task<Walk> UpdateWalksAsync(Guid id, Walk walk);
    }
}
