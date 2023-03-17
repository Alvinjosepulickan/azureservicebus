using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using producerApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ITopicClient>(x=>new TopicClient(builder.Configuration.GetValue<string>("ServiceBus:ConnectionString")
    , builder.Configuration.GetValue<string>("ServiceBus:TopicName")));
builder.Services.AddSingleton<IServiceApp, ServiceApp>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
