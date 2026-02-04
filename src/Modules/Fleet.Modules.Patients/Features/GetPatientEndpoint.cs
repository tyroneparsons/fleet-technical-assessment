using Fleet.Modules.Patients.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Fleet.Modules.Patients.Features
{
    public static class GetPatientEndpoint
    {
        public static IEndpointRouteBuilder MapGetPatient(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/patients/{id}", async Task<Results<Ok<PatientDetail>, NotFound<ErrorDetails>, InternalServerError<string>>>
                (int id, IRequestClient<GetPatient> client) =>
            {
                var response = await client.GetResponse<PatientDetail, PatientNotFound>(new { PatientId = id });

                return response.Message switch
                {
                    PatientDetail patient => TypedResults.Ok(patient),
                    PatientNotFound notFound => TypedResults.NotFound(new ErrorDetails { Message = $"Patient {notFound.PatientId} not found." }),
                    _ => TypedResults.InternalServerError("Unexpected response type.")
                };
            })
            .WithName("GetPatient")
            .WithTags("Patients");

            return endpoints;
        }
    }
}
