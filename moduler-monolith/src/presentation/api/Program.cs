using core_infrastructure;
using messageRelay;
using Microsoft.AspNetCore.Mvc;
using orderApi;
using orderApi.Consumers;
using paymentApi;
using paymentApi.Consumers;
using stockApi;
using stockApi.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoreInfrastructure(options =>
{
    options.Consumers = new List<core_infrastructure.ServiceRegistration.Consumer>
    {
        new core_infrastructure.ServiceRegistration.Consumer
        {
            ConsumerType = typeof(StockDecreaseFailedConsumer),
            QueueName = builder.Configuration.GetValue<string>("Modules:Order:Queues:Consume:OrderFailed")
        },
        new core_infrastructure.ServiceRegistration.Consumer
        {
            ConsumerType = typeof(PaymentSuccessedConsumer),
            QueueName = builder.Configuration.GetValue<string>("Modules:Order:Queues:Consume:PaymentSuccessed")
        },
        new core_infrastructure.ServiceRegistration.Consumer
        {
            ConsumerType = typeof(OrderPlacedConsumer),
            QueueName = builder.Configuration.GetValue<string>("Modules:Stock:Queues:Consume:OrderPlaced")
        },
        new core_infrastructure.ServiceRegistration.Consumer
        {
            ConsumerType = typeof(StockDecreasedConsumer),
            QueueName = builder.Configuration.GetValue<string>("Modules:Payment:Queues:Consume:StockDecreased")
        }
    };
});
builder.Services.AddOrderModule(builder.Configuration);
builder.Services.AddStockModule(builder.Configuration);
builder.Services.AddPaymentModule(builder.Configuration);
builder.Services.AddMessageRelay();

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
