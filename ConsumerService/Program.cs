using Microsoft.Azure.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISubscriptionClient>
    (x =>new SubscriptionClient(builder.Configuration.GetValue<string>("ServiceBus:ConnectionString"),
        builder.Configuration.GetValue<string>("ServiceBus:TopicName"),
        builder.Configuration.GetValue<string>("ServiceBus:Subscription")));
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
