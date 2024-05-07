using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// START What I added
builder.Services.AddDbContext<_MPv2DbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// This chooses who is allowed to perform the fetch requests
builder.Services.AddCors(o => {
    // Add a means for localhost:3000 to perform fetch requests on localhost:2790
    // localhost:3001 optional, if you want to start another front end instance
    o.AddPolicy("Cors Policy", p => {
        p.WithOrigins("http://localhost:3000", "http://localhost:3001");
    });
});
// END what I added

var app = builder.Build();

app.UseCors("Cors Policy");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


// START What I added
app.UseDefaultFiles();
app.UseStaticFiles();
// END what I added

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
