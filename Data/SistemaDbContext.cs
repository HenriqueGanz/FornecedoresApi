using FornecedoresApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresApi.Data
{
    public class SistemaDbContext : DbContext
    {
        public SistemaDbContext(DbContextOptions<SistemaDbContext> options)
            : base(options) {  }
            public DbSet<Fornecedor> Fornecedores { get; set; }
            public DbSet<Contato> Contatos { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)  {
                base.OnModelCreating(builder);
                builder.Entity<Fornecedor>()
                    .HasMany(e => e.Contatos)
                    .WithOne(e => e.Fornecedor)
                    .HasForeignKey(e => e.FornecedorId)
                    .HasPrincipalKey(e => e.Id);
            }
    }

}