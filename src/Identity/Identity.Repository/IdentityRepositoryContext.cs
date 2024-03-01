using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity.Model;

namespace Identity.Repository;

public class IdentityRepositoryContext(DbContextOptions<IdentityRepositoryContext> options) : IdentityDbContext<AppUser>(options)
{
}
