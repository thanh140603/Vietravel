using Microsoft.EntityFrameworkCore;
using Vietravel.Tours.Application.Abstractions;
using Vietravel.Tours.Domain;

namespace Vietravel.Tours.Infrastructure.Persistence;

public sealed class PostgresTourRepository(ToursDbContext db) : ITourRepository
{
    public async Task<IReadOnlyList<Tour>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await db.Tours
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }
}

