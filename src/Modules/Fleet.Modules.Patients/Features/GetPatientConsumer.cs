using Fleet.Modules.Patients.Abstractions;
using Fleet.Modules.Patients.Contracts;
using MassTransit;

namespace Fleet.Modules.Patients.Features
{
    public class GetPatientConsumer(IPatientRepository repository) : IConsumer<GetPatient>
    {
        public async Task Consume(ConsumeContext<GetPatient> context)
        {
            var patient = await repository.GetByIdAsync(context.Message.PatientId);

            if (patient == null)
            {
                await context.RespondAsync(new PatientNotFound { PatientId = context.Message.PatientId });
            }
            else
            {
                await context.RespondAsync(new PatientDetail
                {
                    Id = patient.Id,
                    NHSNumber = patient.NHSNumber,
                    Name = patient.Name,
                    DateOfBirth = patient.DateOfBirth,
                    GPPractice = patient.GPPractice
                });
            }
        }
    }
}
