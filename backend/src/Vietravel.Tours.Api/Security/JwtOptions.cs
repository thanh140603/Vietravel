namespace Vietravel.Tours.Api.Security;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; init; } = "Vietravel.Tours";
    public string Audience { get; init; } = "Vietravel.Tours";
    public string SigningKey { get; init; } = "CHANGE_ME_TO_A_LONG_RANDOM_SECRET";
    public int ExpiryMinutes { get; init; } = 60;

    public string DemoUsername { get; init; } = "admin";
    public string DemoPassword { get; init; } = "admin";
}

