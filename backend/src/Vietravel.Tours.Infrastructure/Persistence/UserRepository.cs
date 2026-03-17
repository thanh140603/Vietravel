using Microsoft.EntityFrameworkCore;
using Vietravel.Tours.Application.Abstractions;
using Vietravel.Tours.Domain;

namespace Vietravel.Tours.Infrastructure.Persistence;

public sealed class UserRepository(ToursDbContext db) : IUserRepository
{
    public Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync(cancellationToken);
        return user;
    }
}

