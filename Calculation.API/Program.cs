using Serilog;
using Serilog.Sinks.File;
using System.Reflection;

//Log.Logger = new LoggerConfiguration().WriteTo.File("../log.txt").CreateBootstrapLogger(); ; ;
//Log.Information("Starting up");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Swagger Calculator API",
        Description = "Demo API With Swagger",
        Version = "v1"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(filePath);
});
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Calculation.API.Services.ICalculation, Calculation.API.Services.Calculation>();
builder.Services.AddCors(option => option.AddDefaultPolicy(build => build.AllowAnyOrigin()));

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.File("../log.txt")
        .ReadFrom.Configuration(ctx.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();
