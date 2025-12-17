using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;
        public SQLRegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
                return null;

            _context.Regions.Remove(existingRegion);    // Add, Update, Remove don't actually communicate with the db
            await _context.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.Regions.FindAsync(id);    // Only takes primary key (unlike FirstOrDefault), can't be used with other properties.
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            // Check if region exists
            // On the line below, EF retrieves the entity from the db and starts automatically tracking it, so no need for Update().
            var regionDomain = await _context.Regions.FindAsync(id);

            if (regionDomain == null)
                return null;

            // Map DTO to Domain model
            regionDomain.Code = region.Code;
            regionDomain.Name = region.Name;
            regionDomain.ImageUrl = region.ImageUrl;

            await _context.SaveChangesAsync();

            return region;
        }
    }
}
