namespace Vietravel.Tours.Api.Security;

public sealed class IpAllowListOptions
{
    public const string SectionName = "IpAllowList";

    public bool Enabled { get; init; } = true;
    public string[] AllowedIps { get; init; } = [];
}

