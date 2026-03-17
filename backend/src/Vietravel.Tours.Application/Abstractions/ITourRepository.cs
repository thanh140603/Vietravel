using Vietravel.Tours.Domain;

namespace Vietravel.Tours.Application.Abstractions;

public interface ITourRepository
{
    Task<IReadOnlyList<Tour>> GetAllAsync(CancellationToken cancellationToken);
}

