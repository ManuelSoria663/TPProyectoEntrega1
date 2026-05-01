using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;
        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Seat>> GetBySectorIdAsync(int sectorId)
        {
            return await _context.Seats
            .Where(s => s.SectorId == sectorId)
            .AsNoTracking()
            .ToListAsync();
        }
        public async Task<Seat?> GetByIdAsync(Guid id)
        {
            return await _context.Seats
            .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task UpdateAsync(Seat seat)
        {
            _context.Seats.Update(seat);
            await _context.SaveChangesAsync();
        }

    }
}
