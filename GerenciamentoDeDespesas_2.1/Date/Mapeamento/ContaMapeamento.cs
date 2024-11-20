using GerenciamentoDeDespesas_2._1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeDespesas_2._1.Date.Mapeamento
{
    public class ContaMapeamento : IEntityTypeConfiguration<Conta_Bancaria>
    {
        public void Configure(EntityTypeBuilder<Conta_Bancaria> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuarios);
        }
    }
}
