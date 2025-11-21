using Microsoft.AspNetCore.Mvc;
using TVGameQuiz.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ADD CORS (very important)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// USE CORS — MUST BE ABOVE EVERYTHING ELSE
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Do NOT use HTTPS redirection when calling from http://127.0.0.1:5500
// But we will keep it here — it still works
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

// 🔥 SEED JSON FROM FILE
SeedData.Seed();

app.Run();
