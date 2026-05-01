using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Sector> Sectors => Set<Sector>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<User> Users => Set<User>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    }
}
