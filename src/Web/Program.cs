using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Configurations;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;

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

            //IOC
            //Injeção de dependencia
            builder.Services.AddTransient<ImobiliariaDbContext>();
            builder.Services.AddTransient<IServiceCliente, ServiceCliente>();
            builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();

            builder.Services.Configure<ConnectionStrings>(
                builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.Configure<Tokens>(
                builder.Configuration.GetSection("Tokens"));


            var app = builder.Build();


            var db = app.Services.GetService<ImobiliariaDbContext>();
            db.Seed();

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
