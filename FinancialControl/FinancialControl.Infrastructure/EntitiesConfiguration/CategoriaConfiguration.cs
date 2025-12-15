using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Infrastructure.EntitiesConfiguration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.CategoriaId);

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Finalidade)
                   .IsRequired();

            //Caso a categoria tente ser apagada, não permitirá se houver transãções associadas
            builder.HasMany(c => c.Transacoes)
                   .WithOne(t => t.Categoria)
                   .HasForeignKey(t => t.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
    new Categoria
    {
        CategoriaId = 1,
        Descricao = "Alimentação",
        Finalidade = FinalidadeCategoria.Despesa
    },
    new Categoria
    {
        CategoriaId = 2,
        Descricao = "Salário",
        Finalidade = FinalidadeCategoria.Receita
    },
    new Categoria
    {
        CategoriaId = 3,
        Descricao = "Geral",
        Finalidade = FinalidadeCategoria.Ambas
    }
);
        }
    }
}
