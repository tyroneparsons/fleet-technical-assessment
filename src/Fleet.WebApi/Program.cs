using Fleet.Modules.Patients;
using Fleet.Modules.Patients.Contracts;
using Fleet.Modules.Patients.Features;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddPatientsModule();

builder.Services.AddMassTransit(c =>
{
    c.AddConsumer<GetPatientConsumer>();
    c.AddRequestClient<GetPatient>();
    c.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/patients/{id}", async Task<Results<Ok<PatientDetail>, NotFound<ErrorDetails>, InternalServerError<string>>> (int id, IRequestClient<GetPatient> client) =>
{
    var response = await client.GetResponse<PatientDetail, PatientNotFound>(new GetPatient { PatientId = id });

    if (response.Is(out Response<PatientNotFound>? notFound))
    {
        return TypedResults.NotFound(new ErrorDetails() {  Message = $"Patient with ID {notFound.Message.PatientId} was not found." });
    }

    if (response.Is(out Response<PatientDetail>? patient))
    {
        return TypedResults.Ok(patient.Message);
    }

    return TypedResults.InternalServerError("An unexpected response was received from the patient module.");

})
.WithName("GetPatient");

app.Run();