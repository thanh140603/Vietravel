using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vietravel.Tours.Application;
using Vietravel.Tours.Application.Abstractions;

namespace Vietravel.Tours.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ToursController(ITourService tourService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IReadOnlyList<TourDto>>> Get(CancellationToken cancellationToken)
    {
        var tours = await tourService.GetToursAsync(cancellationToken);
        return Ok(tours);
    }
}

