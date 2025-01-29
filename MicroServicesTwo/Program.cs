using MassTransit;
using MicroServicesTwo.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumer>();
    x.AddConsumer<OrderUpdatedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        //kuyruk olusturuluyor ve izleniyor.
        cfg.ReceiveEndpoint("microservice-two.order.created.event.queue", 
            e => {



                e.ConcurrentMessageLimit = 1;

                e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
            });


        cfg.ReceiveEndpoint("microservice-two.order.updated.event.queue",
           e => {

               e.ConcurrentMessageLimit = 1;

               e.ConfigureConsumer<OrderUpdatedEventConsumer>(context);
           });


    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
