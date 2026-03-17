using System.Net;
using Microsoft.Extensions.Options;

namespace Vietravel.Tours.Api.Security;

public sealed class IpAllowListMiddleware(RequestDelegate next, IOptions<IpAllowListOptions> options)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var cfg = options.Value;
        if (!cfg.Enabled)
        {
            await next(context);
            return;
        }

        var remoteIp = context.Connection.RemoteIpAddress;
        if (remoteIp is null)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { message = "IP not allowed." });
            return;
        }

        if (IsAllowed(remoteIp, cfg.AllowedIps))
        {
            await next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsJsonAsync(new { message = "IP not allowed." });
    }

    private static bool IsAllowed(IPAddress remoteIp, string[] allowedIps)
    {
        if (allowedIps.Length == 0) return false;

        foreach (var ip in allowedIps)
        {
            if (IPAddress.TryParse(ip, out var allowed) && allowed.Equals(remoteIp))
            {
                return true;
            }
        }

        return false;
    }
}

