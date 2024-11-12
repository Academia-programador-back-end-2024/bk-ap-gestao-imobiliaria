using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web
{
    public class ServicoTrasDosPanos : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicoTrasDosPanos(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Intervalo de 5 minutos
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

                // Criando escopo para acessar o ImobiliariaDbContext
                using (var scope = _serviceProvider.CreateScope())
                {
                    var serviceImovel = scope.ServiceProvider.GetRequiredService<IServiceImovel>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger>();

                    // TODO: Lógica a ser executada em segundo plano, como limpeza ou verificações
                    logger.LogInformation("Serviço de background executando...");

                    // Exemplo: Atualizando dados, limpando registros ou checando status
                    // dbContext.MinhasTarefasDeBackground();


                }
            }
        }
    }
}
