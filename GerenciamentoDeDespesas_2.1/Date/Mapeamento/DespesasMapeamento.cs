using GerenciamentoDeDespesas_2._1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeDespesas_2._1.Date.Map
{
    public class DespesasMapeamento : IEntityTypeConfiguration<Despesas>
    {
        public void Configure(EntityTypeBuilder<Despesas> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuarios);
        }
    }
}
