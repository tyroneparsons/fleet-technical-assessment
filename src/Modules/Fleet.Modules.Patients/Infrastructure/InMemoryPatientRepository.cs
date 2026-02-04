using Fleet.Modules.Patients.Abstractions;
using Fleet.Modules.Patients.Contracts;
using Fleet.Modules.Patients.Entities;
using System.Collections.Concurrent;

namespace Fleet.Modules.Patients.Infrastructure;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly ConcurrentDictionary<int, Practice> _practices = new(new Dictionary<int, Practice>
    {
        { 1, new Practice { Id = 1, Name = "Swarland Avenue Surgery" } },
        { 2, new Practice { Id = 2, Name = "Valley View Health" } },
        { 3, new Practice { Id = 3, Name = "Riverside Medical Centre" } },
        { 4, new Practice { Id = 4, Name = "St. Jude’s Family Practice" } }
    });

    private readonly ConcurrentDictionary<int, Patient> _patients = new(new Dictionary<int, Patient>
    {
        { 1, new Patient { Id = 1, PracticeId = 1, NHSNumber = "485 210 9932", FirstName = "Tyrone", LastName = "Parsons", DateOfBirth = new DateOnly(1975, 5, 14) } },
        { 2, new Patient { Id = 2, PracticeId = 1, NHSNumber = "722 415 8801", FirstName = "Maris", LastName = "Portugal", DateOfBirth = new DateOnly(1992, 11, 28) } },
        { 3, new Patient { Id = 3, PracticeId = 2, NHSNumber = "109 332 7546", FirstName = "David", LastName = "Bevan", DateOfBirth = new DateOnly(1948, 3, 02) } },
        { 4, new Patient { Id = 4, PracticeId = 3, NHSNumber = "654 981 2230", FirstName = "Elena", LastName = "Rossi", DateOfBirth = new DateOnly(1988, 08, 19) } },
        { 5, new Patient { Id = 5, PracticeId = 4, NHSNumber = "833 002 1195", FirstName = "William", LastName = "Thompson", DateOfBirth = new DateOnly(2015, 01, 12) } }
    });

    public async Task<PatientDetail?> GetByIdAsync(int id)
    {
        if (!_patients.TryGetValue(id, out var patient) || !_practices.TryGetValue(patient.PracticeId, out var practice))
        {
            return null;
        }

        var detail = new PatientDetail
        {
            Id = patient.Id,
            Name = $"{patient.FirstName} {patient.LastName}",
            NHSNumber = patient.NHSNumber,
            DateOfBirth = patient.DateOfBirth,
            GPPractice = practice.Name
        };

        return await Task.FromResult(detail);
    }
}
