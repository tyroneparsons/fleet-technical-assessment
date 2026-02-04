using Fleet.Modules.Patients.Infrastructure;

namespace Fleet.Modules.Patients.Tests.Unit;

[TestClass]
public sealed class PatientsRepositoryTests
{
    [TestMethod]
    public async Task InMemoryPatientRepository_GetByIdAsync_WhenPatientExists_ReturnsMappedDetail()
    {
        var repository = new InMemoryPatientRepository();

        var patient = await repository.GetByIdAsync(1);

        Assert.IsNotNull(patient);
        Assert.AreEqual(1, patient.Id);
        Assert.AreEqual("485 210 9932", patient.NHSNumber);
        Assert.AreEqual("Tyrone Parsons", patient.Name);
        Assert.AreEqual(new DateOnly(1975, 5, 14), patient.DateOfBirth);
        Assert.AreEqual("Swarland Avenue Surgery", patient.GPPractice);
    }

    [TestMethod]
    public async Task InMemoryPatientRepository_GetByIdAsync_WhenPatientDoesNotExist_ReturnsNull()
    {
        var repository = new InMemoryPatientRepository();

        var patient = await repository.GetByIdAsync(-123);

        Assert.IsNull(patient);
    }
}

