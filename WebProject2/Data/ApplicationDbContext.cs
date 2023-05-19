using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProject2.Models;

namespace WebProject2.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");

            builder.Entity<Client>(entity =>
            {
                entity.ToTable(name: "Users" );

            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");

            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");

            });


            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaims");

            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");

            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable(name: "UserToken");

            });





        }

        public DbSet<WebProject2.Models.ShopProduct>? ShopProduct { get; set; }

    }
}