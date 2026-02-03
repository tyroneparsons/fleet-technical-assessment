using Fleet.Modules.Patients.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/patients/{id}", (int id) =>
{
    var patient = new Patient()
    {
        Id = 1,
        NHSNumber = "12345678",
        Name = "Tyrone Ward Parsons",
        DateOfBirth = DateTime.Now,
        GPPractice = "Example Practice"
    };

    return patient;
})
.WithName("GetPatient");

app.Run();