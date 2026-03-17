namespace Vietravel.Tours.Application.Abstractions;

public interface ITourService
{
    Task<IReadOnlyList<TourDto>> GetToursAsync(CancellationToken cancellationToken);
}

