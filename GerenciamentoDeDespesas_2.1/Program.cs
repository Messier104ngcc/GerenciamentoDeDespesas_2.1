using GerenciamentoDeDespesas_2._1.Date;
using GerenciamentoDeDespesas_2._1.Helper;
using GerenciamentoDeDespesas_2._1.Manutenção.Logs_System;
using GerenciamentoDeDespesas_2._1.Repository;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace GerenciamentoDeDespesas_2._1
{

    //Essa classe é responsável por configurar e iniciar o aplicativo.
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // adicionando a conexção com Banco de Dados, ou acessando a conction string do banco de dados.
            builder.Services.AddDbContext<DBContextApp>(options =>
            {               
                try
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro ao conectar ao servidor de dados. Verifique a conexão e tente novamente.: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado. Contate o suporte: {ex.Message}");
                }
            });

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddScoped<IDespesasRepositorio, DespesasRepositorio>();

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            builder.Services.AddScoped<IBancos, BancosRepositorio>();

            builder.Services.AddScoped<INewBanco, NewBancoRepositorio>();

            builder.Services.AddScoped<ILogs, Logs>();

            builder.Services.AddScoped<IEmail, Email>();

            builder.Services.AddScoped<ISessao, Sessao>();

            builder.Services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}