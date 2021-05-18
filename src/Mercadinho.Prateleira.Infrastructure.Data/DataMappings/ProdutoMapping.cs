
using Mercadinho.Prateleira.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mercadinho.Prateleira.Infrastructure.Data.DataMappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTO")
                .HasOne(n=> n.Estoque)
                .WithOne(n=> n.Produto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Estoque>(f=> f.ProdutoId);

            builder.HasMany(n=> n.Categorias)
                .WithMany(n=> n.Produtos);
                
            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder.Property(p => p.Descricao)
                .HasColumnName("DESCRICAO")
                .HasColumnType("varchar")
                .HasMaxLength(300)
                .IsRequired();

                
                
                
        }
    }
}
