using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeDespesas_2._1.Date.Mapeamento
{
    public class UsuarioMapeamento : IEntityTypeConfiguration<Models.Usuarios>
    {
        public void Configure(EntityTypeBuilder<Models.Usuarios> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(t => t.UserId);

            builder.Property(t => t.Nome).HasColumnType("varchar(20)");
            builder.Property(t => t.UserName).HasColumnType("varchar(50)");
            builder.Property(t => t.Email).HasColumnType("varchar(50)");
            builder.Property(t => t.Perfil).HasColumnType("enum");
            builder.Property(t => t.Senha).HasColumnType("varchar(14)");
            builder.Property(t => t.ConfSenha).HasColumnType("varchar(14)");
            builder.Property(t => t.DataCadastro).HasColumnType("DateTime");
            builder.Property(t => t.DataAtualizacao).HasColumnType("DateTime");
        }
    }
}
