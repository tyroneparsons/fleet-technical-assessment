using Fleet.Modules.Patients.Entities;

namespace Fleet.Modules.Patients.Contracts;

public interface IPatientRepository
{
    public Task<Patient?> GetByIdAsync(int id);
}
