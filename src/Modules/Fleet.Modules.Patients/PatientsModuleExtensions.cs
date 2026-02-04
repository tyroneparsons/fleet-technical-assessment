using Fleet.Modules.Patients.Abstractions;
using Fleet.Modules.Patients.Contracts;
using Fleet.Modules.Patients.Features;
using Fleet.Modules.Patients.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Modules.Patients;

public static class PatientsModuleExtensions
{
    public static IServiceCollection AddPatientsModule(this IServiceCollection services, IBusRegistrationConfigurator? bus = null)
    {
        services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();

        if (bus != null)
        {
            bus.AddConsumer<GetPatientConsumer>();
            bus.AddRequestClient<GetPatient>();
        }

        return services;
    }

    public static IEndpointRouteBuilder MapPatientsModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGetPatient();
        
        return endpoints;
    }
}