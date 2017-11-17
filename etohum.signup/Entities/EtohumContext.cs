using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Entities
{
    public class EtohumContext : DbContext
    {
        // code first, database context is declared here.
        public EtohumContext(DbContextOptions<EtohumContext> options)
            :base(options)
        {
            // for every time the project runs it will dedict the last database migration,
            // and update the database accordingly (keep database updated)
            Database.Migrate();
        }

        // db sets used to reach tables
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
    }
}
