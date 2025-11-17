using CaseStudy.Contracts;
using CaseStudy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/rezervasyon", (
    ReservationRequest? request,
    IReservationService reservationService,
    ILogger<Program> logger) =>
{
    var validationErrors = ReservationRequestValidator.Validate(request);
    if (validationErrors.Count > 0)
    {
        logger.LogWarning("Rezervasyon istegi dogrulama hatalari: {@Errors}", validationErrors);
        return Results.BadRequest(new ValidationErrorResponse(validationErrors));
    }
    
    var response = reservationService.CreateReservation(request!);
    return response.RezervasyonYapilabilir
        ? Results.Ok(response)
        : Results.BadRequest(response);
})
.WithName("CreateReservation");

app.Run();
