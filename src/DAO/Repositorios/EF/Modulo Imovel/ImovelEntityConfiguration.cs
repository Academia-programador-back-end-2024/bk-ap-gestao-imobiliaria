using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Imovel
{
    public class ImovelEntityConfiguration : IEntityTypeConfiguration<Imovel>
    {
        public void Configure(EntityTypeBuilder<Imovel> builder)
        {
            builder.ToTable("Imoveis");

            builder.HasKey(e => e.ImovelId).HasName("PK__Imoveis__68DA341CB2529FCE");

            builder.Property(e => e.Area).HasColumnType("decimal(10, 2)");
            builder.Property(e => e.Disponivel).HasDefaultValue(true);
            builder.Property(e => e.Endereco).HasMaxLength(255);
            builder.Property(e => e.Negocio).HasDefaultValue(1);
            builder.Property(e => e.Tipo).HasDefaultValue(1);
            builder.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.ClienteDono).WithMany(p => p.Imoveis)
                .HasForeignKey(d => d.ClienteDonoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Imoveis__Cliente__4316F928");

            builder.HasOne(d => d.CorretorGestor).WithMany(p => p.ImoveiCorretorGestors)
                .HasForeignKey(d => d.CorretorGestorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Imoveis__Correto__412EB0B6");

            builder.HasOne(d => d.CorretorNegocio).WithMany(p => p.ImoveiCorretorNegocios)
                .HasForeignKey(d => d.CorretorNegocioId)
                .HasConstraintName("FK__Imoveis__Correto__4222D4EF");

        }
    }
}
