using MongoDB.Driver;
using GameStore.Infrastructure;
using GameStore.Authentication;
using GameStore.Services;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var mongoConnectionString = "mongodb+srv://lass:1@cluster0.xbtinu8.mongodb.net/"; // Environment.GetEnvironmentVariable("DB_URI")
var mongoDatabaseName = config["MongoDatabase"];

if (string.IsNullOrEmpty(mongoConnectionString) || string.IsNullOrEmpty(mongoDatabaseName))
{
    throw new Exception("MongoDB connection settings are missing.");
}

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(config["AllowedHosts"])
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoDatabaseName);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new ApiKeyAuthFilter(configuration, "admin", "manager");
});
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "Cookie";
        options.LoginPath = "/api/access-unauthorized";
        options.AccessDeniedPath = "/api/access-forbidden";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
    options.AddPolicy("ManagerOnly", policy => policy.RequireRole("manager"));
    options.AddPolicy("AdminOrManager", policy => policy.RequireRole("admin", "manager"));
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // ...
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
