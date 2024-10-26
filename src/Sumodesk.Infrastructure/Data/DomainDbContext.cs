using Microsoft.EntityFrameworkCore;

namespace Sumodesk.Infrastructure.Data;

public class DomainDbContext : DbContext
{
    public DomainDbContext(DbContextOptions options)
        : base(options)
    {
        
    }
}
