using core_application.Abstractions;
using core_domain.Entitites;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace messageRelayService
{
    public class PaymentWorker : BackgroundService
    {
        private readonly ILogger<OrderWorker> _logger;
        private readonly IBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public PaymentWorker(ILogger<OrderWorker> logger, IBus bus, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await SendOutboxMessagesToBroker(stoppingToken);
            }
        }

        public async Task SendOutboxMessagesToBroker(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Payment Message Relay Service is running at: {time}", DateTime.Now);

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var dbContextHandlers = scope.ServiceProvider.GetRequiredService<IEnumerable<IDbContextHandler>>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var dbContextHandler = dbContextHandlers.Single(x => x.ContextId == config.GetValue<string>("Modules:Payment:ContextId"));

                var messages = dbContextHandler.GetDbSet<OutboxMessage>().AsNoTracking().ToList();
                foreach (var relatedOutBoxMessage in messages)
                {
                    try
                    {
                        var message = JsonSerializer.Deserialize(relatedOutBoxMessage.Message, Type.GetType(relatedOutBoxMessage.Type));

                        await this._bus.Publish(message);

                        dbContextHandler.GetDbSet<OutboxMessage>().Remove(relatedOutBoxMessage);
                        await dbContextHandler.SaveChangesAsync(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        this._logger.LogError(ex, $"{relatedOutBoxMessage.Id} idli mesaj event bus gönderiminde hata ile karþýlaþýldý");
                    }
                }
            }
        }
    }
}