using Microsoft.EntityFrameworkCore;
using NT.Application.Common.Interfaces.Persistence;
using NT.Domain.BlackList;
using NT.Domain.Teachers;
using NT.Domain.Users;

namespace NT.Infrastructure.Persistence;

internal class NTDbContext : DbContext, INTDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<BlackList> BlackLists { get; set; }

    public NTDbContext(DbContextOptions<NTDbContext> opt) : base(opt) { }
}
