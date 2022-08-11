using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model;

namespace Core.Database;

public class DiceForgerDbContext: IdentityDbContext<IdentityUser> {
    public DiceForgerDbContext(DbContextOptions<DiceForgerDbContext> options) : base(options) {}

    public DbSet<DFCard> DFCards => Set<DFCard>();
}