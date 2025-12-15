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
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");

            builder.HasKey(t => t.TransacaoId);

            builder.Property(t => t.Descricao)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Valor)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.TipoTransacao)
                   .IsRequired();

            builder.HasOne(t => t.Pessoa)
                   .WithMany(p => p.Transacoes)
                   .HasForeignKey(t => t.PessoaId);

            builder.HasOne(t => t.Categoria)
                   .WithMany(c => c.Transacoes)
                   .HasForeignKey(t => t.CategoriaId);

            builder.HasData(
    new Transacao
    {
        TransacaoId = 1,
        Descricao = "Supermercado",
        Valor = 150.99m,
        TipoTransacao = TipoTransacao.Despesa,
        CategoriaId = 1, 
        PessoaId = 1   
    },
    new Transacao
    {
        TransacaoId = 2,
        Descricao = "Pagamento Mensal",
        Valor = 3000.00m,
        TipoTransacao = TipoTransacao.Receita,
        CategoriaId = 2, 
        PessoaId = 1  
    },
    new Transacao
    {
        TransacaoId = 3,
        Descricao = "Lanche na escola",
        Valor = 15.50m,
        TipoTransacao = TipoTransacao.Despesa,
        CategoriaId = 3, 
        PessoaId = 2    
    }
);
        }
    }
}
