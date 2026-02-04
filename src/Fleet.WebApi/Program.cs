using Fleet.Modules.Patients;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMassTransit(c =>
{
    builder.Services.AddPatientsModule(c);

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
app.MapPatientsModuleEndpoints();

app.Run();