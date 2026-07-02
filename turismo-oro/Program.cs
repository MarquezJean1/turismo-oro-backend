using System.Globalization;
using Microsoft.EntityFrameworkCore;
using turismo_oro.Infrastructure;
using turismo_oro.Infrastructure.Data;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(
                builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? ["http://localhost:5173"])
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
    db.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI();
/*if (app.Environment.IsDevelopment())
{
   
}*/

app.UseCors("Frontend");

/*if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}*/
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();
