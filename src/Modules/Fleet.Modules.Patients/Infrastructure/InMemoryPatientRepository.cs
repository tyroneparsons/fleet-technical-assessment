using Fleet.Modules.Patients.Contracts;

namespace Fleet.Modules.Patients.Infrastructure;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Patient> _patients =
    [
        new Patient
        {
            Id = 1,
            NHSNumber = "485 210 9932",
            Name = "Tyrone Ward Parsons",
            DateOfBirth = new DateOnly(1975, 5, 14),
            GPPractice = "Swarland Avenue Surgery"
        },
        new Patient
        {
            Id = 2,
            NHSNumber = "722 415 8801",
            Name = "Amara Okoro",
            DateOfBirth = new DateOnly(1992, 11, 28),
            GPPractice = "High Street Surgery"
        },
        new Patient
        {
            Id = 3,
            NHSNumber = "109 332 7546",
            Name = "David Bevan",
            DateOfBirth = new DateOnly(1948, 3, 02),
            GPPractice = "Valley View Health"
        },
        new Patient
        {
            Id = 4,
            NHSNumber = "654 981 2230",
            Name = "Elena Rossi",
            DateOfBirth = new DateOnly(1988, 08, 19),
            GPPractice = "Riverside Medical Centre"
        },
        new Patient
        {
            Id = 5,
            NHSNumber = "833 002 1195",
            Name = "William Thompson",
            DateOfBirth = new DateOnly(2015, 01, 12),
            GPPractice = "St. Jude’s Family Practice"
        }
    ];

    public async Task<Patient?> GetByIdAsync(int id)
    {
        return await Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));
    }
}
