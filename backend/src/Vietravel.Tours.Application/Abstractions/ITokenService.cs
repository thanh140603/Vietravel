namespace Vietravel.Tours.Application.Abstractions;

public interface ITokenService
{
    string CreateAccessToken(string username);
}

