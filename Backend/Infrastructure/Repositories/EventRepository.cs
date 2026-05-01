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
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Event>> GetAllAsync()
        {
            return await _context.Events
            .AsNoTracking()
            .ToListAsync();
        }
        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task AddAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

    }
}
