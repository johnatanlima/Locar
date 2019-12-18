using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Locar.Data
{
    public class LocarDbContext : IdentityDbContext
    {
        
        public LocarDbContext(DbContextOptions<LocarDbContext> options)
            : base(options)
        {
        }
    }
}
