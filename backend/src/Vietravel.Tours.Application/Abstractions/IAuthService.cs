namespace Vietravel.Tours.Application.Abstractions;

public interface IAuthService
{
    Task RegisterAsync(string username, string password, CancellationToken cancellationToken);
    Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken);
}

