using System.Security.Cryptography;
using System.Text;
using Vietravel.Tours.Application.Abstractions;
using Vietravel.Tours.Domain;

namespace Vietravel.Tours.Application.Services;

public sealed class AuthService(IUserRepository users, ITokenService tokens) : IAuthService
{
    public async Task RegisterAsync(string username, string password, CancellationToken cancellationToken)
    {
        username = NormalizeUsername(username);
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required.", nameof(username));
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password is required.", nameof(password));

        var existing = await users.FindByUsernameAsync(username, cancellationToken);
        if (existing is not null) throw new InvalidOperationException("Username already exists.");

        var user = new User
        {
            Username = username,
            Password = HashPassword(password),
            CreatedAt = DateTime.UtcNow
        };

        await users.AddAsync(user, cancellationToken);
    }

    public async Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        username = NormalizeUsername(username);
        var user = await users.FindByUsernameAsync(username, cancellationToken);
        if (user is null) throw new UnauthorizedAccessException("Invalid credentials.");

        if (!VerifyPassword(password, user.Password)) throw new UnauthorizedAccessException("Invalid credentials.");

        return tokens.CreateAccessToken(user.Username);
    }

    private static string NormalizeUsername(string username) => (username ?? string.Empty).Trim().ToLowerInvariant();

    // Format: v1.<iterations>.<saltB64>.<hashB64>
    private static string HashPassword(string password)
    {
        const int iterations = 100_000;
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            32);

        return $"v1.{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string stored)
    {
        var parts = stored.Split('.', 4);
        if (parts.Length != 4) return false;
        if (parts[0] != "v1") return false;
        if (!int.TryParse(parts[1], out var iterations)) return false;
        var salt = Convert.FromBase64String(parts[2]);
        var expected = Convert.FromBase64String(parts[3]);

        var actual = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            expected.Length);

        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}

