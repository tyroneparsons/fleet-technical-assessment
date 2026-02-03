using Fleet.Modules.Patients.Contracts;
using Fleet.Modules.Patients.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Modules.Patients;

public static class PatientsModuleExtensions
{
    public static IServiceCollection AddPatientsModule(this IServiceCollection services)
    {
        services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();

        return services;
    }
}