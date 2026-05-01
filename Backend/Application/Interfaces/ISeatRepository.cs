using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISeatRepository
    {
        Task<ICollection<Seat>> GetBySectorIdAsync(int sectorId);
        Task<Seat?> GetByIdAsync(Guid id);
        Task UpdateAsync(Seat seat);

    }
}
