using Academia.Programador.Bk.Gestao.Imobiliaria.DAO;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Configurations;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Views;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            // Adicionar serviços de criação do HttpClient 
            builder.Services.AddHttpClient();

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

            // Registro do BackgroundService
            builder.Services.AddHostedService<ServicoTrasDosPanos>();

            builder.Services.AddTransient<ClienteApiService>();

            //Autenticação
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Login/Denied/";
                    options.LoginPath = "/Convidado/Index"; // Defenir página de login
                    options.LogoutPath = "/Login/Logout"; // Defenir página logout
                });

            //Metodo para indicar que irá rodar como serviço windows.
            //builder.Host.UseWindowsService();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Clientes}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
