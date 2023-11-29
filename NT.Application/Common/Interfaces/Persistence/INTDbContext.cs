using Microsoft.EntityFrameworkCore;
using NT.Domain.BlackList;
using NT.Domain.Teachers;
using NT.Domain.Users;

namespace NT.Application.Common.Interfaces.Persistence;

public interface INTDbContext
{
    DbSet<User> Users { get; }

    DbSet<Teacher> Teachers { get; }

    DbSet<BlackList> BlackLists { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
