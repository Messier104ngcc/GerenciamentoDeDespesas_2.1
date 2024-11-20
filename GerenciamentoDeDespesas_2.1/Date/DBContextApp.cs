using GerenciamentoDeDespesas_2._1.Date.Map;
using GerenciamentoDeDespesas_2._1.Date.Mapeamento;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeDespesas_2._1.Date
{
    public class DBContextApp : DbContext
    {

        // construtor
        public DBContextApp(DbContextOptions<DBContextApp> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapeamento());

            modelBuilder.ApplyConfiguration(new DespesasMapeamento());

            modelBuilder.ApplyConfiguration(new ContaMapeamento());

            base.OnModelCreating(modelBuilder);
        }


        // criando a tabela no banco de dados
        public DbSet<Despesas> Despesas { get; set; }

        public DbSet<Models.Usuarios> Usuarios { get; set; }

        public DbSet<Conta_Bancaria> Conta_Bancaria { get; set; }

        public DbSet<BancoModel> Bancos { get; set; }

        public DbSet<BancoModel> BancoModels { get; set; }
    }
}
