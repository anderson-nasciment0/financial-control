using FinancialControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Infrastructure.EntitiesConfiguration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder) 
        {
            builder.ToTable("Pessoas");
            builder.HasKey(p => p.PessoaId);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Idade).IsRequired();

            // Relacionamento com transacoes
            builder.HasMany(p => p.Transacoes)
                   .WithOne(t => t.Pessoa)
                   .HasForeignKey(t => t.PessoaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
    new Pessoa
    {
        PessoaId = 1,
        Nome = "Paulo",
        Idade = 30
    },
    new Pessoa
    {
        PessoaId = 2,
        Nome = "Ana",
        Idade = 16
    }
);
        }
    }
}
