using glmanager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace glmanager.Data;

public class AppDbContext: IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Usuario>().Property(u => u.NomeCompleto).IsRequired();
        builder.Entity<Usuario>().Property(u => u.Cpf).IsRequired();
    }
}
