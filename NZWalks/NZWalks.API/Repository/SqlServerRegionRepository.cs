using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class SqlServerRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SqlServerRegionRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = new Guid();
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
             
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if(region == null)
            {
                return null;
            }
            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Walks = region.Walks;
            existingRegion.Area = region.Area;
            existingRegion.Latitude = region.Latitude;
            existingRegion.Longitude = region.Longitude;
            existingRegion.Population = region.Population;

            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
