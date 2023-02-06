using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add user services
builder.Services.AddAsposeServices();
builder.Services.AddCors();
var corsSettings = builder.Configuration.GetSection(ConfigurationContstants.CORS_SETTINGS).Get<CorsSettings>();

var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins(corsSettings.AllowedCorsOrigins.ToArray())
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders(HeaderNames.ContentDisposition);
});

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
