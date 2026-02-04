using Fleet.Modules.Patients.Contracts;

namespace Fleet.Modules.Patients.Abstractions;

public interface IPatientRepository
{
    public Task<PatientDetail?> GetByIdAsync(int id);
}
