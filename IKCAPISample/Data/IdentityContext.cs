using IKCAPISample.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKCAPISample.Data
{
    public class IdentityContext: IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> dbContextOptions):base(dbContextOptions)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ApplicationUser>().ToTable("Users");
            //builder.Entity<ApplicationRole>().ToTable("Roles");

            base.OnModelCreating(builder);
        }
    }
}
