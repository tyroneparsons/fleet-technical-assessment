using Fleet.Modules.Patients;
using Fleet.Modules.Patients.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddPatientsModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/patients/{id}", async (int id, IPatientRepository repository) =>
{
    return await repository.GetByIdAsync(id);
})
.WithName("GetPatient");

app.Run();