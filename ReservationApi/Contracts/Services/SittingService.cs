using Microsoft.EntityFrameworkCore;
using ReservationApi.Contracts.Interfaces;
using ReservationApi.Data;

namespace ReservationApi.Contracts.Services
{
    public class SittingService : ISittingsService
    {
        private readonly ReservationApiDbContext _context;
        public SittingService(ReservationApiDbContext context)
        {
            _context = context;
        }
        public async Task<Sitting> CreateSitting(Sitting sitting)
        {
            await _context.Sittings.AddAsync(sitting);
            await _context.SaveChangesAsync();
            return sitting;
        }

        public async Task<List<SittingType>> GetSittingTypes()
        {
            return await _context.SittingTypes.ToListAsync();
        }

        public async Task<SittingType> GetSittingTypeIdAsync(int id)
        {
            return await _context.SittingTypes.FirstAsync(t => t.Id == id);
        }

        public async Task<List<Sitting>> CreateSittingGroup(List<Sitting> sittings)
        {
            await _context.Sittings.AddRangeAsync(sittings);
            await _context.SaveChangesAsync();
            return sittings;
        }

        public async Task DeleteSitting(Sitting sitting)
        {
            _context.Remove(sitting);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<Sitting> GetSitting(int id)
        {
            return await _context.Sittings.FindAsync(id);
        }

        public async Task<List<Sitting>> GetSittings()
        {
            return await _context.Sittings.Include(s => s.SittingType).ToListAsync();
        }

        public async Task<List<Sitting>> GetSittingsRange(DateTime start, DateTime end)
        {
            return await _context.Sittings.Where(a => a.Start.Date >= start.AddDays(-1).Date && a.End.Date <= end.AddDays(1).Date).ToListAsync();
        }

        public async Task<Sitting> UpdateSitting(Sitting sitting)
        {
            _context.Update(sitting);
            await _context.SaveChangesAsync();
            return sitting;
        }

        public async Task<List<Sitting>> UpdateSittingGroup(List<Sitting> sittings)
        {
            _context.UpdateRange(sittings);
            await _context.SaveChangesAsync();
            return sittings;
        }
    }
}
