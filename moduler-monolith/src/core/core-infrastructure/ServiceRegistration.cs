using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace core_infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, Action<Options> options)
        {
            var dependencyOptions = new Options();
            options(dependencyOptions);

            services.AddMassTransit(config =>
            {
                dependencyOptions.Consumers.ForEach(consumer =>
                {
                    config.AddConsumer(consumer.ConsumerType);
                });

                config.UsingInMemory((cxt, cfg) =>
                {
                    //cfg.ConfigureEndpoints(cxt);
                    dependencyOptions.Consumers.ForEach(consumer =>
                    {
                        cfg.ReceiveEndpoint(consumer.QueueName, ep =>
                        {
                            ep.UseMessageRetry(x => x.Interval(dependencyOptions.MaxRetryCount, TimeSpan.FromMilliseconds(dependencyOptions.RetryAttemptInterval)));
                            ep.ConfigureConsumer(cxt, consumer.ConsumerType);
                        });
                    });
                });
            });

            return services;
        }

        public sealed class Options
        {
            public int MaxRetryCount { get; set; } = 3;
            public int RetryAttemptInterval { get; set; } = 10000; //MilliSeconds
            public List<Consumer> Consumers { get; set; } = new List<Consumer>();
        }

        public sealed class Consumer
        {
            public string QueueName { get; set; }
            public Type ConsumerType { get; set; }
        }
    }
}
