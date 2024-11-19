
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Configurations;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Adicionando o contexto e configurando para usar o SQL Server
            builder.Services.AddDbContext<ImobiliariaDbContext>((serviceProvider, options) =>
            {
                var connectionStrings = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>().Value;
                options.UseSqlServer(connectionStrings.Master);
            });

            builder.Services.Configure<ConnectionStrings>(
                builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.AdicionarImplementacoesDeDados();
            builder.Services.AdicionarImplementacoesDominio();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
