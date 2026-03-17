using Vietravel.Tours.Application.Abstractions;

namespace Vietravel.Tours.Application.Services;

public sealed class TourService(ITourRepository tourRepository) : ITourService
{
    public async Task<IReadOnlyList<TourDto>> GetToursAsync(CancellationToken cancellationToken)
    {
        var tours = await tourRepository.GetAllAsync(cancellationToken);
        return tours
            .Select(t => new TourDto(t.Id, t.Name, t.Price, t.City))
            .ToArray();
    }
}

