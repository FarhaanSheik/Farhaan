using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Farhaan.Models;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farhaan.Areas.Identity.Data;

public class FarhaanContext : IdentityDbContext<IdentityUser>
{
    public FarhaanContext(DbContextOptions<FarhaanContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //ModelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        //{
        //    entity.HasKey(e => new {e.LoginProvider, e.ProviderKey})
        //}
    // Customize the ASP.NET Identity model and override the defaults if needed.
    // For example, you can rename the ASP.NET Identity table names and more.
    // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<Farhaan.Models.User> User { get; set; } = default!;

public DbSet<Farhaan.Models.Booking> Booking { get; set; } = default!;

public DbSet<Farhaan.Models.Car> Car { get; set; } = default!;

   
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.UserName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.PhoneNumber).HasMaxLength(20).IsRequired();
        builder.Property(u => u.LicenseNumber).HasMaxLength(8).IsRequired();
    }
}



    



       