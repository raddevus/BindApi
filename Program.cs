using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.MapPost("/RegisterUser1", IResult ([FromQuery] string uuid) =>   {
    return Results.Ok(new { Message = $"QUERY - You sent in uuid: {uuid}" });
})
.WithOpenApi();

app.MapPost("/RegisterUser2", IResult ([FromForm] string uuid) =>   {
    return Results.Ok(new { Message = $"FORM - You sent in uuid: {uuid}" });
})
.DisableAntiforgery()
.WithOpenApi();

app.Run();
