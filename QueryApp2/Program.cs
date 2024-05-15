using Microsoft.Azure.Cosmos;
using QueryApp2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var configuration = builder.Configuration;

builder.Services.AddSingleton((provider) =>
{
    var endpointUri = configuration["CosmosDBSettings:EndpointUri"];
    var primarykey = configuration["CosmosDBSettings:PrimaryKey"];
    var databaseName = configuration["CosmosDBSetting:DatabaseName"];

    var cosmosClientOptions = new CosmosClientOptions
    {
        //ApplicationName = databaseName
    };

    var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

    var cosmosClient = new CosmosClient(endpointUri, primarykey, cosmosClientOptions);
    cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;
    return cosmosClient;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();

builder.Services.AddScoped<ICustomQuestionRepository, CustomQuestionRepository>();

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
