using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddControllers();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapPost("/RegisterUser1", IResult ([FromQuery] string uuid) =>   {
    return Results.Ok(new { Message = $"QUERY - You sent in uuid: {uuid}" });
})
.WithOpenApi();

app.MapPost("/RegisterUser2", IResult ([FromForm] string uuid) =>   {
    return Results.Ok(new { Message = $"FORM - You sent in uuid: {uuid}" });
})
.DisableAntiforgery()
.WithOpenApi();

app.MapPost("/RegisterUser3", IResult ([FromBody] string uuid) =>   {
    return Results.Ok(new { Message = $"FORM - You sent in uuid: {uuid}" });
})
.WithOpenApi();

app.MapPost("/RegisterUser4", IResult ([FromBody] UuidHolder uuid) =>   {
    return Results.Ok(new { Message = $"BODY - You sent in uuid: {uuid.uuid}" });
})
.WithOpenApi();

app.MapPost("/RegisterUser5", IResult ([FromHeader] string uuid) =>   {
    return Results.Ok(new { Message = $"HEADER - You sent in uuid: {uuid}" });
})
.WithOpenApi();

app.MapPost("/CheckJournalEntry1", IResult ([FromForm] string uuid, [FromForm] JournalEntry jentry) =>   {
    return Results.Ok(new { Message = $"FORM - You sent in uuid: {uuid} But your jentry.id failed : {jentry.Id}" });
})
.DisableAntiforgery()
;//.WithOpenApi();

app.MapControllers();

app.Run();

record UuidHolder{
    public string uuid{get;set;}
}

public class JournalEntry{
    public Int64 Id{get;set;}
    public String? Title{get;set;}
    public String Note{get;set;}
    public String Created{get;set;}
    public String? Updated{get;set;}
}
