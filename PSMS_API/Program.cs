using Microsoft.EntityFrameworkCore;
using PSMS_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PsmsDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PSMSDB")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/db-check", async (PsmsDbContext db) => 
    await db.Database.CanConnectAsync() ? "DB Connected." : "DB Connected Fail.");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();