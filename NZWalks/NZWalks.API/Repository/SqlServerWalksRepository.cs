using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class SqlServerWalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SqlServerWalksRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Walk> AddWalksAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalksAsync(Guid id)
        {
            var walkToDelete = await _dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
            if(walkToDelete != null)
            {
                _dbContext.Walks.Remove(walkToDelete);
                await _dbContext.SaveChangesAsync();
                return walkToDelete;
            }
            return null;  
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            var walks = await _dbContext.Walks.
                Include(x=>x.Region).
                Include(x =>x.WalkDifficulty).ToListAsync();
            return walks;
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            var walk = await _dbContext.Walks.
                Include(x => x.Region).
                Include(x => x.WalkDifficulty).FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk> UpdateWalksAsync(Guid id, Walk walk)
        {
            var walkToUpdate = await _dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
            if (walkToUpdate == null)
            {
                return null;
            }
            walkToUpdate.Name = walk.Name;
            walkToUpdate.Length = walk.Length;
            walkToUpdate.WalkDifficultyId = walk.WalkDifficultyId;
            await _dbContext.SaveChangesAsync();

            return walkToUpdate;
        }
    }
}
