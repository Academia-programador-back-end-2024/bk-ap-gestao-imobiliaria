using Academia.Programador.Bk.Gestao.Imobiliaria.DAO;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Configurations;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //SEED
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // Adicionando o contexto e configurando para usar o SQL Server
            builder.Services.AddDbContext<ImobiliariaDbContext>((serviceProvider, options) =>
            {
                var connectionStrings = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>().Value;
                options.UseSqlServer(connectionStrings.Master);
            });

            builder.Services.Configure<ConnectionStrings>(
                builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.Configure<Tokens>(
                builder.Configuration.GetSection("Tokens"));


            builder.Services.AdicionarImplementacoesDeDados();
            builder.Services.AdicionarImplementacoesDominio();

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<ImobiliariaDbContext>();
                db.Seed();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Clientes}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
